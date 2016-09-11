using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SeaBattle.GameLogic
{
    class ShipToggles
    {
        public static ShipToggles instance;

        public Deck NowDecker = Deck.onedeck;
        
        public ShipOrientation NowShipOrientation = ShipOrientation.Horizontal;

        public Dictionary<RadioButton,Deck> DeckTogglesDictionary = new Dictionary<RadioButton,Deck>();

        public Dictionary<RadioButton, ShipOrientation> OrientationTogglesDictionary = new Dictionary<RadioButton, ShipOrientation>();

        private Dictionary<Deck, byte> countsOfDecks = new Dictionary<Deck, byte>(4);

        public ShipToggles()
        {
            countsOfDecks.Add(Deck.onedeck, 4);
            countsOfDecks.Add(Deck.twodeck, 3);
            countsOfDecks.Add(Deck.threedeck, 2);
            countsOfDecks.Add(Deck.fourdeck, 1);
            NowDecker = Deck.fourdeck;
            NowShipOrientation = ShipOrientation.Horizontal;
            instance = this;
        }
    }

    enum Deck
    {
        fourdeck,
        threedeck,
        twodeck,
        onedeck
    }
    
    enum ShipOrientation
    {
        Horizontal,
        Vertical
    }
}
