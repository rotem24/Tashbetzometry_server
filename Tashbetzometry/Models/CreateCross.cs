using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tashbetzometry.Models.DAL
{
    public class CreateCross
    {
        int crossNum;
        string userMail;
        string firstName;
        string lastName;
        string grid;
        string keys;
        string words;
        string clues;
        string legend;


        public CreateCross()
        {

        }

        public CreateCross(string userMail, string grid, string keys, string words, string clues, string legend)
        {
            UserMail = userMail;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;
        }

        public CreateCross(int crossNum, string userMail, string grid, string keys, string words, string clues, string legend)
        {
            CrossNum = crossNum;
            UserMail = userMail;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;
        }

        public CreateCross(int crossNum, string userMail, string firstName, string lastName, string grid, string keys, string words, string clues, string legend)
        {
            CrossNum = crossNum;
            UserMail = userMail;
            FirstName = firstName;
            LastName = lastName;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;
        }

        public int CrossNum { get => crossNum; set => crossNum = value; }
        public string UserMail { get => userMail; set => userMail = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Grid { get => grid; set => grid = value; }
        public string Keys { get => keys; set => keys = value; }
        public string Words { get => words; set => words = value; }
        public string Clues { get => clues; set => clues = value; }
        public string Legend { get => legend; set => legend = value; }


        public int PostUserCreateCross(CreateCross UserCreateCross)
        {
            DBService db = new DBService();
            int numAffected = db.PostUserCreateCross(UserCreateCross);
            return numAffected;
        }

        //הבאת כלל התשבצים שמשתמש יצר
        public List<CreateCross> GetAllCreateCross(string mail)
        {
            DBService db = new DBService();
            return db.GetAllCreateCross(mail);
        }
      

    }
}