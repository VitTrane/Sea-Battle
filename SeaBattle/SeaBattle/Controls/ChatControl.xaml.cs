using SeaBattle.BattleShipServiceCallback;
using SeaBattle.GameService;
using SeaBattle.Managers;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using SeaBattle.Helpers;
using System.Windows.Input;
using SeaBattle.Pages;

namespace SeaBattle.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {       
        public ChatControl()
        {
            InitializeComponent();
            messageTextBox.MaxLength = Validator.MAX_LENGTH_MESSAGE;

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;
            ClientManager.Instance.Callback.SetHandler<RecieveMessageResponse>(GetMessage);
        }

        private void GetMessage(object sender, ResponseEventArgs e)
        {
            RecieveMessageResponse response = e.Response as RecieveMessageResponse;
            if (response != null)
            {
                if (response.Message != null)
                {
                    GetMessage(response.Name, response.Message);
                }
            }
        }

        private void GetMessage(string nickname,string message) 
        {
            chatTextBox.Text += Environment.NewLine + string.Format("[{0}] {1}: {2}", DateTime.Now.ToString(), nickname, message);
            chatTextBox.ScrollToEnd();
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(messageTextBox.Text))
                {
                    chatTextBox.Text += Environment.NewLine + string.Format("[{0}] {1}: {2}", DateTime.Now.ToString(), "Вы", messageTextBox.Text);                    
                    SendMessageRequest request = new SendMessageRequest() { Message = messageTextBox.Text };
                    messageTextBox.Text = "";
                    Managers.ClientManager.Instance.Client.SendMessage(request);
                    chatTextBox.ScrollToEnd();
                }
            }
            catch (TimeoutException ex)
            {
                string message = string.Format("{0},\n {1} \n {2}", "Превышенно время ожидания",
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);

                MessageBox.Show("Ошибка!: превышенно время ожидания");
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login());
            }
            catch (CommunicationException ex)
            {
                string message = string.Format("{0},\n {1} \n {2}", "Проблемы соединения с серверро",
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);

                MessageBox.Show("Ошибка!: Проблемы соединения с серверром");
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login());
            } 
            catch(Exception ex)
            {
                string message = string.Format("{0},\n {1}",
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);

                MessageBox.Show("Ошибка!: Проблемы соединения с серверром");
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login());
            }
        }

        public void CloseChat() 
        {
            ClientManager.Instance.Callback.RemoveHandler<RecieveMessageResponse>();
        }

        private void messageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                sendMessageButton_Click(null, null);
            }
        }
    }
}
