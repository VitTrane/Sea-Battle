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
        public double Wins { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}