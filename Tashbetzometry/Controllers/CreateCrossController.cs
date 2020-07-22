using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Controllers
{
    public class CreateCrossController : ApiController
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
        // GET api/SharedCross/{mail}/WatchAll
        [HttpGet]
        [Route("api/CreateCross/{mail}/WatchAll")]
        public List<CreateCross> GetAllSharedCross(string mail)
        {
            CreateCross UCC = new CreateCross();
            return UCC.GetAllCreateCross(mail);
        }

        //הבאת מספר התשבצים שמשתמש מסוים שיתף -
        // GET api/User/CreateCross/mail
        [HttpGet]
        [Route("api/CreateCross/{mail}/count")]
        public int GetCreateCross(string mail)
        {
            CreateCross CC = new CreateCross();
            return CC.GetCreateCrossForUFromDB(mail);
        }

        // POST api/<controller>
        public void Post([FromBody]CreateCross UserCross)
        {
            CreateCross UCC = new CreateCross();
            UCC.PostUserCreateCross(UserCross);
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