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

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        private const int MAP_WIDTH = 10;
        private const int MAP_HEIGHT = 10;

        private ShipToggles _shipToggles;
        private Sea _seaPlayer;
        private Sea _seaOpponent;


        public Game()
        {
            InitializeComponent();
            fourship.IsChecked = true;
            HorizontalOrientation.IsChecked = true;

            _shipToggles = new ShipToggles();
            _seaPlayer = new Sea(playerSquare);
            _seaOpponent = new Sea(opponentSquare);

            _shipToggles.toggles.Add(_shipToggles.CountsOfDecks[DeckCount.fourdeck], fourship);
            _shipToggles.toggles.Add(_shipToggles.CountsOfDecks[DeckCount.threedeck], threeship);
            _shipToggles.toggles.Add(_shipToggles.CountsOfDecks[DeckCount.twodeck], twoship);
            _shipToggles.toggles.Add(_shipToggles.CountsOfDecks[DeckCount.onedeck], oneship);
        }

        private void labelBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.SwitchPage(new MainMenu());
        }

        private void seaPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                var element = (UIElement)e.Source;
                int row = Grid.GetRow(element);
                int column = Grid.GetColumn(element);
                if (_shipToggles.CountsOfShips[GetDeckCount()] > 0)
                {
                    if (CanShipStayThere(GetDeckCount(), row, column, GetOrientation()))
                    {

                        _seaPlayer.CreateShip(column - 1, row - 1, GetDeckCount(), GetOrientation());
                        _shipToggles.dicShipCount(GetDeckCount());
                        messageLabel.Content = "";
                    }
                    else
                        messageLabel.Content = "Нельзя!";
                }
                else
                    messageLabel.Content = "Превышено количество кораблей";
            }
        }
        /// <summary>
        /// Провеярем можно ли здесь  установить корабль
        /// </summary>
        /// <param name="deckCount">Количество палуб</param>
        /// <param name="i">Координаты по строке</param>
        /// <param name="j">Координаты по столбцу</param>
        /// <param name="orientation">Ориентация корабля</param>
        /// <returns></returns>
        private bool CanShipStayThere(byte deckCount, int i, int j, ShipOrientation orientation)
        {
            if (i < 1 && j < 1)
                return false;
            
            if (orientation == ShipOrientation.Horisontal)
            {
                if (j + deckCount - 1 > 10) // можно ли приватные константы из класса Sea сделать публичными и проверять на них?
                    return false;
            }
            else
            {
                if (j + deckCount - 1 > 10) // аналогичный вопрос
                    return false;
            }
            
            if (orientation == ShipOrientation.Horisontal)
            {
                if (j+deckCount<11)
                {
                    //Проверяем правый край корабля
                    if (IsShipThere(i,j + deckCount))
                        return false;
                    //Проверяем правый верхний угол корабля
                    if (i-1>0)
                        if (IsShipThere(i-1,j+deckCount))
                            return false;
                    //Проверяем правый нижний угол корабля
                    if (i+1<11)
                        if (IsShipThere(i+1,j+deckCount))
                            return false;
                }
                if (j-1>0)
                {
                    //Проверяем левую сторону корабля
                    if (IsShipThere(i,j - 1))
                        return false;
                    //Проверяем левый верхний угол корабля
                    if (i - 1 > 0)
                        if (IsShipThere(i - 1,j - 1))
                            return false;
                    //Проверяем левый нижний угол корабля
                    if (i + 1 < 11)
                        if (IsShipThere(i + 1,j - 1))
                            return false;
                }

                //Проверяем верхнюю грань корабля
                if (i-1>0)
                {
                    for (int k = j; k < j + deckCount; k++)
                        if (IsShipThere(i-1, k))
                            return false;
                }
                //Проверяем нижнюю грань корабля
                if (i+1<11)
                {
                    for (int k = j; k < j + deckCount; k++)
                        if (IsShipThere(i + 1,k))
                            return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Проверяет наличие корабля в клетке
        /// </summary>
        /// <param name="j">Координата колонки (от 1 до 10)</param>
        /// <param name="i">Координата строки (от 1 до 10)</param>
        /// <returns></returns>
        private bool IsShipThere(int i, int j)
        {
            if (_seaPlayer.Map[i - 1, j - 1].State == FieldState.Ship)
                return true;
            else 
                return false;
        }

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

        private ShipOrientation GetOrientation()
        {
            if ((bool)HorizontalOrientation.IsChecked)
                return ShipOrientation.Horisontal;

            if ((bool)VerticalOrientation.IsChecked)
                return ShipOrientation.Vertical;

            return ShipOrientation.Horisontal;
        }
    }
}
