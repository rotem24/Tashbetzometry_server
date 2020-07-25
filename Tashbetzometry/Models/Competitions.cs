using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Models
{
    public class Competitions
    {
        int contestNum;
        string sendFrom;
        int fromCountAnswer;
        string sendTo;
        int toCountAnswer;
        string grid;
        string keys;
        string word;
        string clues;
        string legend;
        Notifications notification;

        public Competitions()
        {

        }

        public Competitions(int contestNum, string grid, string keys, string word, string clues, string legend)
        {
            ContestNum = contestNum;
            Grid = grid;
            Keys = keys;
            Word = word;
            Clues = clues;
            Legend = legend;
        }

        public Competitions(string sendFrom, int fromCountAnswer, string sendTo, int toCountAnswer, string grid, string keys, string word, string clues, string legend, Notifications notification)
        {
            SendFrom = sendFrom;
            FromCountAnswer = fromCountAnswer;
            SendTo = sendTo;
            ToCountAnswer = toCountAnswer;
            Grid = grid;
            Keys = keys;
            Word = word;
            Clues = clues;
            Legend = legend;
            Notification = notification;
        }

        public Competitions(int contestNum, string sendFrom, int fromCountAnswer, string sendTo, int toCountAnswer, string grid, string keys, string word, string clues, string legend, Notifications notification)
        {
            ContestNum = contestNum;
            SendFrom = sendFrom;
            FromCountAnswer = fromCountAnswer;
            SendTo = sendTo;
            ToCountAnswer = toCountAnswer;
            Grid = grid;
            Keys = keys;
            Word = word;
            Clues = clues;
            Legend = legend;
            Notification = notification;
        }

        public Competitions(string sendFrom, int fromCountAnswer, string sendTo, int toCountAnswer, string grid, string keys, string word, string clues, string legend)
        {
            SendFrom = sendFrom;
            FromCountAnswer = fromCountAnswer;
            SendTo = sendTo;
            ToCountAnswer = toCountAnswer;
            Grid = grid;
            Keys = keys;
            Word = word;
            Clues = clues;
            Legend = legend;
        }

        public int ContestNum { get => contestNum; set => contestNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public int FromCountAnswer { get => fromCountAnswer; set => fromCountAnswer = value; }
        public string SendTo { get => sendTo; set => sendTo = value; }
        public int ToCountAnswer { get => toCountAnswer; set => toCountAnswer = value; }
        public string Grid { get => grid; set => grid = value; }
        public string Keys { get => keys; set => keys = value; }
        public string Word { get => word; set => word = value; }
        public string Clues { get => clues; set => clues = value; }
        public string Legend { get => legend; set => legend = value; }
        public Notifications Notification { get => notification; set => notification = value; }

        public int PostCompetitions(Competitions competitions)
        {
            DBService db = new DBService();
            int numAffected = db.PostCompetitions(competitions);
            return numAffected;
        }
        public Competitions GetCompetitonCross(int crossNum)
        {
            DBService db = new DBService();
            return db.GetCompetitonCross(crossNum);
        }

    }
}