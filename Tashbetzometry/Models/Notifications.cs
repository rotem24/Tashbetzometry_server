using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Models
{
    public class Notifications
    {
        int serialNum;
        string sendFrom;
        string sendTo;
        string type;
        DateTime date;

        public Notifications()
        {

        }

        public Notifications(string sendFrom, string sendTo, string type, DateTime date)
        {
            SendFrom = sendFrom;
            SendTo = sendTo;
            Type = type;
            Date = date;
        }

        public Notifications(int serialNum, string sendFrom, string sendTo, string type, DateTime date)
        {
            SerialNum = serialNum;
            SendFrom = sendFrom;
            SendTo = sendTo;
            Type = type;
            Date = date;
        }

        public int SerialNum { get => serialNum; set => serialNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public string SendTo { get => sendTo; set => sendTo = value; }
        public string Type { get => type; set => type = value; }
        public DateTime Date { get => date; set => date = value; }

        public int PostNotification(Notifications n)
        {
            DBService db = new DBService();
            int numAffected = db.PostNotification(n);
            return numAffected;
        }
    }
}