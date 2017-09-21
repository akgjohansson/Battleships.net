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
        private User AddAndOrLoadUser(string name)
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

        public Game CreateGame(string name1 , string name2)
        {

            Player player1 = AddPlayer(AddAndOrLoadUser(name1));
            Player player2 = AddPlayer(AddAndOrLoadUser(name2));
            Game game = new Game
            {
                Players = new List<Player>
                {
                    player1 , player2
                },
                StartedAt = DateTime.Now
            };
            Session.Save(game);
            return game;
        }

        public Player AddPlayer(User user)
        {
            Player player = new Player
            {
                User = user,
            };
            return player;
        }


        public void SetupGrid(int rows, int columns)
        {

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Grid grid = new Grid
                    {
                        Coordinate = $"{((char)(i + 65))}{j + 1}",
                        IsHit = false
                    };
                    Session.Save(grid);
                }
            }
            
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

        public void Cleanup()
        {

        }

        private bool DoesPlayerExist(string name, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }



    }
}