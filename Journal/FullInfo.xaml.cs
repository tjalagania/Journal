using Journal.src;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for FullInfo.xaml
    /// </summary>
    public partial class FullInfo : Window
    {
        public static JournalItem Jr { get; set; }
        public FullInfo()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog prnt = new PrintDialog();
            prnt.PrintTicket.PageOrientation = PageOrientation.Landscape;
            if (prnt.ShowDialog() == true)
            {
                
                FlowDocument document = parentDocContainer.Document;
                
                FlowDocument doc = document;
                
                
                prnt.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator,"Print Document");
            }
        }
    }
}
