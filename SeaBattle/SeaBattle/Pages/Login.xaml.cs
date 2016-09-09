using SeaBattle.GameService;
using SeaBattle.Helpers;
using SeaBattle.Managers;
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

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void registerTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.SwitchPage(new Register());
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validator.IsValidUsername(usernameTextBox.Text) || !Validator.IsValidPassword(passwordBox.Password, 6))
                {
                    errorMessageTextBlock.Text = "Неверный логин или пароль";
                    return;
                }

                var authorizeRequest = new AuthorizeRequest() { Login = usernameTextBox.Text, Password = passwordBox.Password };
                ClientManager.Instance.Client.Authorize(authorizeRequest);
                ClientManager.Instance.SubscribeToResponse<AuthorizeResponse>(Autorize);
            }
            catch (Exception ex)
            {
                //TODO: добавить выбрасывание popup c сообщением об ошибке
                string s = ex.Message;
            }
        }

        private void Autorize()
        {
            AuthorizeResponse res = ClientManager.Instance.GetResponse<AuthorizeResponse>();
            if (res != null)
            {
                if (res.IsSuccess)
                {
                    Switcher.SwitchPage(new MainMenu());
                }
                else
                {
                    errorMessageTextBlock.Text = res.Error;
                }
            }
            ClientManager.Instance.UnsubscribeFromResponse<AuthorizeResponse>();
        }
    }
}
