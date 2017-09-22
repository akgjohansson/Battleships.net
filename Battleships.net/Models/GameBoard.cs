using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Battleships.net.DataBase.Builder;
using Battleships.net.DataBase.Setup;
using Battleships.net.DataBase.DobbyDBHelper;

namespace Battleships.net.Models
{
    public class GameBoard
    {
        public Game Game { get; set; }
        public Dictionary<string,Grid> Grid { get; set; }
        public Battleships.net.DataBase.Builder.Player ActivePlayer { get; set; }

        public void JoinGame(string player2)
        {
            Setup setup = new Setup();
            setup.Let2ndPlayerJoin(Game, player2);
            setup.CloseSession();
        } 
        public void SwitchActivePlayer()
        {
            DobbyDBHelper dobby = new DobbyDBHelper();
            this.ActivePlayer = dobby.SwitchPlayer(this.Game , this.ActivePlayer);

            dobby.FreeDobby();

        }
        public static GameBoard StartGame(string player1, string player2 , int rows , int columns)
        {
            GameBoard gameBoard = new GameBoard();
            Setup setup = new Setup();
            gameBoard.Game = setup.CreateGame(player1, player2 , rows , columns , gameBoard);
            
            setup.CloseSession();
            return gameBoard;
        }
        public static GameBoard StartGame(string player1, int rows, int columns)
        {
            GameBoard gameBoard = new GameBoard();
            Setup setup = new Setup();
            gameBoard.Game = setup.CreateGame(player1, rows, columns , gameBoard);
            return gameBoard;
        }
        public Message DropBomb(string coordinate)
        {
            if (Grid[coordinate.ToUpper()].IsHit)
            {
                return null;
            }
            else
            {
                Grid thisGrid = Grid[coordinate.ToUpper()];
                DobbyDBHelper dobby = new DobbyDBHelper();
                Message message = dobby.DropBomb(thisGrid);
                message.SunkenShip = IsShipSunk(thisGrid,dobby);
                message.GameOver = dobby.IsGameOver(thisGrid.Player);

                dobby.FreeDobby();

                return message;
                
            }
        }

        

        /// <summary>
        /// Tries to place ship. If successful, it returns true. If not, no action have been done on the database
        /// </summary>
        /// <param name="startGrid"></param>
        /// <param name="orientation"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string[] PlaceShip(string startCoordinate , string orientation , int length)
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
                return coordinates;
            }
            else
            {
                return null;
            }
            
        }

        public void DeleteGame()
        {
            Setup setup = new Setup();
            setup.CleanUp(Game);
            setup.CloseSession();
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
                if (!Grid.Keys.Contains(coordinate))
                    return false;
                if (Grid[coordinate].Ship != null)
                    return false;
            }
            return true;

        }

        
        private Ship IsShipSunk(Grid thisGrid , DobbyDBHelper dobby)
        {
            List<Grid> shipGrid = dobby.GetShipCoords(thisGrid);
            foreach (Grid grid in shipGrid)
            {
                if (grid.IsHit == false)
                    return null;
            }
            return shipGrid[0].Ship;
        }

        private void UpdateGrid()
        {
            DobbyDBHelper dobby = new DobbyDBHelper();
            dobby.UpdateGridToDB(Grid.Values.ToList());
            dobby.FreeDobby();
        }

        public List<Game> GetActiveGames()
        {
            DobbyDBHelper dobby = new DobbyDBHelper();
            List<Game> gameList = dobby.GetActiveGames();
            dobby.FreeDobby();
            return gameList;
        }
    }
}