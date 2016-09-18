using SeaBattle.BattleShipServiceCallback;
using SeaBattle.GameService;
using SeaBattle.Helpers;
using SeaBattle.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
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
            usernameTextBox.MaxLength = Validator.MAX_LENGTH_USERNAME;
            passwordBox.MaxLength = Validator.MAX_LENGTH_PASSWORD;
        }

        private void registerTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.SwitchPage(new Register());
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validator.IsValidUsername(usernameTextBox.Text) || !Validator.IsValidPassword(passwordBox.Password))
                {
                    errorMessageTextBlock.Text = "Неверный логин или пароль";
                    return;
                }

                Autorize();
            }
            catch (TimeoutException ex)
            {
                string message = string.Format("{0},\n {1} \n {2}", "Превышенно время ожидания",
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);

                MessageBox.Show("Ошибка!: превышенно время ожидания");
                ClientManager.Instance.Dispose();
            }
            catch (CommunicationException ex)
            {
                string message = string.Format("{0},\n {1} \n {2}", "Проблемы соединения с серверро",
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);

                MessageBox.Show("Ошибка!: Проблемы соединения с серверром");
                ClientManager.Instance.Dispose();
            }
            catch (Exception ex)
            {
                string message = string.Format("{0} \n {1},\n {2}", ex.Message,
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);
                ClientManager.Instance.Dispose();
            }
        }

        /// <summary>
        /// Аторизует пользователя
        /// </summary>
        private void Autorize()
        {
            ClientManager.Instance.CreateClient();
            var authorizeRequest = new AuthorizeRequest() { Login = usernameTextBox.Text, Password = passwordBox.Password };
            AuthorizeResponse response = ClientManager.Instance.Client.Authorize(authorizeRequest);  
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    ClientManager.Instance.PlayerNickname = usernameTextBox.Text;
                    ClientManager.Instance.ClientId = response.ClientId;
                    Switcher.SwitchPage(new MainMenu());
                }
                else
                {
                    errorMessageTextBlock.Text = response.Error;
                }
            }
        }
    }
}
