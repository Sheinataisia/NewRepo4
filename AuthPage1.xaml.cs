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

namespace Sheina41
{
    /// <summary>
    /// Логика взаимодействия для AuthPage1.xaml
    /// </summary>
    public partial class AuthPage1 : Page
    {
        public AuthPage1()
        {
            InitializeComponent();
        }

        private void VxodBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxLogin.Text;
            string password = TBoxPassword.Text;
            if(login == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }
            User user = шеина41Entities1.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if(user != null)
            {
                if (CapchaPanel.Visibility == Visibility.Visible && TBoxCaptcha.Text != _currentCaptchaCode)
                {
                    MessageBox.Show("Введена неверная капча");
                    LoginBtn.IsEnabled = false;
                    TBoxLogin.Text = "";
                    TBoxPassword.Text = "";
                    TBoxCaptcha.Text = "";
                    await Task.Delay(10000);
                    LoginBtn.IsEnabled = true;
                }
                else
                {
                    Manager1.MainFrame.Navigate(new ProductPage(user));
                    TBoxLogin.Text = "";
                    TBoxPassword.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
                if (CapchaPanel.Visibility == Visibility.Visible)
                {
                    LoginBtn.IsEnabled = false;
                    TBoxLogin.Text = "";
                    TBoxPassword.Text = "";
                    TBoxCaptcha.Text = "";
                    await Task.Delay(10000);
                    LoginBtn.IsEnabled = true;
                }
                else
                {
                    CapchaPanel.Visibility = Visibility.Visible;
                    TBoxCaptcha.Visibility = Visibility.Visible;
                    TButtonGo.Visibility = Visibility.Visible;
                    TBoxLogin.Text = "";
                    TBoxPassword.Text = "";
                    TBoxCaptcha.Text = "";
                    RandomCatcha();
                }
            }
        }
        string _currentCaptchaCode = "";

        private void RandomCatcha()
        {
            string chars = "ABCDEFGHJKLMNPQSTUVWXY23456789";
            Random random = new Random();

            string code = "";
            for (int i = 0; i < 4; i++)
            {
                code += chars[random.Next(chars.Length)];
            }
            _currentCaptchaCode = code;
            capthaOneWord.Text = code[0].ToString();
            capthaOTwoWord.Text = code[1].ToString();
            capthaThreeWord.Text = code[2].ToString();
            capthaFourWord.Text = code[3].ToString();
        }
        
        private void TBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TButtonGo_Click(object sender, RoutedEventArgs e)
        {
            if (TBoxCaptcha.Text != _currentCaptchaCode)
            {
                MessageBox.Show("Неверная капча! Попробуйте еще раз.");
                RandomCatcha();
                TBoxCaptcha.Text = "";
            }
            else
            {
                MessageBox.Show("капча введена верно Попробуйте еще раз.");
            }
        }
    }
}
