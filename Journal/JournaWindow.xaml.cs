using Journal.src;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Journal
{
    /// <summary>
    /// Interaction logic for JournaWindow.xaml
    /// </summary>
    /// 
    public delegate void InsertJourlanCollectin(JournalItem jr);
    public partial class JournaWindow : Window
    {
        private ObservableCollection<JournalItem> Jurnal;
        private ObservableCollection<Adressee> adresses;
        private ObservableCollection<Board> board;

        public static InsertJourlanCollectin insertjournal;
        public Recipirnt rcp { get; set; }
        public JournaWindow()
        {
            InitializeComponent();
            Jurnal = new ObservableCollection<JournalItem>();
            insertjournal = injournal;
        }

        private void injournal(JournalItem jr)
        {
            Jurnal.Insert(0, jr);
        }

        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void maximizeWindow_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                return;
            }
            WindowState = WindowState.Maximized;
        }

        private void minimzeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private ObservableCollection<JournalItem> getjournal(DateTime dt)
        {
            return new ObservableCollection<JournalItem>(IndboxDB.GetAllJournalItem(dt));
        }
        private void mwindow_Loaded(object sender, RoutedEventArgs e)
        {
            datesearch.SelectedDate = DateTime.Now;
            Jurnal = getjournal((DateTime)datesearch.SelectedDate);
            journalList.ItemsSource = Jurnal;

            adresses = new ObservableCollection<Adressee>(IndboxDB.GetAllAdressee());
            addressCombo.ItemsSource = adresses;

            board = new ObservableCollection<Board>(IndboxDB.GetBoard());
            boardCombo.ItemsSource = board;
        }

        private void datesearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datesearch.SelectedDate != null)
            {
                Jurnal = getjournal((DateTime)datesearch.SelectedDate);
            }
            else
            {
                Jurnal = getjournal(DateTime.Now);
            }
            journalList.ItemsSource = Jurnal;
        }

        

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            EditItem.Jr = (JournalItem)btn.Tag;
            EditItem editwind = new EditItem()
            {
                board = board,
                adresses = adresses
            };
            editwind.Owner = this;
            editwind.ShowDialog();
           
        }

        private void fullInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            FullInfo.Jr = (JournalItem)btn.Tag;
            FullInfo fl = new FullInfo();
            
            fl.ShowDialog();
        }

        private void ListItemBtn_Click(object sender, RoutedEventArgs e)
        {
            datesearch.SelectedDate = DateTime.Now;
            Jurnal = getjournal((DateTime)datesearch.SelectedDate);
            journalList.ItemsSource = Jurnal;
        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
            AddItem adtm = new AddItem() { adresses = adresses, board = board,recpient = rcp };
            adtm.Owner = this;
            adtm.ShowDialog();
            
        }
        private void ClearData()
        {
            authorTxbx.Clear();
            names.Clear();
            addressCombo.SelectedItem = null;
            boardCombo.SelectedItem = null;
            datesearch.SelectedDate = DateTime.Now;
        }
        private void exprotfile_Click(object sender, RoutedEventArgs e)
        {
            ExportINFile exp = new ExportINFile();
            exp.ShowDialog();
        }

        private void authorTxbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (authorTxbx.Text != string.Empty && Jurnal.Count > 0)
            {
                IEnumerable<JournalItem> authors = Jurnal.Where(n => n.Author.Contains(authorTxbx.Text));

                journalList.ItemsSource = authors;
            }
            else
                journalList.ItemsSource = Jurnal;
            
        }

        private void names_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (names.Text != string.Empty && Jurnal.Count > 0)
            {
                IEnumerable<JournalItem> journalnames = Jurnal.Where(n => n.Name.Contains(names.Text));
                journalList.ItemsSource = journalnames;

            }
            else
                journalList.ItemsSource = Jurnal;
        }

        private void addressCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cm = (ComboBox)sender;
            if (cm.SelectedItem != null && Jurnal.Count > 0)
            {
                if(cm.SelectedItem is Adressee)
                {
                    IEnumerable<JournalItem> jr = Jurnal.Where(n => n.OwnAdressee.ID == ((Adressee)cm.SelectedItem).ID);
                    journalList.ItemsSource = jr;
                }
                else if(cm.SelectedItem is Board)
                {
                    IEnumerable<JournalItem> jr = Jurnal.Where(n => n.OwnBoard.ID == ((Board)cm.SelectedItem).ID);
                    journalList.ItemsSource = jr;
                }
                
                
            }
            else
                journalList.ItemsSource = Jurnal;
        }
    }
}
