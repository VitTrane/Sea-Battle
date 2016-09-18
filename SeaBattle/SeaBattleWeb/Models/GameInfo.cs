using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaBattle.Models
{
    public class GameInfo
    {
        public string Creator { get; set;}
        public string CreationDate{ get; set;}
        public GameInfo() { }
        public GameInfo(string name, string date)
        {
            this.CreationDate = date;
            this.Creator = name;
        }
    }
}