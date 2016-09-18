﻿using System;
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
using SeaBattle.GameService;
using System.Collections.ObjectModel;
using SeaBattle.BattleShipServiceCallback;
using SeaBattle.Managers;
using System.Windows.Controls.Primitives;

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для FindGame.xaml
    /// </summary>
    public partial class FindGame : UserControl
    {
        ObservableCollection<DTOAwaitingGame> _games;

        public ObservableCollection<DTOAwaitingGame> Games
        {
            get { return _games; }
            set { _games = value; }
        }

        public FindGame()
        {
            InitializeComponent();
            _games = new ObservableCollection<DTOAwaitingGame>();

            gamesListView.ItemsSource = _games;
            ClientManager.Instance.Callback.SetHandler<GetListGamesResponse>(GetAvailableGames);

            try
            {
                ClientManager.Instance.Client.GetListAvailableGames();
            }
            catch (Exception ex)
            {
                string message = string.Format("{0} \n {1},\n {2}", ex.Message,
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);

                ClientManager.Instance.Dispose();
            }           
        }

        private void backTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClientManager.Instance.Callback.RemoveHandler<GetListGamesResponse>();
            Switcher.SwitchPage(new MainMenu());
        }

        private void gamesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            DTOAwaitingGame game = (DTOAwaitingGame)gamesListView.ItemContainerGenerator.ItemFromContainer(dep);

            ClientManager.Instance.Callback.SetHandler<CurentGameResponse>(GetConnectToGames);
            var request = new ConnectToGameRequest() { GameId = game.GameId};
            ClientManager.Instance.Client.ConnectToGame(request);            
        }

        /// <summary>
        /// Возвращает список полученных игр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetAvailableGames(object sender, ResponseEventArgs e)
        {
            if (this != null)
            {
                GetListGamesResponse response = e.Response as GetListGamesResponse;
                if (response != null)
                {
                    List<DTOAwaitingGame> newGames = response.Games.Where(g => !Games.Contains(g)).ToList();
                    foreach (var game in newGames)
                    {
                        _games.Add(game);
                    }
                }
            }
        }

        /// <summary>
        /// Поключаеться к игре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetConnectToGames(object sender, ResponseEventArgs e)
        {
            CurentGameResponse response = e.Response as CurentGameResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    Switcher.SwitchPage(new Game(false));
                }
                else
                {
                    MessageBox.Show(response.Error);
                }
            }
            ClientManager.Instance.Callback.RemoveHandler<CurentGameResponse>();
        }     
    }
}
