using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.src
{
    public class JournalItem
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public DateTime DateOfRec { get; set; }
        public Board OwnBoard { get; set; }
        public Adressee OwnAdressee { get; set; }
        public string Note { get; set; }
        public Recipirnt RC {get;set;}
    }
}
