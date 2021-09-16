using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.src
{
    public class Recipirnt
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                try
                {
                    _id = value;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private string _name;
        public string Name 
        { 
            get 
            {
                return _name;
            } 
            set 
            {
                if (value != string.Empty)
                {
                    _name = value;
                }
                else
                    throw new Exception("სახელი არ შეიძლება იყოს ცარიელი");
            } 
        }

        public Recipirnt() { }
        public Recipirnt(int id,string name)
        {
            ID = id;
            Name = name;
        }
    }
}
