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
using SeaBattle.GameService;
using SeaBattle.GameLogic;
using SeaBattle.Managers;
using SeaBattle.BattleShipServiceCallback;

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        private ShipToggles _shipToggles;
        private Sea _seaPlayer;
        private Sea _seaOpponent;
        private StateGame _stateGame;

        public Game(bool isHost)
        {
            InitializeComponent();
            fourship.IsChecked = true;
            HorizontalOrientation.IsChecked = true;            

            _shipToggles = new ShipToggles();
            _seaPlayer = new Sea(playerSquare);
            _seaOpponent = new Sea(opponentSquare);

            AddTooggles();

            if (isHost)
            {
                sendReadyButton.IsEnabled = false;
                SetEnableChat(false);
                _stateGame = StateGame.OpponentWaiting;
                ClientManager.Instance.Callback.SetHandler<CurentGameResponse>(GetOpponent);
                textInfoGameTextBlock.Text = "Ожидание подключения соперника...";
            }
            else 
            {
                SetEnableChat(true);
                _stateGame = StateGame.PreparationGame;
                sendReadyButton.IsEnabled = true;
                textInfoGameTextBlock.Text = "Подготовка к игре...";
            }

            ClientManager.Instance.Callback.SetHandler<SendOpponentIsReadyResponse>(ResultOpponentSendReady);
            ClientManager.Instance.Callback.SetHandler<StartGameResponse>(StartGame);
            ClientManager.Instance.Callback.SetHandler<EndGameResponse>(EndGameGame);
            ClientManager.Instance.Callback.SetHandler<ShotResponse>(ResultFire);
        }

        private void opponentSquare_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_stateGame == StateGame.Game)
            {
                var element = (UIElement)e.Source;
                int row = Grid.GetRow(element);
                int column = Grid.GetColumn(element);

                if (row > 0 && column > 0)
                {
                    XYCoordinate position = new XYCoordinate(){X=column,Y=row};
                    Shot shot = new Shot() { XyCoordinate = position };
                    ClientManager.Instance.Client.DoShot(shot);
                }
            }
        }   

        private void ResultFire(object sender, ResponseEventArgs e)
        {
            ShotResponse response = e.Response as ShotResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    Shot s = response.CurrentShot;
                    //TODO доделать выстрел
                }
                else
                {
                }
            } 
        }

        private void EndGameGame(object sender, ResponseEventArgs e)
        {
            EndGameResponse response = e.Response as EndGameResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    _stateGame = StateGame.Finished;
                    textInfoGameTextBlock.Text = "Бой";
                    //TODO: показать победителя
                    ClientManager.Instance.Callback.RemoveHandler<EndGameResponse>();
                }
                else
                {
                    //TODO: добавить popup с ошибками
                }
            } 
        }

        private void StartGame(object sender, ResponseEventArgs e)
        {
            StartGameResponse response = e.Response as StartGameResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    _stateGame = StateGame.Game;
                    textInfoGameTextBlock.Text = "Бой";
                    ClientManager.Instance.Callback.RemoveHandler<StartGameResponse>();
                }
                else
                {
                    //TODO: добавить popup с ошибками
                }
            }            
        }

        private void ResultOpponentSendReady(object sender, ResponseEventArgs e)
        {
            SendOpponentIsReadyResponse response = e.Response as SendOpponentIsReadyResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    ClientManager.Instance.Callback.RemoveHandler<SendOpponentIsReadyResponse>();
                }
                else
                {
                    //TODO: добавить popup с ошибками
                }
            }           
        }        

        /// <summary>
        /// Получаем подключившегося соперника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetOpponent(object sender, ResponseEventArgs e)
        {
            CurentGameResponse response = e.Response as CurentGameResponse;
            if (response != null)
            {
                if (response.IsSuccess) 
                {
                   _stateGame = StateGame.PreparationGame;
                   ClientManager.Instance.Callback.RemoveHandler<CurentGameResponse>();
                }
            }
        }        

        private void labelBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.SwitchPage(new MainMenu());
        }

        private void seaPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_stateGame == StateGame.PreparationGame)
            {
                if (sender != null)
                {
                    var element = (UIElement)e.Source;
                    int row = Grid.GetRow(element);
                    int column = Grid.GetColumn(element);
                    if (_shipToggles.CountsOfShips[GetDeckCount()] > 0)
                    {
                        if (_seaPlayer.CanShipStayThere(GetDeckCount(), row, column, GetOrientation()))
                        {
                            _seaPlayer.CreateShip(column - 1, row - 1, GetDeckCount(), GetOrientation());
                            _shipToggles.dicShipCount(GetDeckCount());
                            messageTextBlock.Text = "";
                        }
                        else
                        {
                            if (row > 0 && column > 0)
                                messageTextBlock.Text = "В это место корабль поставить нельзя!";
                        }
                    }
                    else
                    {
                        messageTextBlock.Text = "Превышено количество кораблей";
                    }
                }
            }
        }        

        private void seaPlayer_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_stateGame == StateGame.PreparationGame)
            {
                if (sender != null)
                {
                    var element = (UIElement)e.Source;
                    int row = Grid.GetRow(element);
                    int column = Grid.GetColumn(element);

                    if (row > 0 && column > 0)
                    {
                        if (_seaPlayer.Map[row-1,column-1].Ship!=null)
                        {
                            _shipToggles.incShipCount((byte)_seaPlayer.Map[row - 1, column - 1].Ship.Decks.Length);
                            _seaPlayer.DeleteShip(_seaPlayer.Map[row - 1, column - 1].Ship);                            
                        }
                    }
                }
            }
        }

        private void sendReadyButton_Click(object sender, RoutedEventArgs e)
        {
            ClientManager.Instance.Callback.SetHandler<SendReadyResponse>(ResultSendReady);
            SendReadyRequest request = new SendReadyRequest() { ClientId = ClientManager.Instance.ClientId, Ships = _seaPlayer.Ships.ToArray()};
            ClientManager.Instance.Client.SendReady(request);
            sendButton.IsEnabled = true;
        }

        private void ResultSendReady(object sender, ResponseEventArgs e)
        {
            SendReadyResponse response = e.Response as SendReadyResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    ClientManager.Instance.Callback.RemoveHandler<SendReadyResponse>();
                }
                else
                {
                    //TODO: добавить popup с ошибками
                }
            }            
        }

        /// <summary>
        /// Возвращает количество палуб для выбраного корабля
        /// </summary>
        /// <returns></returns>
        private byte GetDeckCount()
        {
            if ((bool)fourship.IsChecked)
                return _shipToggles.CountsOfDecks[DeckCount.fourdeck];

            if ((bool)threeship.IsChecked)
                return _shipToggles.CountsOfDecks[DeckCount.threedeck];

            if ((bool)twoship.IsChecked)
                return _shipToggles.CountsOfDecks[DeckCount.twodeck];

            if ((bool)oneship.IsChecked)
                return _shipToggles.CountsOfDecks[DeckCount.onedeck];

            return _shipToggles.CountsOfDecks[DeckCount.fourdeck];
        }

        /// <summary>
        /// Возвращает выбраную ориентацию корабля
        /// </summary>
        /// <returns></returns>
        private ShipOrientation GetOrientation()
        {
            if ((bool)HorizontalOrientation.IsChecked)
                return ShipOrientation.Horisontal;

            if ((bool)VerticalOrientation.IsChecked)
                return ShipOrientation.Vertical;

            return ShipOrientation.Horisontal;
        }

        /// <summary>
        /// Делает чат доступным или не доступным
        /// </summary>
        /// <param name="isEnable">Доступен ли чат</param>
        private void SetEnableChat(bool isEnable)
        {
            Chat.IsEnabled = isEnable;

            if (isEnable)
                Chat.Visibility = Visibility.Visible;
            else
                Chat.Visibility = Visibility.Hidden;
        }

        private void AddTooggles()
        {
            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.fourdeck], fourship);
            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.threedeck], threeship);
            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.twodeck], twoship);
            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.onedeck], oneship);
        }          
    }
}
