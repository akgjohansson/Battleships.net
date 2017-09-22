using Battleships.net.DataBase.Builder;
using Battleships.net.DataBase.DobbyDBHelper;
using Battleships.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Battleships.net.Controllers
{
    [RoutePrefix("bsapi")]
    public class BattleshipsApiController : ApiController
    {
        [Route("startGame"),HttpGet]
        public IHttpActionResult InitiateGame(string player1 , string player2 , int rows , int columns)
        {
            GameBoard gameBoard = GameBoard.StartGame(player1 , player2 , rows , columns);
            DobbyDBHelper dobby = new DobbyDBHelper();
            gameBoard.Grid = dobby.GetGridDictionary();
            dobby.FreeDobby();
            return Ok(gameBoard);
        }

        public IHttpActionResult InitiateGame(string player1, int rows , int columns)
        {
            GameBoard gameBoard = GameBoard.StartGame(player1, rows, columns);
            DobbyDBHelper dobby = new DobbyDBHelper();
            gameBoard.Grid = dobby.GetGridDictionary();
            dobby.FreeDobby();
            return Ok(gameBoard);
        }

        public IHttpActionResult JoinGame(GameBoard gameBoard, string player2)
        {
            gameBoard.JoinGame(player2);
            return Ok(gameBoard);
        }

        [Route("placeShip"),HttpGet]
        public IHttpActionResult PlaceShip(GameBoard gameBoard , string startCoord , string orientation , int length)
        {
            string[] coordinates = gameBoard.PlaceShip(startCoord, orientation, length);
            if (coordinates == null)
                return BadRequest("Ship cannot be placed here!");
            else
                return Ok(coordinates);
        }

        [Route("dropBomb"),HttpGet]
        public IHttpActionResult DropBomb(GameBoard gameBoard , string coordinate)
        {
            Message message = gameBoard.DropBomb(coordinate);
            if (message == null)
                return BadRequest("You have already fired here!");

            gameBoard.SwitchActivePlayer();

            if (message.GameOver)
                return Ok("Game over!");
            else if (message.SunkTheShip)
                return Ok("You sunk the ship!");
            else if (message.HitShip)
                return Ok("Hit!");
            else
                return Ok("Miss!");
        }
        
        [Route("joinGame"),HttpGet]
        public IHttpActionResult ShowActiveGames(GameBoard gameBoard)
        {
            List<Game> listOfGames = gameBoard.GetActiveGames();
            return Ok(gameBoard);
        }

        [Route("endGame"),HttpGet]
        public IHttpActionResult EndGame(GameBoard gameBoard)
        {
            gameBoard.DeleteGame();
            return Ok(gameBoard);
        }
        
    }
}