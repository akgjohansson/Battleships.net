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
        public List<Grid> Grid { get; set; }
    }
}