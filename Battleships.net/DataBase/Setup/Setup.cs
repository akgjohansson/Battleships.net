using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Cfg;
using NHibernate;
using Battleships.net.DataBase.Builder;
using NHibernate.Linq;
using NHibernate.Criterion;
using Battleships.net.Services;

namespace Battleships.net.DataBase.Setup
{
    public class Setup : HandleDataBase
    {
        public ISession Session { get; set; }
        public Setup(ISession session)
        {
            Session = session;
        }
        public Setup()
        {
            Session = DbService.OpenSession();
        }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public UserPerson AddAndOrLoadUser(string name)

        {
            UserPerson user;
            if (!DoesPlayerExist(name))
            {
                user = new UserPerson
                {
                    NickName = name
                };
                Session.Save(user);
            }
            else
            {
                user = Session.Query<UserPerson>().Where(u => u.NickName == name).First(); 
            }
            return user;
        }

        public void CloseSession()
        {
            DbService.CloseSession(Session);
        }

        public Game CreateGame(string name1 , string name2)
        {
            Game game = CreateGameMethod();
            Player player1 = AddPlayer(AddAndOrLoadUser(name1) , true , game);
            Player player2 = AddPlayer(AddAndOrLoadUser(name2) , false , game);
            return game;
        }

        public Game CreateGame(string name1)
        {
            Game game = CreateGameMethod();
            Player player1 = AddPlayer(AddAndOrLoadUser(name1), true , game);
            return game;
        }

        private Game CreateGameMethod()
        {
            Game game = new Game
            {
                StartedAt = DateTime.Now
            };
            Session.Save(game);
            return game;
        }

        public void Let2ndPlayerJoin(Game game, string name2)
        {
            Player player2 = AddPlayer(AddAndOrLoadUser(name2), false, game);
        }

        public Player AddPlayer(UserPerson user , bool isHost , Game game)
        {
            Player player = new Player
            {
                UserPerson = user,
                IsHost = isHost,
                Game = game
            };
            Session.Save(player);
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
                    Session.Flush();
                }
            }
            
        }

        private bool CanShipFitHere(Ship ship)
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
        public void PlaceShipHere(Ship ship , string[] coordinates)
        {
            Session.Save(ship);
            foreach (string coordinate in coordinates)
            {
                Grid thisGrid = Session.Query<Grid>().Where(p => p.Coordinate == coordinate).Single();
                thisGrid.Ship = ship;
                Session.Save(thisGrid);
            }
            
        }

        public List<Grid> GetGrid()
        {
            return Session.Query<Grid>().ToList();
        }
        public List<Grid> GetGrid(string coordinate)
        {
            return Session.Query<Grid>().Where(c => c.Coordinate.ToLower() == coordinate.ToLower()).ToList();
        }

        public void Cleanup()
        {
            Session.Delete("from Grid");
            Session.Delete("from Ship");
            Session.Delete("from Player");
            Session.Delete("from Game");
        }

        private bool DoesPlayerExist(string name, bool caseSensitive = false)
        {
            var query = Session.Query<UserPerson>().Where(u => u.NickName.Equals(name)).ToList();
            
            if (query.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void UpdateGridToDB(List<Grid> gridList)
        {
            var session = DbService.OpenSession();
            foreach (Grid grid in gridList)
            {
                session.Save(grid);
            }
            DbService.CloseSession(session);
        }

    }
}