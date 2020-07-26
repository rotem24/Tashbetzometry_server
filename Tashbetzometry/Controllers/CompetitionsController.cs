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

        [HttpGet]
        [Route("api/Competitions/User2/{ContestNum}/")]
        public Competitions GetCompetitionForUser2(int contestNum)
        {
            Competitions com = new Competitions();
            return com.GetCompetitonCrossForUser2(contestNum);
        }

        // POST api/<controller>
        public int Post([FromBody]Competitions c)
        {
            Competitions competition = new Competitions();
            return competition.PostCompetitions(c);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Competitions/User2/{ContestNum}/{ToCountAnswer}")]
        public void Put(int contestNum, int toCountAnswer)
        {
            Competitions c = new Competitions();
            c.UpdateCompetitionCrossUser2(contestNum, toCountAnswer);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}