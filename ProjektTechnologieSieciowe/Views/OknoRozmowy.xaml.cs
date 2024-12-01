using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
        public int ReceiverId { get; set; } = 0;
        public int SenderId { get; set; } = 0;
        public Client Client { get; set; }

        public OknoRozmowy()
        {
            InitializeComponent();
            Messages = new ObservableCollection<Message>();

            LoadData();
            
        }

        public async Task LoadData()
        {
            this.DataContext = this;
            Client = new Client();
            Client.Connect(Config.host, Config.port);

            List<Message> messages = new List<Message>();

            RequestData<Message> reqmsg = new RequestData<Message>();
            reqmsg.Data = new Message();
            reqmsg.Method = TechnologieSiecioweLibrary.Enums.Method.List;
            reqmsg.Token = Config.token;

            Client.SendData(reqmsg.GetJSONBody());

            var result = await Client.WaitForResponseAsync();

            ResponseData<List<Message>> resp = new ResponseData<List<Message>>();
            resp.ReadDataFromJSON(result);

            foreach (Message message in resp.Data)
            {
                messages.Add(message);
            }

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
