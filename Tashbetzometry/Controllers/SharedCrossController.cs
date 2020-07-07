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

        // GET api/SharedCross/{crossnum}/
        [HttpGet]
        [Route("api/SharedCross/{crossNum}/")]
        public SharedCross Get(int crossNum)
        {
            SharedCross sc = new SharedCross();
            return sc.GetSharedCross(crossNum);
        }
        // GET api/SharedCross/{mail}/WatchAll
        [HttpGet]
        [Route("api/SharedCross/{mail}/WatchAll")]
        public List<SharedCross> GetAllSharedCross(string mail)
        {
            SharedCross sc = new SharedCross();
            return sc.GetAllSharedCross(mail);
        }

        //הבאת מספר התשבצים שמשתמש מסוים שיתף -
        // GET api/User/sharedcross/mail
        [HttpGet]
        [Route("api/SharedCross/{mail}/count")]
        public int GetSharedWith(string mail)
        {
            SharedCross SC = new SharedCross();
            return SC.GetSharedWithForUFromDB(mail);
        }

        //הבאת מספר התשבצים ששתיפו עם משתמש מסוים  -
        // GET api/User/sharedcross/mail
        [HttpGet]
        [Route("api/SharedCross/{mail}/countfrom")]
        public int GetCrossFrom(string mail)
        {
            SharedCross SC = new SharedCross();
            return SC.GetSharedFromForUFromDB(mail);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/SharedCross/")]
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