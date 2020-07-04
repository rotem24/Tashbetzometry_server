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
        string userName;
        string firstName;
        string lastName;
        string image;
        string sendToGet;
        string[] sendTo;
        string type;
        int crossNum;
        DateTime date;

        public Notifications()
        {

        }

        public Notifications(string sendFrom, string[] sendTo, string type, DateTime date)
        {
            SendFrom = sendFrom;
            SendTo = sendTo;
            Type = type;
            Date = date;
        }

        public Notifications(int serialNum, string sendFrom, string userName, string firstName, string lastName, string image, string sendToGet, string type, int crossNum, DateTime date)
        {
            SerialNum = serialNum;
            SendFrom = sendFrom;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Image = image;
            SendToGet = sendToGet;
            SendTo = sendTo;
            Type = type;
            CrossNum = crossNum;
            Date = date;
        }

        public int SerialNum { get => serialNum; set => serialNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public string UserName { get => userName; set => userName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Image { get => image; set => image = value; }
        public string SendToGet { get => sendToGet; set => sendToGet = value; }
        public string[] SendTo { get => sendTo; set => sendTo = value; }
        public string Type { get => type; set => type = value; }
        public int CrossNum { get => crossNum; set => crossNum = value; }
        public DateTime Date { get => date; set => date = value; }

        public List<Notifications> GetNotifications(string mail)
        {
            DBService db = new DBService();
            return db.GetNotifications(mail);
        }
    }
}