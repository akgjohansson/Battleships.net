using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.net.DataBase.Builder
{
    public class Game
    {
        public virtual Guid GameId { get; set; }
        public virtual int Rows { get; set; }
        public virtual int Columns { get; set; }
        public virtual DateTime StartedAt { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}