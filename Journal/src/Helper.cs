using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Journal.src
{
    public class Helper
    {
        public static bool CheckValidText(string text,string msg)
        {
            if(text != string.Empty)
            {
                return true;
            }
            MessageBox.Show(msg, "გაფრთხილება", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
       
    }
}
