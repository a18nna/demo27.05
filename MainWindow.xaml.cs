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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo27._05
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DemoContext())
            {
                var user = db.users.FirstOrDefault(u => u.login == loginTextBox.Text && u.password == passwordTextBox.Text);
                if (user != null)
                {
                    OpenProductList(user);
                }
                else 
                    MessageBox.Show("Неверный логин или пароль!\t\nПроверьте правильность введенных данных и повторите попытку.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenProductList(User user)
        {
            var productList = new ProductListWindow(user);
            productList.Show();
            this.Close();
        }

        private void loginAsGuest_Click(object sender, RoutedEventArgs e)
        {
            var guestUser = new User { full_name = "Гость", role = new Role { role1 = "гость" } };
            OpenProductList(guestUser);
        }
    }
}
