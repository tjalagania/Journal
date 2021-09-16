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
    /// Interaction logic for EditItem.xaml
    /// </summary>
    /// 
   
    public partial class EditItem : Window
    {
        public ObservableCollection<Board> board { get; set; }
        public ObservableCollection<Adressee>adresses { get; set; }
        public static JournalItem Jr { get; set; }
        public EditItem()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EditItemAddressesCombo.ItemsSource = adresses;
            EditItemBoardCombo.ItemsSource = board;

            for(int i = 0; i < board.Count; i++)
            {
                if(board[i].ID == Jr.OwnBoard.ID)
                {
                    EditItemBoardCombo.SelectedIndex = i;
                    break;
                }
            }
            for(int i = 0; i < adresses.Count; i++)
            {
                if(adresses[i].ID == Jr.OwnAdressee.ID)
                {
                    EditItemAddressesCombo.SelectedIndex = i;
                    break;
                }
            }
        }

        private void editItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Helper.CheckValidText(authorEditItemTxt.Text, "ავტორი არ შეიძლება იყოს ცარიელი"))
            {
                return;
            }
            if (!Helper.CheckValidText(EditItemNameTxt.Text, "დასახელება არ შეიძლება იყოს ცარიელი"))
            {
                return;
            }
            if (EditItemAddressesCombo.SelectedItem == null)
            {
                Helper.CheckValidText("", "აირჩიეთ ადრესატი");
                return;
            }
            if (EditItemBoardCombo.SelectedItem == null)
            {
                Helper.CheckValidText("", "მიუთითეთ კოლეგია");
                return;
            }

            JournalItem tmpjr = new JournalItem()
            {
                ID = Jr.ID,
                Author = authorEditItemTxt.Text.Trim(),
                Name = EditItemNameTxt.Text.Trim(),
                OwnAdressee = (Adressee)EditItemAddressesCombo.SelectedItem,
                OwnBoard = (Board)EditItemBoardCombo.SelectedItem,
                Note = editItemNoteTxt.Text.Trim()
                
            };
            bool tag = IndboxDB.UpdateJourlanItem(tmpjr);
            if (tag)
            {
                MessageBox.Show("ჩანაწერი დარედაქტირდა წარმატებით");
            }
            else
            {
                MessageBox.Show("ვერ მოხერხდა ჩანაწერის რედაქტირება", "გაფრხილება",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning
                        );
            }
        }
    }
}
