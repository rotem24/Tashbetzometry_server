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
        string text;
        int crossNum;
        int helpNum;
        int contestNum;
        DateTime date;
        bool isRead;
        bool hasDone;

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

        public Notifications(int serialNum, string sendFrom, string userName, string firstName, string lastName, string image, string sendToGet, string type, string text, int crossNum, int helpNum, int contestNum, DateTime date, bool isRead, bool hasDone)
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
            Text = text;
            CrossNum = crossNum;
            HelpNum = helpNum;
            ContestNum = contestNum;
            Date = date;
            IsRead = isRead;
            HasDone = hasDone;
        }

        public Notifications(string sendFrom, string[] sendTo, string type, string text, int helpNum, DateTime date)
        {
            SendFrom = sendFrom;
            SendTo = sendTo;
            Type = type;
            Text = text;
            HelpNum = helpNum;
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
        public string Text { get => text; set => text = value; }
        public int CrossNum { get => crossNum; set => crossNum = value; }
        public int HelpNum { get => helpNum; set => helpNum = value; }
        public int ContestNum { get => contestNum; set => contestNum = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool IsRead { get => isRead; set => isRead = value; }
        public bool HasDone { get => hasDone; set => hasDone = value; }

        public List<Notifications> GetNotifications(string mail)
        {
            DBService db = new DBService();
            return db.GetNotifications(mail);
        }

        public int UpdateIsReadNotification(string mail)
        {
            DBService db = new DBService();
            return db.UpdateIsReadNotification(mail);
        }

        public int UpdateHasDoneNotification(int crossNum)
        {
            DBService db = new DBService();
            return db.UpdateHasDoneNotification(crossNum);
        }

        public int UpdateHasDoneHelpNotification(int helpNum)
        {
            DBService db = new DBService();
            return db.UpdateHasDoneHelpNotification(helpNum);
        }

        public int UpdateHasDoneContesNotification(int ContestNum)
        {
            DBService db = new DBService();
            return db.UpdateHasDoneContesNotification(ContestNum);
        }

        public int DeleteNotification(int serialNum)
        {
            DBService db = new DBService();
            return db.DeleteNotification(serialNum);
        }

        public int InsertHelpNotification(Notifications n)
        {
            DBService db = new DBService();
            return db.InsertHelpNotification(n);
        }


    }
}