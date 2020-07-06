﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tashbetzometry.Models;

namespace Tashbetzometry.Controllers
{
	public class HintsController : ApiController
	{
		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

        //הבאת מספר הרמזים שלקח משתמש מסוים -
        // GET api/Hints/mail
        [HttpGet]
        [Route("api/Hints/{mail}/countHints")]
        public int GetCountHints(string mail)
        {
            Hints CH = new Hints();
            return CH.GetCountHintsFromDB(mail);
        }

        // GET api/<controller>/5
        public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		public void Post([FromBody]Hints hints)
		{
			Hints h = new Hints();
			h.PostHintToDB(hints);
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