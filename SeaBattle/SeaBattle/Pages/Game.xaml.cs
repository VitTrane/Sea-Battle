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

            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.fourdeck], fourship);
            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.threedeck], threeship);
            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.twodeck], twoship);
            _shipToggles.Toggles.Add(_shipToggles.CountsOfDecks[DeckCount.onedeck], oneship);
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
