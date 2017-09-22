using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.net.DataBase.Builder
{
    public class Player
    {
        public virtual Guid PlayerId { get; set; }
        public virtual System.Guid UserPersonId { get; set; }
        public virtual UserPerson UserPerson { get; set; }
        public virtual Game Game { get; set; }
        public virtual ICollection<Ship> Ship { get; set; }
        public virtual bool IsHost { get; set; }
        public virtual ICollection<Grid> Grid { get; set; }
    }
}