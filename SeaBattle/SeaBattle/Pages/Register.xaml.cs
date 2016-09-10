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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
            ClientManager.Instance.SubscribeToResponse<RegisterResponse>(RegisterPlayer);
            ClientManager.Instance.SubscribeToResponse<AuthorizeRequest>(RegisterPlayer);
        }

        private void backTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            Switcher.SwitchPage(new Login());
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validator.IsValidUsername(usernameTextBox.Text))
            {
                errorMessageTextBlock.Text = "Такой логин не подходит";
                return;
            }
            if (!Validator.IsValidMailAddress(emailTextBox.Text))
            {
                errorMessageTextBlock.Text = "Вы ввели email неправильного формата";
                return;
            }
            if (!Validator.IsValidPassword(passwordBox.Password, 6))
            {
                errorMessageTextBlock.Text = "Пароль должен иметь от 6 до 16 символов";
                return;
            }

            try
            {                
                var registerrequest = new RegisterRequest() { Login = usernameTextBox.Text, Email = emailTextBox.Text, Password = passwordBox.Password };
                ClientManager.Instance.Client.Register(registerrequest);
                
            }
            catch (Exception)
            {
                //TODO: добавить выбрасывание popup c сообщением об ошибке
            }
        }

        private void RegisterPlayer()
        {
            RegisterResponse response = ClientManager.Instance.GetResponse<RegisterResponse>();
            if (response.IsSuccess)
            {
                var AutorizeRequest = new AuthorizeRequest() { Login = usernameTextBox.Text, Password = passwordBox.Password };
                ClientManager.Instance.Client.Authorize(AutorizeRequest);                
            }
            else
            {
                errorMessageTextBlock.Text = response.Error;
            }
            ClientManager.Instance.UnsubscribeFromResponse<RegisterResponse>();
        }

        private void Autorize()
        {
            AuthorizeResponse response = ClientManager.Instance.GetResponse<AuthorizeResponse>();
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    Switcher.SwitchPage(new MainMenu());
                }
                else
                {
                    errorMessageTextBlock.Text = response.Error;
                }
            }
            ClientManager.Instance.UnsubscribeFromResponse<AuthorizeResponse>();
        }
    }
}
