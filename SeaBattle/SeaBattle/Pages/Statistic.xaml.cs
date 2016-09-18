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
using System.ComponentModel;

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : UserControl
    {
        private ListSortDirection _sortDirection;
        private GridViewColumnHeader _sortColumn;

        private ObservableCollection<DTOFullGameInfo> _lastGames;
        private ObservableCollection<DTOFullUserInfo> _topPlayers;

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
                if (response.Games != null)
                {
                    var lastGames = response.Games;
                    foreach (var game in lastGames)
                    {
                        _lastGames.Add(game);
                    }
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
            catch (Exception ex)
            {
                string message = string.Format("{0} \n {1},\n {2}", ex.Message,
                    ex.ToString(), ex.StackTrace);
                ClientManager.Instance.Logger.WriteLineError(message);
                ClientManager.Instance.Dispose();
                Switcher.SwitchPage(new Login());
            }
        }

        /// <summary>
        /// Сортирует список рейтинга игроков по выбранному столбцу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topPlayersListView_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            if (column == null)
            {
                return;
            }

            if (_sortColumn == column)
            {
                _sortDirection = _sortDirection == ListSortDirection.Ascending ?
                                                   ListSortDirection.Descending :
                                                   ListSortDirection.Ascending;
            }
            else
            {
                if (_sortColumn != null)
                {
                    _sortColumn.Column.HeaderTemplate = null;
                    _sortColumn.Column.Width = _sortColumn.ActualWidth - 20;
                }

                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
                column.Column.Width = column.ActualWidth + 20;
            }

            if (_sortDirection == ListSortDirection.Ascending)
            {
                column.Column.HeaderTemplate =
                                   Resources["ArrowUp"] as DataTemplate;
            }
            else
            {
                column.Column.HeaderTemplate =
                                    Resources["ArrowDown"] as DataTemplate;
            }

            string header = string.Empty;
            Binding b = _sortColumn.Column.DisplayMemberBinding as Binding;
            if (b != null)
            {
                header = b.Path.Path;
            }

            ICollectionView resultDataView = CollectionViewSource.GetDefaultView(
                                                          topPlayersListView.ItemsSource);
            resultDataView.SortDescriptions.Clear();
            resultDataView.SortDescriptions.Add(
                               new SortDescription(header, _sortDirection));
        }
    }
}
