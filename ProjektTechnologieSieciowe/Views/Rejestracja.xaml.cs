using System;
using System.Collections.Generic;
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
using TechnologieSiecioweLibrary.Models;

namespace ProjektTechnologieSieciowe.Views
{
    public partial class Rejestracja : Window
    {
        public Rejestracja()
        {
            InitializeComponent();
            LoginTextBox.Focus();
        }

        private async void Rejestracja_Click(object sender, RoutedEventArgs e)
        {
            string login, password, email, country;
            login = LoginTextBox.Text;
            password = HasloPasswordBox.Password;
            email = AdresEmailTextBox.Text;
            country = KrajTextBox.Text;

            if(String.IsNullOrEmpty(login)||
                String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Wymagane pola nie zostały uzupełnione");
                return;
            }

            RequestData<User> requestNewUser = new RequestData<User>();
            requestNewUser.Data = new User() { UserName = login, Email = email, Password = password, Country = country };
            requestNewUser.Method = TechnologieSiecioweLibrary.Enums.Method.Post;

            Client client = new Client();
            client.Connect(Config.host, Config.port);

            client.SendData(requestNewUser.GetJSONBody());

            var result = await client.WaitForResponseAsync();

            ResponseData<User> responseUser = new ResponseData<User>();
            responseUser.ReadDataFromJSON(result);

            if (responseUser.ResponseCode == TechnologieSiecioweLibrary.Enums.ResponseCode.CREATED)
            {
                MessageBox.Show("Pomyślnie dodano nowego użytkownika");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else if (responseUser.ResponseCode == TechnologieSiecioweLibrary.Enums.ResponseCode.USER_ALREADY_EXISTS)
            {
                MessageBox.Show("Użytkownik o podanym loginie już istnieje");
            }
            else
            {
                MessageBox.Show("Nastąpił nieoczekiwany błąd");
            }

            client.Disconnect();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
