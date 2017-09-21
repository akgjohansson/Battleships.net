using Battleships.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Battleships.net.Models;

namespace Battleships.net.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("login") ]
        public IHttpActionResult login(string username)
        {
            // Log-In i databas
            var newPlayer = new Player() { Name = username };
            return Ok(newPlayer);
        }
    }
}
