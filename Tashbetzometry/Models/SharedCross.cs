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
        string sendTo;
        string grid;
        string keys;
        string words;
        string clues;
        string legend;
        string a;

        public SharedCross()
        {
        }



        public SharedCross(int crossNum, string sendFrom, string sendTo, string grid, string keys, string words, string clues, string legend)
        {
            CrossNum = crossNum;
            SendFrom = sendFrom;
            SendTo = sendTo;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;
        }

        public SharedCross(string sendFrom, string sendTo, string grid, string keys, string words, string clues, string legend)
        {
            SendFrom = sendFrom;
            SendTo = sendTo;
            Grid = grid;
            Keys = keys;
            Words = words;
            Clues = clues;
            Legend = legend;

        }

        public int CrossNum { get => crossNum; set => crossNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public string SendTo { get => sendTo; set => sendTo = value; }
        public string Grid { get => grid; set => grid = value; }
        public string Keys { get => keys; set => keys = value; }
        public string Words { get => words; set => words = value; }
        public string Clues { get => clues; set => clues = value; }
        public string Legend { get => legend; set => legend = value; }

        public int PostSharedCross(SharedCross sharedCross)
        {
            DBService db = new DBService();
            int numAffected = db.PostSharedCross(sharedCross);
            return numAffected;
        }

        public SharedCross GetSharedCross(string mail)
        {
            DBService db = new DBService();
            return db.GetSharedCross(mail);
        }
    }
}