using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaBattle.Models
{
    public class TopPlayerItem
    {
        public string UserName { get; set; }
        public int GameCount { get; set; }
        public double WinRate { get; set; }
        public string RegistrationDate { get; set; }
        public TopPlayerItem() { }
        public TopPlayerItem(string name, int games, double wins,string reg ) 
        {
            this.UserName = name;
            this.GameCount = games;
            this.WinRate = wins;
            this.RegistrationDate = reg;
        }
    }
}