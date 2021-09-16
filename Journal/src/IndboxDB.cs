using Journal.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.src
{
    public class IndboxDB
    {
        private static string connectionString = Settings.Default.Inbox.First();

        public static List<Board> GetBoard()
        {
            List<Board> boards = new List<Board>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "SELECT * FROM Board";
                    SqlDataReader rd = com.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            Board br = new Board()
                            {
                                ID = Convert.ToInt32(rd["BoardID"]),
                                Name = rd["BoardName"].ToString()
                             };
                            boards.Add(br);
                            
                        }
                    }
                    return boards;
                }
                
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
        public static List<Adressee> GetAllAdressee()
        {
            List<Adressee> adressee = new List<Adressee>();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "SELECT * FROM Adressee";
                    SqlDataReader rd = com.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rd.Read())
                    {
                        Adressee adre = new Adressee(Convert.ToInt32(rd["AdresseID"]), rd["AdresseeName"].ToString());
                        adressee.Add(adre);
                    }
                    return adressee;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public static bool AddJournalItem(JournalItem jr)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                try 
                {
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "INSERT INTO Journal (Authors,JournalName,DateOfRecipient,BoardID,AdresseeID,Notes,RecipientID) VALUES(" +
                         "@author,@name,@date,@boardid,@adresseeid,@note,@rcid)";
                    com.Parameters.AddWithValue("@author", jr.Author);
                    com.Parameters.AddWithValue("@name", jr.Name);
                    com.Parameters.AddWithValue("@date", jr.DateOfRec);
                    com.Parameters.AddWithValue("@boardid", jr.OwnBoard.ID);
                    com.Parameters.AddWithValue("@adresseeid", jr.OwnAdressee.ID);
                    com.Parameters.AddWithValue("@note", jr.Note);
                    com.Parameters.AddWithValue("@rcid", jr.RC.ID);

                    if (com.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public static List<Recipirnt> GetAllRecipients()
        {
            List<Recipirnt> recipient = new List<Recipirnt>();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "SELECT * FROM Recipient";

                    SqlDataReader rd = com.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            recipient.Add(new Recipirnt(Convert.ToInt32(rd["RecipientID"]),rd["RecipientName"].ToString()));
                        }
                    }
                    return recipient;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public static List<JournalItem> GetAllJournalItem(DateTime date)
        {
            List<JournalItem> journal = new List<JournalItem>();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = $"select * from Journal,Adressee,Board,Recipient where Journal.AdresseeID = Adressee.AdresseID and Journal.BoardID = Board.BoardID and DateOfRecipient >= DATEADD(hour,-10,@date) and Journal.RecipientID = Recipient.RecipientID Order By Journal.DateOfRecipient DESC";
                    com.Parameters.AddWithValue("@date", date);
                    IAsyncResult res = com.BeginExecuteReader(CommandBehavior.CloseConnection);

                    SqlDataReader rd = com.EndExecuteReader(res);
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            JournalItem jr = new JournalItem()
                            {
                                ID = Convert.ToInt32(rd["JournalID"]),
                                Author = rd["Authors"].ToString(),
                                Name = rd["JournalName"].ToString(),
                                DateOfRec = Convert.ToDateTime(rd["DateOfRecipient"]),
                                OwnBoard = new Board()
                                {
                                    ID = Convert.ToInt32(rd["BoardID"]),
                                    Name = rd["BoardName"].ToString()
                                },
                                OwnAdressee = new Adressee(
                                    Convert.ToInt32(rd["AdresseID"]), rd["AdresseeName"].ToString()),
                                Note = rd["Notes"].ToString(),
                                RC = new Recipirnt(Convert.ToInt32(rd["RecipientID"]),rd["RecipientName"].ToString())
                            };
                            journal.Add(jr);
                        }

                    }
                    
                    return journal;
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static List<JournalItem> SearchAllJournal(DateTime datefrom,DateTime datefor)
        {
            List<JournalItem> journal = new List<JournalItem>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = $"select * from Journal,Adressee,Board,Recipient where Journal.AdresseeID = Adressee.AdresseID and Journal.BoardID = Board.BoardID and DateOfRecipient BETWEEN @datefrom and @datefor and Journal.RecipientID = Recipient.RecipientID Order By Journal.DateOfRecipient DESC";
                    com.Parameters.AddWithValue("@datefrom", datefrom);
                    com.Parameters.AddWithValue("@datefor", datefor);
                    IAsyncResult res = com.BeginExecuteReader(CommandBehavior.CloseConnection);

                    SqlDataReader rd = com.EndExecuteReader(res);
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            JournalItem jr = new JournalItem()
                            {
                                ID = Convert.ToInt32(rd["JournalID"]),
                                Author = rd["Authors"].ToString(),
                                Name = rd["JournalName"].ToString(),
                                DateOfRec = Convert.ToDateTime(rd["DateOfRecipient"]),
                                OwnBoard = new Board()
                                {
                                    ID = Convert.ToInt32(rd["BoardID"]),
                                    Name = rd["BoardName"].ToString()
                                },
                                OwnAdressee = new Adressee(
                                    Convert.ToInt32(rd["AdresseeID"]), rd["AdresseeName"].ToString()),
                                Note = rd["Notes"].ToString(),
                                RC = new Recipirnt(Convert.ToInt32(rd["RecipientID"]), rd["RecipientName"].ToString())
                            };
                            journal.Add(jr);
                        }

                    }

                    return journal;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public static bool UpdateJourlanItem(JournalItem jr)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = $"UPDATE Journal SET Authors = @author, " +
                        $"JournalName = @jrname," +
                        $"BoardID = @brid," +
                        $"AdresseeID = @adrid," +
                        $"Notes = @note WHERE JournalID = @id";

                    com.Parameters.AddWithValue("@author", jr.Author);
                    com.Parameters.AddWithValue("@jrname", jr.Name);
                    com.Parameters.AddWithValue("@brid", jr.OwnBoard.ID);
                    com.Parameters.AddWithValue("@adrid", jr.OwnAdressee.ID);
                    com.Parameters.AddWithValue("@note", jr.Note);
                    com.Parameters.AddWithValue("@id", jr.ID);

                    int count = com.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
