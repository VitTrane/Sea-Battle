using SeaBattle.BattleShipServiceCallback;
using SeaBattle.GameService;
using SeaBattle.Helpers;
using SeaBattle.Managers;
using System;
using System.Collections.Generic;
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
                errorMessageTextBlock.Text = string.Format("Пароль должен иметь от {0} до {1} символов", 
                    Validator.MIN_LENGTH_PASSWORD,
                    Validator.MAX_LENGTH_PASSWORD);
                return;
            }

            try
            {
                ClientManager.Instance.CreateClient();
                RegisterPlayer();
                
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("Ошибка!: превышенно время ожидания");
                ClientManager.Instance.Dispose();
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Ошибка!: Проблемы соединения с серверром");
                ClientManager.Instance.Dispose();
            }  
        }

        /// <summary>
        /// Регистрирует пользователя
        /// </summary>
        private void RegisterPlayer()
        {
            var registerRequest = new RegisterRequest() { Login = usernameTextBox.Text, Email = emailTextBox.Text, Password = passwordBox.Password };
            RegisterResponse response = ClientManager.Instance.Client.Register(registerRequest);            
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    Autorize();
                    
                }
                else
                {
                    errorMessageTextBlock.Text = response.Error;
                }
            }
        }

        /// <summary>
        /// Аторизует пользователя
        /// </summary>
        private void Autorize()
        {
            var AutorizeRequest = new AuthorizeRequest() { Login = usernameTextBox.Text, Password = passwordBox.Password };
             AuthorizeResponse response = ClientManager.Instance.Client.Authorize(AutorizeRequest);
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
