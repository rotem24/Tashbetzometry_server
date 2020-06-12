using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Models
{
    public class AddWord
    {
        int serialNum;
        string userMail;
        string wordKey;
        int numOfLike;
        string firstName;
        string lastName;

        public AddWord()
        {

        }

        public AddWord(string userMail, string wordKey, int numOfLike)
        {
            UserMail = userMail;
            WordKey = wordKey;
            NumOfLike = numOfLike;
        }

        public AddWord(int serialNum, string userMail, string wordKey, int numOfLike)
        {
            SerialNum = serialNum;
            UserMail = userMail;
            WordKey = wordKey;
            NumOfLike = numOfLike;
        }

        public AddWord(string wordKey, int numOfLike, string firstName, string lastName)
        {
            WordKey = wordKey;
            NumOfLike = numOfLike;
            FirstName = firstName;
            LastName = lastName;
        }

        public AddWord(string wordKey, int numOfLike)
        {
            WordKey = wordKey;
            NumOfLike = numOfLike;
        }

        public int SerialNum { get => serialNum; set => serialNum = value; }
        public string UserMail { get => userMail; set => userMail = value; }
        public string WordKey { get => wordKey; set => wordKey = value; }
        public int NumOfLike { get => numOfLike; set => numOfLike = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }

        public int PostAddWord(AddWord addWord)
        {
            DBService db = new DBService();
            int numAffected = db.PostAddWord(addWord);
            return numAffected;
        }

        //הבאת המילים שהמשתמש מציע להוסיף
        public List<AddWord> GetAddWordFromDB()
        {
            //הבאת המילים שנוספו על ידי המשתמש מה-DB
            DBService db = new DBService();
            return db.GetAddWordFromDB();
           
        }
        //עדכון לייקים להגדרה חדשה שנוספה על ידי המשתמש
        public int UpdateLikeDB(AddWord addWord)
        {
            DBService db = new DBService();
            return db.UpdateLikeDB(addWord);
        }
        //מחיקת הגדרות שהוצעו להוספה וקיבלו מעל 10 לייקים
        public int deletTen()
        {
            DBService db = new DBService();
            return db.UpdateTenDB();
        }
    }
}