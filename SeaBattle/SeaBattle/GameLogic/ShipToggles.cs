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
        public Dictionary<byte, byte> CountsOfShips { get; private set; }

        /// <summary>
        /// Словарь радибатонов, для управления ими
        /// </summary>
        public Dictionary<byte, RadioButton> Toggles { get; set; }
        
        public ShipToggles()
        {
            CountsOfDecks = new Dictionary<DeckCount, byte>(4);
            CountsOfShips = new Dictionary<byte, byte>(4);
            Toggles = new Dictionary<byte, RadioButton>();

            CountsOfDecks.Add(DeckCount.onedeck, 1);
            CountsOfDecks.Add(DeckCount.twodeck, 2);
            CountsOfDecks.Add(DeckCount.threedeck, 3);
            CountsOfDecks.Add(DeckCount.fourdeck, 4);

            CountsOfShips.Add(1, 4);
            CountsOfShips.Add(2, 3);
            CountsOfShips.Add(3, 2);
            CountsOfShips.Add(4, 1);
        }
        
        /// <summary>
        /// Отнимает единицу от количества допустимых кораблей 
        /// </summary>
        /// <param name="shipDecks">Количество палуб в корабле</param>
        public void dicShipCount(byte shipDecks)
        {            
            CountsOfShips[shipDecks]--;
            if(CountsOfShips[shipDecks]==0)
            {
                var toggle = Toggles[shipDecks];
                toggle.IsEnabled = false;
                toggle.IsChecked = false;
            }
        }
    }    
}
