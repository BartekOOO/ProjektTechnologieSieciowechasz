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
    /// <summary>
    /// Interaction logic for GlowneOkno.xaml
    /// </summary>
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

           

            MessageBox.Show($"Zalogowano jako: {userDetails.Data.UserName}, {userDetails.Data.Email}");
        }
    }
}
