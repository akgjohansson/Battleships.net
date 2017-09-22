using Battleships.net.DataBase.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Battleships.net.Models
{
    public class Message
    {
        public bool HitShip { get; set; }
        //public bool SunkTheShip { get;}
        public Ship SunkenShip { get; set; }
        public bool GameOver { get; set; }
        public bool SunkTheShip
        {
            get {
                if (SunkenShip == null)
                    return false;
                else
                    return true;
            }
        }

    }
}