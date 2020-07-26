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
        int crossNum;
        Notifications notification;

        public Competitions()
        {

        }

        public Competitions(string sendFrom, int fromCountAnswer, string sendTo, int toCountAnswer, int crossNum, Notifications notification)
        {
            SendFrom = sendFrom;
            FromCountAnswer = fromCountAnswer;
            SendTo = sendTo;
            ToCountAnswer = toCountAnswer;
            CrossNum = crossNum;
            Notification = notification;
        }

        public Competitions(int contestNum, string sendFrom, int fromCountAnswer, string sendTo, int toCountAnswer, int crossNum, Notifications notification)
        {
            ContestNum = contestNum;
            SendFrom = sendFrom;
            FromCountAnswer = fromCountAnswer;
            SendTo = sendTo;
            ToCountAnswer = toCountAnswer;
            CrossNum = crossNum;
            Notification = notification;
        }

        public int ContestNum { get => contestNum; set => contestNum = value; }
        public string SendFrom { get => sendFrom; set => sendFrom = value; }
        public int FromCountAnswer { get => fromCountAnswer; set => fromCountAnswer = value; }
        public string SendTo { get => sendTo; set => sendTo = value; }
        public int ToCountAnswer { get => toCountAnswer; set => toCountAnswer = value; }
        public int CrossNum { get => crossNum; set => crossNum = value; }
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

