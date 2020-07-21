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
        int sendFromTimer;
        string sendToget;
        int sendToTimer;
        string grid;
        string keys;
        string word;
        string clues;
        string legend;

        string[] sendTo;
        Notifications notification;

        public Competitions()
        {

        }

        public Competitions(string sendFrom, int sendFromTimer, string[] sendTo, int sendToTimer, string grid, string keys, string word, string clues, string legend, Notifications notification)
        {
            SendFrom = sendFrom;
            SendFromTimer = sendFromTimer;
            SendTo = sendTo;
            SendToTimer = sendToTimer;
            Grid = grid;
            Keys = keys;
            Word = word;
            Clues = clues;
            Legend = legend;
            Notification = notification;
        }

        public Competitions(int contestNum, string sendFrom, int sendFromTimer, string sendToget, int sendToTimer, string grid, string keys, string word, string clues, string legend, string[] sendTo, Notifications notification)
        {
            ContestNum = contestNum;
            SendFrom = sendFrom;
            SendFromTimer = sendFromTimer;
            SendToget = sendToget;
            SendToTimer = sendToTimer;
            Grid = grid;
            Keys = keys;
            Word = word;
            Clues = clues;
            Legend = legend;
            SendTo = sendTo;
            Notification = notification;
        }

        public int ContestNum { get => contestNum; set => contestNum = value; }
        public string SendToget { get => sendToget; set => sendToget = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public int SendFromTimer { get => sendFromTimer; set => sendFromTimer = value; }
        public int SendToTimer { get => sendToTimer; set => sendToTimer = value; }
        public string Grid { get => grid; set => grid = value; }
        public string Keys { get => keys; set => keys = value; }
        public string Word { get => word; set => word = value; }
        public string Clues { get => clues; set => clues = value; }
        public string Legend { get => legend; set => legend = value; }
        public string[] SendTo { get => sendTo; set => sendTo = value; }
        public Notifications Notification { get => notification; set => notification = value; }

        public int PostCompetitions(Competitions competitions)
        {
            DBService db = new DBService();
            int numAffected = db.PostCompetitions(competitions);
            return numAffected;
        }

    }
}