using ProjektTechnologieSieciowe.Views;
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

namespace ProjektTechnologieSieciowe
{
    public partial class GlowneOkno : Window
    {
        public GlowneOkno()
        {
            InitializeComponent();
            LoadUserData();
        }

        public async Task LoadUserData()
        {
            RequestData<User> userInfo = new RequestData<User>();
            userInfo.Token = Config.token;
            userInfo.Data = new User();
            userInfo.Method = TechnologieSiecioweLibrary.Enums.Method.Get;

            Client client = new Client();
            client.Connect(Config.host, Config.port);

            client.SendData(userInfo.GetJSONBody());

            var resuet = await client.WaitForResponseAsync();

            ResponseData<User> userDetails = new ResponseData<User>();
            userDetails.ReadDataFromJSON(resuet);

            DaneMenuItem.Header = $"Zalogowano jako: {userDetails.Data.UserName}";

            client.Disconnect();
        }

        private void NowaKonwersacjaClick(object sender, RoutedEventArgs e)
        {
            OknoRozmowy konwersacja = new OknoRozmowy();
            MainGrid.Children.Clear();
            MainGrid.Children.Add(konwersacja);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ZakonczKonwersacjeClick(object sender, RoutedEventArgs e)
        {

        }

        private void WylogujClick(object sender, RoutedEventArgs e)
        {
            MainWindow logowanie = new MainWindow();
            logowanie.Show();
            this.Close();
            Config.token = null;
        }

        private void WlaczOgolnaKonwersacjeClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
