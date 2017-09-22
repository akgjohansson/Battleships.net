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
using Battleships.net.Models;


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

        public Game CreateGame(string name1 , string name2 , int rows , int columns)
        {
            this.CleanUp();
            Game game = CreateGameMethod( rows ,  columns);
            Builder.Player player1 = AddPlayer(AddAndOrLoadUser(name1) , true , game);
            Builder.Player player2 = AddPlayer(AddAndOrLoadUser(name2) , false , game);
            SetupGrid(player1, player2, rows, columns);
            return game;
        }

        

        public Game CreateGame(string name1 , int rows , int columns)
        {
            this.CleanUp();
            Game game = CreateGameMethod( rows ,  columns);
            Builder.Player player1 = AddPlayer(AddAndOrLoadUser(name1), true , game);
            game.Players.Add(player1);
            
            return game;
        }

        private Game CreateGameMethod(int rows , int columns)
        {
            Game game = new Game
            {
                StartedAt = DateTime.Now,
                Rows = rows,
                Columns = columns
            };
            Session.Save(game);
            return game;
        }

        public void Let2ndPlayerJoin(Game game, string name2)
        {
            DobbyDBHelper.DobbyDBHelper dobby = new DobbyDBHelper.DobbyDBHelper();
            List<Grid> grid = dobby.GetGrid();
            dobby.FreeDobby();
            
            Builder.Player player2 = AddPlayer(AddAndOrLoadUser(name2), false, game);
            SetupGrid(game.Players.ToList()[0], player2, game.Rows, game.Columns);

        }

        public Builder.Player AddPlayer(UserPerson user , bool isHost , Game game)
        {
            Builder.Player player = new Builder.Player
            {
                UserPerson = user,
                IsHost = isHost,
                Game = game
            };
            Session.Save(player);
            return player;
        }

        public void SetupGrid(Builder.Player player1 , Builder.Player player2 ,int rows, int columns)
        {
            SetThisGridUp(rows, columns , player1);
            SetThisGridUp(rows, columns, player2);
        }

        private void SetThisGridUp(int rows, int columns , Builder.Player player)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Grid grid = new Grid
                    {
                        Coordinate = $"{((char)(i + 65))}{j + 1}",
                        IsHit = false,
                        Player = player
                    };
                    Session.Save(grid);
                    Session.Flush();
                }
            }
        }

        /// <summary>
        /// NB! No check if it can fit here is available here, it must be done previously!
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="coordinates"></param>
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



        public void CleanUp()
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

    }
}