using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tashbetzometry.Models;

namespace Tashbetzometry.Controllers
{
    public class SharedCrossController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/SharedCross/{mail}/
        [HttpGet]
        [Route("api/SharedCross/{crossNum}/")]
        public SharedCross Get(int crossNum)
        {
            SharedCross sc = new SharedCross();
            return sc.GetSharedCross(crossNum);
        }

        // POST api/<controller>
        public void Post([FromBody] SharedCross sharedCross)
        {
            SharedCross sc = new SharedCross();
            sc.PostSharedCross(sharedCross);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}