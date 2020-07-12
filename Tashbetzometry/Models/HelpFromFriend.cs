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
        string keyWord;
        string word;
        string solution;
        bool isHelped;

        string[] sendTo;
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

        public HelpFromFriend(int helpNum, string sendFrom, string firstName, string lastName, string image, string sendToGet, string keyWord, string word, string solution, bool isHelped)
        {
            HelpNum = helpNum;
            SendFrom = sendFrom;
            FirstName = firstName;
            LastName = lastName;
            Image = image;
            SendToGet = sendToGet;
            KeyWord = keyWord;
            Word = word;
            Solution = solution;
            IsHelped = isHelped;
        }

        public int HelpNum { get => helpNum; set => helpNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Image { get => image; set => image = value; }
        public string SendToGet { get => sendToGet; set => sendToGet = value; }
        public string KeyWord { get => keyWord; set => keyWord = value; }
        public string Word { get => word; set => word = value; }
        public string Solution { get => solution; set => solution = value; }
        public bool IsHelped { get => isHelped; set => isHelped = value; }
        public string[] SendTo { get => sendTo; set => sendTo = value; }
        public Notifications Notification { get => notification; set => notification = value; }


        public int PostHelpFromFriend(HelpFromFriend help)
        {
            string keyWordNoSpace = help.KeyWord.Replace(" ", "");
            DBService db = new DBService();
            int numAffected = db.PostHelpFromFriend(help, keyWordNoSpace);
            return numAffected;
        }

        public HelpFromFriend GetHelpFromFriend(int helpNum)
        {
            DBService db = new DBService();
            return db.GetHelpFromFriend(helpNum);
        }
    }
}