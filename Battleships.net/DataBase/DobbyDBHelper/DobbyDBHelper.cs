using Battleships.net.Services;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Battleships.net.DataBase.Builder;
using NHibernate.Linq;
using Battleships.net.Models;

namespace Battleships.net.DataBase.DobbyDBHelper
{
    public class DobbyDBHelper : HandleDataBase
    {
        public ISession Session { get; set; }
        public DobbyDBHelper()
        {
            Session = DbService.OpenSession();
        }

        public void FreeDobby()
        {
            DbService.CloseSession(Session);
        }

        public void SinkTheShip(Ship ship)
        {
            Session.Save(ship);
        }

        public List<Grid> GetShipCoords(Grid grid)
        {
            return Session.Query<Grid>().Where(g => g.Ship == grid.Ship).ToList();
        }

        public List<Grid> GetGrid()
        {
            return Session.Query<Grid>().ToList();
        }
        public Dictionary<string, Grid> GetGridDictionary()
        {
            List<Grid> grid = GetGrid();
            Dictionary<string, Grid> gridDict = new Dictionary<string, Grid>();
            foreach (Grid item in grid)
            {
                gridDict.Add(item.Coordinate, item);
            }
            return gridDict;
        }

        public Grid GetGrid(string coordinate , Builder.Player player)
        {
            return Session.Query<Grid>().Where(c => (c.Coordinate.ToLower() == coordinate.ToLower()) && (c.Player == player)).Single();
        }
        public void UpdateGridToDB(List<Grid> gridList)
        {
            foreach (Grid grid in gridList)
            {
                Session.Save(grid);
            }
        }

        public Message DropBomb(Grid grid)
        {
            Grid thisGrid = Session.Query<Grid>().Where(c => c.Coordinate == grid.Coordinate).Single();
            thisGrid.IsHit = true;
            Session.Save(thisGrid);

            Message message = new Message();
            List<Ship> ships = Session.Query<Ship>().Where(s => s.Grid == thisGrid).ToList();
            message.HitShip = (ships.Count != 0);
            
            return message;
        }

    }
}