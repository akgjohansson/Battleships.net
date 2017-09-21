using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    class Ship
    {
        public virtual Guid Id { get; set; }
        public virtual System.String StartGrid { get; set; }
        public virtual System.String Orientation { get; set; }
        public virtual System.Int32 Length { get; set; }
        public virtual System.Boolean IsSunk { get; set; }
        public virtual Player Player { get; set; }
    }
}