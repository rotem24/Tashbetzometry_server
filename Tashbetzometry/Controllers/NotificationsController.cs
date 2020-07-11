using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Web.Http;
using Tashbetzometry.Models;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Controllers
{
    public class NotificationsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Notifications/{mail}/")]
        public List<Notifications> Get(string mail)
        {
            Notifications n = new Notifications();
            return n.GetNotifications(mail);
        }

        // POST api/<controller>
        public void Post([FromBody] Notifications n)
        {
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("api/Notifications/IsRead/{mail}/")]
        public int Put(string mail)
        {
            Notifications n = new Notifications();
            return n.UpdateIsReadNotification(mail);
        }

        [HttpPut]
        [Route("api/Notifications/HasDone/SharedCross/{crossNum}/")]
        public int PutHasDone(int crossNum)
        {
            Notifications n = new Notifications();
            return n.UpdateHasDoneNotification(crossNum);
        }

        [HttpPut]
        [Route("api/Notifications/HasDone/HelpFromFriend/{helpNum}/")]
        public int PutHelpHasDone(int helpNum)
        {
            Notifications n = new Notifications();
            return n.UpdateHasDoneHelpNotification(helpNum);
        }

        [HttpPut]
        [Route("api/Notifications/HasDone/Competitions/{ContestNum}/")]
        public int PutContesHasDone(int ContestNum)
        {
            Notifications n = new Notifications();
            return n.UpdateHasDoneContesNotification(ContestNum);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Notifications/SharedCross/{crossNum}/")]
        public int Delete(int crossNum)
        {
            Notifications n = new Notifications();
            return n.DeleteNotification(crossNum);
        }

        [HttpDelete]
        [Route("api/Notifications/HelpFromFriend/{helpNum}/")]
        public int DeleteHelp(int helpNum)
        {
            Notifications n = new Notifications();
            return n.DeleteNotification(helpNum);
        }

        [HttpDelete]
        [Route("api/Notifications/Competitions/{ContestNum}/")]
        public int DeleteContest(int ContestNum)
        {
            Notifications n = new Notifications();
            return n.DeleteContestNotification(ContestNum);
        }
    }
}