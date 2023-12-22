using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
using static PriceDynamics.MainWindow;

namespace PriceDynamics
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Window_Activate(object sender, EventArgs e)
        {
            Pas.Focus();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            switch (Pas.Password)
            {
                case "123":
                    Account.Enter = "operator";
                    Close();
                    MessageBox.Show("Добрый день! Вы вошли как пользователь.");
                    break;
                case "1234567890":
                    Account.Enter = "admin";
                    Close();
                    MessageBox.Show("Здравствуйте. Вы вошли под учётной записью администратора.");
                    break;
                default:
                    MessageBox.Show("Пароль неверный");
                    Pas.Clear();
                    break;

            }
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Close();
        }
    }
}
