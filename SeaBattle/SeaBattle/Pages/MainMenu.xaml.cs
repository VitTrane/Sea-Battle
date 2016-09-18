﻿using SeaBattle.BattleShipServiceCallback;
using SeaBattle.GameService;
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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void backTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ClientManager.Instance.Client.LeaveGame();
                ClientManager.Instance.Client.Logout();
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login()); 
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("Ошибка!: превышенно время ожидания");
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login()); 
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Ошибка!: Проблемы соединения с серверром");
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login()); 
            }
            catch (Exception ex)
            {
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientManager.Instance.Callback.SetHandler<CreateGameResponse>(CreateGame);
                CreateGameRequest request = new CreateGameRequest();
                ClientManager.Instance.Client.CreateGame(request); 
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("Ошибка!: превышенно время ожидания");
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login()); 
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Ошибка!: Проблемы соединения с серверром");
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login()); 
            }                         
        }

        private void connectionGameButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.SwitchPage(new FindGame());
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.SwitchPage(new Statistic());
        }

        private void CreateGame(object sender, ResponseEventArgs e)
        {
            CreateGameResponse response = e.Response as CreateGameResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    Switcher.SwitchPage(new Game(true));
                }
                else 
                {
                    errorMessageTextBlock.Text = response.Error;
                }
            }
        }
    }
}
