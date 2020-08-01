using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tashbetzometry.Models;

namespace Tashbetzometry.Controllers
{
	public class WordForUserController : ApiController
	{
		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet]
		[Route("api/WordForUser/{mail}/Level")]
		public List<WordForUser> Get(string mail)
		{
			WordForUser wfu = new WordForUser();
			return wfu.GetUserLevelFromDB(mail);
		}

        [HttpGet]
        [Route("api/WordForUser/{mail}/hardwords")]
        public List<WordForUser> Gethardwords(string mail)
        {
            WordForUser wfu = new WordForUser();
            return wfu.GetUseHardWordsFromDB(mail);
        }


        // POST api/<controller>
        public void Post([FromBody]WordForUser wfu)
		{
			WordForUser countWFU = new WordForUser();
			countWFU.PostWordsFUCountToDB(wfu);
		}

		// PUT api/<controller>/5
		public void Put(int id,[FromBody] string value)
		{
		}

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/WordForUser/{mail}/{word}/{solution}")]
        public int Delete(string mail, string word, string solution)
		{
            WordForUser n = new WordForUser();
            return n.DeleteHardWord(mail, word,solution);
        }
	}
}