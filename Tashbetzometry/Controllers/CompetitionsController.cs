using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tashbetzometry.Models;

namespace Tashbetzometry.Controllers
{
    public class CompetitionsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet]
        [Route("api/Competitions/{crossnum}/")]
        public Competitions GetCompetition(int crossnum)
        {
            Competitions com = new Competitions();
            return com.GetCompetitonCross(crossnum);
        }
   
        // POST api/<controller>
        public int Post([FromBody]Competitions c)
        {
            Competitions competition = new Competitions();
            return competition.PostCompetitions(c);
        }
        
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}