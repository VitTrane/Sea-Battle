using SeaBattle.BattleShipServiceCallback;
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
            usernameTextBox.MaxLength = Validator.MAX_LENGTH_USERNAME;
            passwordBox.MaxLength = Validator.MAX_LENGTH_PASSWORD;
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
            if (!Validator.IsValidPassword(passwordBox.Password))
            {
                errorMessageTextBlock.Text = "Пароль должен иметь от 6 до 16 символов";
                return;
            }

            try
            {
                ClientManager.Instance.CreateClient();
                ClientManager.Instance.Callback.SetHandler<RegisterResponse>(RegisterPlayer);
                ClientManager.Instance.Callback.SetHandler<AuthorizeResponse>(Autorize);
                var registerrequest = new RegisterRequest() { Login = usernameTextBox.Text, Email = emailTextBox.Text, Password = passwordBox.Password };
                ClientManager.Instance.Client.Register(registerrequest);
                
            }
            catch (Exception)
            {
                //TODO: добавить выбрасывание popup c сообщением об ошибке
            }
        }

        /// <summary>
        /// Регистрирует пользователя
        /// </summary>
        /// <param name="sender">Объект вызвавший мето</param>
        /// <param name="e">Данные события для обработки</param>
        private void RegisterPlayer(object sender, ResponseEventArgs e)
        {
            RegisterResponse response = e.Response as RegisterResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    var AutorizeRequest = new AuthorizeRequest() { Login = usernameTextBox.Text, Password = passwordBox.Password };
                    ClientManager.Instance.Client.Authorize(AutorizeRequest);
                }
                else
                {
                    errorMessageTextBlock.Text = response.Error;
                }
            }
            ClientManager.Instance.Callback.RemoveHandler<RegisterResponse>();

        }

        /// <summary>
        /// Аторизует пользователя
        /// </summary>
        /// <param name="sender">Объект вызвавший метод</param>
        /// <param name="e">Данные события для обработки</param>
        private void Autorize(object sender, ResponseEventArgs e)
        {
            AuthorizeResponse response = e.Response as AuthorizeResponse;
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
            ClientManager.Instance.Callback.RemoveHandler<AuthorizeResponse>();
        }
    }
}
