using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaBattle.Models
{
    public class LastGamesItem
    {
        public string FirstPlayer { get; set; }
        public string SecondPlayer { get; set; }
        public string Winner { get; set; }  
        public string DateStart { get; set; }
        public string Duration { get; set; }
        public LastGamesItem() { }
        public LastGamesItem(string fp, string sp, string w, string st, string d)
        {
            this.FirstPlayer = fp;
            this.SecondPlayer = sp;
            this.Winner = w;
            this.DateStart = st;
            this.Duration = d;
        }
    }
}