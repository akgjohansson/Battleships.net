using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.net.DataBase.Builder
{
    class User
    {
        public virtual Guid UserId { get; set; }
        public virtual System.String NickName { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}