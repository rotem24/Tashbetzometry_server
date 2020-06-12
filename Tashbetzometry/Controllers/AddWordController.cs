using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tashbetzometry.Models;

namespace Tashbetzometry.Controllers
{
    public class AddWordController : ApiController
    {
        // GET api/<controller>
        public List<AddWord> Get()
        {
            AddWord ad = new AddWord();
            return ad.GetAddWordFromDB();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]AddWord addWord)
        {
            AddWord ad = new AddWord();
            ad.PostAddWord(addWord);
        }

        // PUT api/<controller>/5
        //עדכון מספר לייקים
        [HttpPut]
        [Route("api/AddWord/AddLike")]
        public void Put([FromBody]AddWord addWord)
        {
            AddWord ad = new AddWord();
            ad.UpdateLikeDB(addWord);
        }
        // מחיקת הגדרות שהוצעו להוספה וקיבלו מעל 10 לייקים
        [HttpPut]
        [Route("api/AddWord/deletTen")]
        public void Put()
        {
            AddWord w = new AddWord();
            w.deletTen();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}