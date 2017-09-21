using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Cfg;
using NHibernate;
using Battleships.net.DataBase.Builder;
using NHibernate.Linq;

namespace Battleships.net.DataBase.Setup
{
    public class Setup : HandleDataBase
    {
        public ISession Session { get; set; }
        public Setup(ISession session)
        {
            
        }
        public User AddAndOrLoadUser(string name)
        {
            User user;
            if (!DoesPlayerExist(name))
            {
                user = new User
                {
                    NickName = name
                };
                Session.Save(user);
            }
            else
            {
                user = Session.Query<User>().Where(u => u.NickName == name).First(); 
            }
            return user;
        }

        public void SetupGrid(int rows, int columns)
        {
            throw new NotImplementedException();
        }

        private bool CanShipFitHere(string startGrid, string orientation, int length)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Tries to place ship. If successful, it returns true. If not, no action have been done on the database
        /// </summary>
        /// <param name="startGrid"></param>
        /// <param name="orientation"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public bool PlaceShipHere(string startGrid, string orientation, int length)
        {
            if (CanShipFitHere(startGrid, orientation, length))
            {
                throw new NotImplementedException();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoesPlayerExist(string name, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }


    }
}