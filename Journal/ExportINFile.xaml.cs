using Journal.src;
using Microsoft.Win32;
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
    /// Interaction logic for ExportINFile.xaml
    /// </summary>
    public partial class ExportINFile : Window
    {
        ObservableCollection<JournalItem> jritem;
        public ExportINFile()
        {
            InitializeComponent();
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
        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            
            if(datefor.SelectedDate != null || datesfrom.SelectedDate != null)
            {
                DateTime dtmp = (DateTime)datefor.SelectedDate;
                DateTime dtfor = dtmp.AddDays(1);
                DateTime dtfrom = (DateTime)datesfrom.SelectedDate;
                jritem = new ObservableCollection<JournalItem>(IndboxDB.SearchAllJournal(dtfrom, dtfor));
                journalList.ItemsSource = jritem;
                
            }
            if (jritem.Count > 0)
                exportBtn.IsEnabled = true;
        }

        private void mwindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dl = new SaveFileDialog();
            dl.DefaultExt = ".xlsx";
            dl.Filter = "Excel File (*.xlsx)|*.xlsx";
            if (dl.ShowDialog() == true)
            {
                CreateEXCEL crt = new CreateEXCEL(dl.FileName, jritem);
            }
        }
    }
}
