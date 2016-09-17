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
using SeaBattle.Managers;
using SeaBattle.GameService;
using System.Collections.ObjectModel;
using SeaBattle.BattleShipServiceCallback;
using System.ServiceModel;

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : UserControl
    {
        ObservableCollection<DTOFullGameInfo> _lastGames;
        ObservableCollection<DTOFullUserInfo> _topPlayers;

        public ObservableCollection<DTOFullUserInfo> TopPlayers
        {
            get { return _topPlayers; }
            set { _topPlayers = value; }
        }

        public ObservableCollection<DTOFullGameInfo> LastGames
        {
            get { return _lastGames; }
            set { _lastGames = value; }
        }

        public Statistic()
        {
            InitializeComponent();

            _lastGames = new ObservableCollection<DTOFullGameInfo>();
            _topPlayers = new ObservableCollection<DTOFullUserInfo>();
            topPlayersListView.ItemsSource = _topPlayers;
            lastGamesListView.ItemsSource = _lastGames;

            ClientManager.Instance.Callback.SetHandler<GetTopPlayersResponse>(GetTopPlayers);
            ClientManager.Instance.Callback.SetHandler<GetLastGamesResponse>(GetLastGames);

            RequestStatistics();
        }

        private void backTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClientManager.Instance.Callback.RemoveHandler<GetTopPlayersResponse>();
            ClientManager.Instance.Callback.RemoveHandler<GetLastGamesResponse>();
            Switcher.SwitchPage(new MainMenu());
        }

        private void GetTopPlayers(object sender, ResponseEventArgs e)
        {
            GetTopPlayersResponse response = e.Response as GetTopPlayersResponse;
            if (response != null)
            {
                var topPlayers = response.TopPlayers;
                foreach (var player in topPlayers)
                {
                    _topPlayers.Add(player);
                }
            }
        }

        private void GetLastGames(object sender, ResponseEventArgs e)
        {
            GetLastGamesResponse response = e.Response as GetLastGamesResponse;
            if (response != null)
            {
                var lastGames = response.Games;
                foreach (var game in lastGames)
                {
                    _lastGames.Add(game);
                }
            }
        }

        private void RequestStatistics() 
        {
            try
            {
                ClientManager.Instance.Client.GetTopPlayers();
                ClientManager.Instance.Client.GetLastGames();
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
    }
}
