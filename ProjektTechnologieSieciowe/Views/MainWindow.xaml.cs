using ProjektTechnologieSieciowe.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ProjektTechnologieSieciowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LogowanieButton_Click(object sender, RoutedEventArgs e)
        {
            string login, password;
            login = LoginTextBox.Text;
            password = HasloPasswordBox.Password;

            User user = new User(0,login,password,"");

            RequestData<User> requestData = new RequestData<User>();
            requestData.Data = user;
            requestData.Method = TechnologieSiecioweLibrary.Enums.Method.Login;

            Client client = new Client();
            client.Connect(Config.host,Config.port);

            client.SendData(requestData.GetJSONBody());

            var tokenResponse = await client.WaitForResponseAsync();

            ResponseData<Token> responseData = new ResponseData<Token>();
            responseData.ReadDataFromJSON(tokenResponse);

            if (responseData.ResponseCode == TechnologieSiecioweLibrary.Enums.ResponseCode.OK)
            {
                Config.token = responseData.Data;
                GlowneOkno okno = new GlowneOkno();
                okno.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Błędne dane logowania");
            }
            client.Disconnect();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rejestracja rejestracjaOkno = new Rejestracja();
            rejestracjaOkno.Show();
            this.Close();
        }
    }
}
