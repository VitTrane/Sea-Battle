using SeaBattle.GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SeaBattle.GameLogic
{
    enum DeckCount
    {
        fourdeck,
        threedeck,
        twodeck,
        onedeck
    }

    class ShipToggles
    {
        public Dictionary<DeckCount, byte> CountsOfDecks { get; private set; }
        public Dictionary<DeckCount, byte> CountsOfShips { get; private set; }

        public ShipToggles()
        {
            CountsOfDecks = new Dictionary<DeckCount, byte>(4);
            CountsOfShips = new Dictionary<DeckCount, byte>(4);

            CountsOfDecks.Add(DeckCount.onedeck, 1);
            CountsOfDecks.Add(DeckCount.twodeck, 2);
            CountsOfDecks.Add(DeckCount.threedeck, 3);
            CountsOfDecks.Add(DeckCount.fourdeck, 4);

            CountsOfShips.Add(DeckCount.onedeck, 4);
            CountsOfShips.Add(DeckCount.twodeck, 3);
            CountsOfShips.Add(DeckCount.threedeck, 2);
            CountsOfShips.Add(DeckCount.fourdeck, 1);
        }
    }    
}
