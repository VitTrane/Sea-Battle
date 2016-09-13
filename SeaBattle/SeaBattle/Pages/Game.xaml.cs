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
                if (row > 0 && column > 0) 
                {
                    _seaPlayer.CreateShip(column - 1, row - 1, GetDeckCount(), GetOrientation());
                }
            }
        }

        private int GetDeckCount()
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
