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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechnologieSiecioweLibrary.Models;

namespace ProjektTechnologieSieciowe.Views
{
    /// <summary>
    /// Interaction logic for OknoRozmowy.xaml
    /// </summary>
    public partial class OknoRozmowy : UserControl
    {
        public ObservableCollection<Message> Messages { get; set; }
        public OknoRozmowy()
        {
            InitializeComponent();
            Messages = new ObservableCollection<Message>();

            this.DataContext = this;

            Messages.Add(new Message() { SenderName = "Bartek", MessageText = "Halo halo"});
            Messages.Add(new Message() { SenderName = "Mikołaj", MessageText = "Gówno" });
            Messages.Add(new Message() { SenderName = "Bartek", MessageText = "Dupa" });
            Messages.Add(new Message() { SenderName = "Bartek", MessageText = "Dupa" });
            Messages.Add(new Message() { SenderName = "Bartek", MessageText = "Dupa" });
            Messages.Add(new Message() { SenderName = "Bartek", MessageText = "Dupa" });
            Messages.Add(new Message() { SenderName = "Bartek", MessageText = "Dupa" });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(MessageTextBox.Text))
            {
                Message newMessage = new Message() { SenderName = "Bartek", MessageText = MessageTextBox.Text };
                Messages.Add(newMessage);
                ChatListBox.ScrollIntoView(newMessage);
                MessageTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Wiadomość nie może być pusta");
            }
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.Enter)
            {
                Button_Click(null,null);
            }
        }
    }
}
