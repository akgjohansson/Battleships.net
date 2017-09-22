using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Battleships.net.DataBase.Builder;
using Battleships.net.DataBase.Setup;

namespace Battleships.net.Models
{
    public class GameBoard
    {
        public Game Game { get; set; }
        public Dictionary<string,Grid> Grid { get; set; }

        public bool DropBomb(string coordinate)
        {
            if (Grid[coordinate.ToUpper()].IsHit)
            {
                return false;
            }
            else
            {
                DropThisBomb(coordinate.ToUpper());
                return true;
            }
        }

        public bool PlaceShip(string startCoordinate , string orientation , int length)
        {
            Ship ship = new Ship
            {
                StartGrid = startCoordinate,
                Orientation = orientation,
                Length = length
            };
            string[] coordinates = GetShipCoords(ship);
            if (CanShipFitHere(coordinates))
            {
                Setup setup = new Setup();
                setup.PlaceShipHere(ship , coordinates);
                setup.CloseSession();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private string[] GetShipCoords(Ship ship)
        {
            string[] coordinates = new string[ship.Length];
            int stepDown = 0;
            int stepRight = 0;
            switch (ship.Orientation)
            {
                case "V":
                    stepDown = 1;
                    break;
                case "H":
                    stepRight = 1;
                    break;
                case "DU":
                    stepDown = -1;
                    stepRight = 1;
                    break;
                case "DD":
                    stepDown = 1;
                    stepRight = 1;
                    break;
                default:
                    break;
            }
            char x = ship.StartGrid[0];
            int y = (int)ship.StartGrid[1];
            for (int i = 0; i < ship.Length; i++)
            {
                if (i != 0)
                {
                    if (stepDown == -1 && x == 'A')
                        return null;
                    x = (char)((int)x + stepRight);
                    y += stepDown;
                }
                coordinates[i] = $"{x}{y}";
            }
            return coordinates;
        }

        private bool CanShipFitHere(string[] coordinates)
        {
            foreach (string coordinate in coordinates)
            {
                if (Grid[coordinate].Ship != null)
                    return false;
            }
            return true;

        }

        private void DropThisBomb(string v)
        {
            throw new NotImplementedException();
        }

        private void UpdateGrid()
        {
            
            Setup.UpdateGridToDB(Grid.Values.ToList());
        }
    }
}