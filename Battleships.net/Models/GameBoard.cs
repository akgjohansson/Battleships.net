using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Battleships.net.DataBase.Builder;

namespace Battleships.net.Models
{
    public class GameBoard
    {
        public Game Game { get; set; }
        public Dictionary<string,Grid> Grid { get; set; }

        public bool DropBomb(string coordinate)
        {
            if (Grid[coordinate.ToUpperInvariant].IsHit)
            {
                return false;
            }
            else
            {
                DropThisBomb(coordinate.ToUpper());
                return true;
            }
        }


    }
}