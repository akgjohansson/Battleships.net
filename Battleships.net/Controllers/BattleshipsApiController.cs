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
        [Route("placeShip"),HttpGet]
        public IHttpActionResult PlaceShip(GameBoard gameBoard , string startCoord , string orientation , int length)
        {
            string[] coordinates = gameBoard.PlaceShip(startCoord, orientation, length);
            if (coordinates == null)
                return BadRequest("Ship cannot be placed here!");
            else
                return Ok(coordinates);
        }

        public IHttpActionResult DropBomb(GameBoard gameBoard , string coordinate)
        {
            Message message = gameBoard.DropBomb(coordinate);
            if (message == null)
                return BadRequest("You have already fired here!");
            else if (message.SunkTheShip)
                return Ok("You sunk the ship!");
            else if (message.HitShip)
                return Ok("Hit!");
            else
                return Ok("Miss!");
        }
    }
}