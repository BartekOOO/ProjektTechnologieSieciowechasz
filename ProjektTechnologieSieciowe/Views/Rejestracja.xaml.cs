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

namespace ProjektTechnologieSieciowe.Views
{
    /// <summary>
    /// Interaction logic for Rejestracja.xaml
    /// </summary>
    public partial class Rejestracja : Window
    {
        public Rejestracja()
        {
            InitializeComponent();
        }

        private void Rejestracja_Click(object sender, RoutedEventArgs e)
        {
            string login, password, email;
            login = LoginTextBox.Text;
            password = HasloPasswordBox.Password;
            email = AdresEmailTextBox.Text;

            if(String.IsNullOrEmpty(login)||
                String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Wymagane pola nie zostały uzupełnione");
                return;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
