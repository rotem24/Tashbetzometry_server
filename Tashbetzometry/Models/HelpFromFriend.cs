using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Models
{
    public class HelpFromFriend
    {
        int helpNum;
        string sendFrom;
        string firstName;
        string lastName;
        string image;
        string sendToGet;
        string[] sendTo;
        string keyWord;
        bool isHelped;
        Notifications notification;

        public HelpFromFriend()
        {

        }

        public HelpFromFriend(string sendFrom, string[] sendTo, string keyWord, bool isHelped, Notifications notification)
        {
            SendFrom = sendFrom;
            SendTo = sendTo;
            KeyWord = keyWord;
            IsHelped = isHelped;
            Notification = notification;
        }

        public int HelpNum { get => helpNum; set => helpNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Image { get => image; set => image = value; }
        public string SendToGet { get => sendToGet; set => sendToGet = value; }
        public string[] SendTo { get => sendTo; set => sendTo = value; }
        public string KeyWord { get => keyWord; set => keyWord = value; }
        public bool IsHelped { get => isHelped; set => isHelped = value; }
        public Notifications Notification { get => notification; set => notification = value; }

        public int PostHelpFromFriend(HelpFromFriend help)
        {
            string keyWordNoSpace = help.KeyWord.Replace(" ", "");

            DBService db = new DBService();
            int numAffected = db.PostHelpFromFriend(help, keyWordNoSpace);
            return numAffected;
        }
    }
}