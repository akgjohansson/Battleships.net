using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    class Game
    {
        public virtual Guid Id { get; set; }
        public virtual System.DateTime StartedAt { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}