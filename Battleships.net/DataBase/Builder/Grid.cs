using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Battleships.net.DataBase.Builder
{
    public class Grid
    {
        public virtual Guid GridId { get; set; }
        public virtual string Coordinate { get; set; }
        public virtual Ship Ship { get; set; }
        public virtual bool IsHit { get; set; }
    }
}