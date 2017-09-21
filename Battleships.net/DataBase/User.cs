using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    class User
    {
        public virtual Guid Id { get; set; }
        public virtual System.String NickName { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}