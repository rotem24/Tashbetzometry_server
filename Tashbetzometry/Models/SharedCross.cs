using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Models
{
    public class SharedCross
    {
        int crossNum;
        string sendFrom;
        string userName;
        string firstName;
        string lastName;
        string image;
        string sendToGet;
        string[] sendTo;
        string grid;
        string keys;
        string words;
        string clues;
        string legend;
        Notifications notification;
        int count;

        public int CrossNum { get => crossNum; set => crossNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public string UserName { get => userName; set => userName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Image { get => image; set => image = value; }
        public string SendToGet { get => sendToGet; set => sendToGet = value; }
        public string[] SendTo { get => sendTo; set => sendTo = value; }
        public string Grid { get => grid; set => grid = value; }
        public string Keys { get => keys; set => keys = value; }
        public string Words { get => words; set => words = value; }
        public string Clues { get => clues; set => clues = value; }
        public string Legend { get => legend; set => legend = value; }
        public Notifications Notification { get => notification; set => notification = value; }
        public int Count { get => count; set => count = value; }

        public SharedCross()
        {
        }

        

        public SharedCross(int crossNum, string sendFrom, string sendToGet, string grid, string keys, string words, string clues, string legend, Notifications notification)
        {
            CrossNum = crossNum;
            SendFrom = sendFrom;
            SendToGet = sendToGet;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;
            Notification = notification;
        }

        public SharedCross(string sendFrom, string[] sendTo, string grid, string keys, string words, string clues, string legend)
        {
            SendFrom = sendFrom;
            SendTo = sendTo;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;
        }

        public SharedCross(int crossNum, string sendFrom, string userName, string firstName, string lastName, string image, string sendToGet, string grid, string keys, string words, string clues, string legend)
        {
            CrossNum = crossNum;
            SendFrom = sendFrom;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Image = image;
            SendToGet = sendToGet;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;
        }

        public SharedCross(int count)
        {
            Count = count;
        }

        public int PostSharedCross(SharedCross sharedCross)
        {
            DBService db = new DBService();
            int numAffected = db.PostSharedCross(sharedCross);
            return numAffected;
        }

        public SharedCross GetSharedCross(int crossNum)
        {
            DBService db = new DBService();
            return db.GetSharedCross(crossNum);
        }

        ////מספר התשבצים ששיתף משתמש עם משתמש אחר
        public int GetSharedWithForUFromDB(string mail)
        {
            DBService db = new DBService();
            return db.GetSharedWithForUFromDB(mail);
        }

        ////מספר התשבצים ששותפו עם משתמש מסוים
        public int GetSharedFromForUFromDB(string mail)
        {
            DBService db = new DBService();
            return db.GetSharedFromForUFromDB(mail);
        }
        
    }
}