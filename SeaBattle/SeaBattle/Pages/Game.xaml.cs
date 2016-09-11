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
using SeaBattle.GameLogic;

namespace SeaBattle.Pages
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : UserControl
    {

        public Game()
        {
            InitializeComponent();
            ShipToggles toggles = new ShipToggles();

            toggles.DeckTogglesDictionary.Add(fourship, Deck.fourdeck);
            toggles.DeckTogglesDictionary.Add(threeship, Deck.threedeck);
            toggles.DeckTogglesDictionary.Add(twoship, Deck.twodeck);
            toggles.DeckTogglesDictionary.Add(oneship, Deck.onedeck);

            toggles.OrientationTogglesDictionary.Add(VerticalOrientation, ShipOrientation.Vertical);
            toggles.OrientationTogglesDictionary.Add(HorizontalOrientation, ShipOrientation.Horizontal);

            Sea playerSquare = new Sea(seaPlayer);
            Sea opponentSquare = new Sea(seaOpponent);
        }

        private void labelBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Switcher.SwitchPage(new MainMenu());
        }
    }
}
