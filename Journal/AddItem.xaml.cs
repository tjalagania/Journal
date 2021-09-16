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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {

        public ObservableCollection<Board> board { get; set; }
        public ObservableCollection<Adressee> adresses { get; set; }
        public Recipirnt recpient { get; set; }
        public AddItem()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            addItemAddressesCombo.ItemsSource = adresses;
            addItemBoardCombo.ItemsSource = board;
        }

        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            string author;
            string name;
            Board tempBoard;
            Adressee tmpAddress;
            Recipirnt tmpRec;
            if (!Helper.CheckValidText(authorAddItemTxt.Text,"ავტორი არ შეიძლება იყოს ცარიელი"))
            {
                return;
            }
            if(!Helper.CheckValidText(addItemNameTxt.Text,"დასახელება არ შეიძლება იყოს ცარიელი"))
            {
                return;
            }
            if(addItemAddressesCombo.SelectedItem == null)
            {
                Helper.CheckValidText("", "აირჩიეთ ადრესატი");
                return;
            }
            if(addItemBoardCombo.SelectedItem == null)
            {
                Helper.CheckValidText("", "მიუთითეთ კოლეგია");
                return;
            }
            author = authorAddItemTxt.Text.Trim();
            name = addItemNameTxt.Text.Trim();
            tempBoard = (Board)addItemBoardCombo.SelectedItem;
            tmpAddress = (Adressee)addItemAddressesCombo.SelectedItem;

            JournalItem tmpjourlan = new JournalItem()
            {
                Author = author,
                Name = name,
                OwnBoard = tempBoard,
                OwnAdressee = tmpAddress,
                RC = recpient,
                Note = addItemNoteTxt.Text.Trim(),
                DateOfRec = DateTime.Now
            };
            bool tag = IndboxDB.AddJournalItem(tmpjourlan);
            if (tag)
            {
                MessageBox.Show("ჩანაწერი დაემატა წარმატებით", "ინფორმაცია", MessageBoxButton.OK, MessageBoxImage.Information);
                JournaWindow.insertjournal?.Invoke(tmpjourlan);
                authorAddItemTxt.Clear();
                addItemNameTxt.Clear();
                addItemBoardCombo.SelectedItem = null;
                addItemAddressesCombo.SelectedItem = null;
                addItemNoteTxt.Clear();

            }
            else
            {
                MessageBox.Show("ვერმოხერხდა ჩანაწერის შენახვა", 
                    "ინფორმაცია", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
        }
    }
}
