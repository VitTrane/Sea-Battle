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
        private bool _isMyTurn;

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
                _stateGame = StateGame.OpponentWaiting;
                SetEnableControls(_stateGame);
                ClientManager.Instance.Callback.SetHandler<CurentGameResponse>(GetOpponent);                
            }
            else 
            {                
                _stateGame = StateGame.PreparationGame;
                SetEnableControls(_stateGame);
                sendReadyButton.IsEnabled = true;
            }

            ClientManager.Instance.Callback.SetHandler<SendOpponentIsReadyResponse>(ResultOpponentSendReady);
            ClientManager.Instance.Callback.SetHandler<StartGameResponse>(StartGame);
            ClientManager.Instance.Callback.SetHandler<EndGameResponse>(EndGameGame);
            ClientManager.Instance.Callback.SetHandler<ShotResponse>(ResultFire);
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
                    SetEnableControls(_stateGame);
                    ClientManager.Instance.Callback.RemoveHandler<CurentGameResponse>();
                }
            }
        } 

        /// <summary>
        /// Выстреливает по полю противника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opponentSquare_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_isMyTurn)
            {
                if (_stateGame == StateGame.Game)
                {
                    var element = (UIElement)e.Source;
                    int row = Grid.GetRow(element);
                    int column = Grid.GetColumn(element);

                    if (row > 0 && column > 0)
                    {
                        if (_seaOpponent.Map[column - 1, row - 1].State == FieldState.Sea)
                        {
                            XYCoordinate position = new XYCoordinate() { X = column - 1, Y = row - 1 };
                            Shot shot = new Shot() { XyCoordinate = position };
                            ClientManager.Instance.Client.DoShot(shot);
                        }
                    }
                }
            }
        }   

        /// <summary>
        /// Обрабатывет результат выстрела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultFire(object sender, ResponseEventArgs e)
        {
            ShotResponse response = e.Response as ShotResponse;
            if (response != null)
            {
                Shot shot = response.CurrentShot;
                if (response.IsSuccess && _isMyTurn)
                {                    
                    _seaOpponent.SetShot(shot);
                    if(shot.Status == ShotStatus.Missed)
                        _isMyTurn = false;
                }
                else
                {
                    _seaPlayer.SetShot(shot);
                    if (shot.Status == ShotStatus.Missed)
                        _isMyTurn = true;
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

        private void labelBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Managers.ClientManager.Instance.Client.LeaveGame();
                Switcher.SwitchPage(new MainMenu());
            }
            catch (Exception)
            {
            }
            
        }

        /// <summary>
        /// Ставит на поле выбранный корабль
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Удаляет с поля поставленный корабль
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            _shipToggles.incShipCount((byte)_seaPlayer.Map[row - 1, column - 1].Ship.DeckCount);
                            _seaPlayer.DeleteShip(_seaPlayer.Map[row - 1, column - 1].Ship);                            
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Сообщает противнику, что вы готовы к бою
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_shipToggles.GetNotSetShipCount() > 0)
            {
                messageTextBlock.Text = "Раставленны не все корабли";
            }
            else
            {
                ClientManager.Instance.Callback.SetHandler<SendReadyResponse>(ResultSendReady);
                SendReadyRequest request = new SendReadyRequest() {Ships = _seaPlayer.Ships.ToArray() };
                ClientManager.Instance.Client.SendReady(request);
                _stateGame = StateGame.WaitReadyOpponent;
                SetEnableControls(_stateGame);
            }
        }

        /// <summary>
        /// Получает ответ от сервера, правильно ли раставлины корабли
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    _stateGame = StateGame.PreparationGame;
                    SetEnableControls(_stateGame);
                    messageTextBlock.Text = "Не правильно раставлены корабли";
                }
            }            
        }

        /// <summary>
        /// Начинает игру, если все игроки готовы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame(object sender, ResponseEventArgs e)
        {
            StartGameResponse response = e.Response as StartGameResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    _stateGame = StateGame.Game;
                    SetEnableControls(_stateGame);
                    ClientManager.Instance.Callback.RemoveHandler<StartGameResponse>();
                    if (response.NextShotUserId == ClientManager.Instance.ClientId)
                    {
                        _isMyTurn = true;
                    }
                    else
                    {
                        _isMyTurn = false;
                    }
                }
                else
                {
                    //TODO: добавить popup с ошибками
                }
            }
        }

        /// <summary>
        /// Выполняеться когда, ктото из игроков выигрывает
        /// </summary>
        /// <param name="sender">Объект, который вызвал этот метод</param>
        /// <param name="e">Дополнительные данные события</param>
        private void EndGameGame(object sender, ResponseEventArgs e)
        {
            EndGameResponse response = e.Response as EndGameResponse;
            if (response != null)
            {
                if (response.IsSuccess)
                {
                    _stateGame = StateGame.Finished;
                    textInfoGameTextBlock.Text = "Бой";
                    MessageBoxResult res = MessageBox.Show(String.Format("Победил игрок {0}", response.Winner.Login),"Игра завершена", MessageBoxButton.OK);
                    ClientManager.Instance.Callback.RemoveHandler<EndGameResponse>();
                    chatBox.CloseChat();
                    if (res == MessageBoxResult.OK)
                        Switcher.SwitchPage(new MainMenu());
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
        /// В зависимости от состояния игры, делает доступными нужные контролы или недоступными
        /// </summary>
        /// <param name="isEnable">Доступен ли чат</param>
        private void SetEnableControls(StateGame stateGame)
        { 
            if (stateGame == StateGame.PreparationGame)
            {
                textInfoGameTextBlock.Text = "Подготовка к игре...";
                Chat.Visibility = Visibility.Visible;
                Chat.IsEnabled = true;
                sendReadyButton.IsEnabled = true;
                sendReadyButton.Visibility = Visibility.Visible;
            }

            if (stateGame == StateGame.OpponentWaiting)
            {
                textInfoGameTextBlock.Text = "Ожидание подключения соперника...";
                Chat.Visibility = Visibility.Hidden;
                Chat.IsEnabled = false;
                sendReadyButton.IsEnabled = false;
                sendReadyButton.Visibility = Visibility.Hidden;
            }

            if (stateGame == StateGame.WaitReadyOpponent)
            {
                sendReadyButton.IsEnabled = false;
            }

            if(stateGame == StateGame.Game)
            {
                Ships.Visibility = Visibility.Hidden;
                Ships.IsEnabled = false;
                textInfoGameTextBlock.Text = "Бой...";
            }
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
