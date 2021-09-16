using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.src
{
    public class Adressee
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Adressee() { }
        public Adressee(string name)
        {
            Name = name;
        }
        public Adressee(int id,string name):this(name)
        {
            ID = id;
        }
    }
}
