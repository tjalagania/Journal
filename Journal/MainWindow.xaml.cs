using Journal.src;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Journal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.Inbox = File.ReadLines("ConnectionString.txt");
        }

        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void close_button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Close();
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if(recipientCombo.SelectedItem == null)
            {
                Helper.CheckValidText("", "მიუთითეთ მიმღები");
                return;
            }
            JournaWindow wn = new JournaWindow() { rcp = (Recipirnt)recipientCombo.SelectedItem };
            wn.Show();
            this.Close();
        }

        private void mwindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            List<Recipirnt> recipients = IndboxDB.GetAllRecipients();

            recipientCombo.ItemsSource = recipients;
           
        }
    }
}
