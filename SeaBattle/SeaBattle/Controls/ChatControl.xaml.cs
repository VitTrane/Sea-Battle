﻿using SeaBattle.BattleShipServiceCallback;
using SeaBattle.GameService;
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

namespace SeaBattle.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        public string ChatText
        {
            get { return (string)GetValue(ChatTextProperty); }
            set { SetValue(ChatTextProperty, value); }
        }

        public static readonly DependencyProperty ChatTextProperty =
            DependencyProperty.Register("ChatText", typeof(string), typeof(ChatControl), new UIPropertyMetadata(null));

        public string ChatMessageText
        {
            get { return (string)GetValue(ChatMessageTextProperty); }
            set { SetValue(ChatMessageTextProperty, value); }
        }

        public static readonly DependencyProperty ChatMessageTextProperty =
            DependencyProperty.Register("ChatMessageText", typeof(string), typeof(ChatControl), new UIPropertyMetadata(null));


        public ChatControl()
        {
            InitializeComponent();
            ClientManager.Instance.Callback.SetHandler<RecieveMessageResponse>(GetMessage);
        }

        private void GetMessage(object sender, ResponseEventArgs e)
        {
            RecieveMessageResponse response = e.Response as RecieveMessageResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    GetMessage(response.Message);
                }
            }
        }

        private void GetMessage(string message) 
        {
            ChatText += Environment.NewLine + "[" + DateTime.Now.ToString() + "] " + message;
            chatTextBox.ScrollToEnd();
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendMessageRequest request = new SendMessageRequest() { Message = ChatMessageText };
                Managers.ClientManager.Instance.Client.SendMessage(request);
            }
            catch (Exception)
            {
            }
            
        }

        public void CloseChat() 
        {
            ClientManager.Instance.Callback.RemoveHandler<RecieveMessageResponse>();
        }
    }
}
