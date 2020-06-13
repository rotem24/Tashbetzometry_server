using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tashbetzometry.Models.DAL;

namespace Tashbetzometry.Models
{
	public class User
	{
		string mail;
		string userName;
		string password;
		string firstName;
		string lastName;
		string image;
        int score;
		string theme;

		public User()
		{

		}

		public User(string mail, string userName, string password, string firstName, string lastName, string image, int score, string theme)
		{
			Mail = mail;
			UserName = userName;
			Password = password;
			FirstName = firstName;
			LastName = lastName;
			Image = image;
            Score = score;
			Theme = theme;
		}

		public User(string mail, string password)
		{
			Mail = mail;
			Password = password;
		}

		public string Mail { get => mail; set => mail = value; }
		public string Password { get => password; set => password = value; }
		public string UserName { get => userName; set => userName = value; }
		public string FirstName { get => firstName; set => firstName = value; }
		public string LastName { get => lastName; set => lastName = value; }
		public string Image { get => image; set => image = value; }
        public int Score { get => score; set => score = value; }
		public string Theme { get => theme; set => theme = value; }


		//login
		public User GetUserFromDB(string mail, string password)
		{
			DBService db = new DBService();
			return db.GetUserFromDB(mail, password);
		}

		//register
		public int InsertUserToServer(User user)
		{
			DBService db = new DBService();
			int numAffected = db.InsertUserToDB(user);
			return numAffected;
		}

		//forgetPassword
		public User GetForgetPassFromDB(string mail)
		{
			DBService db = new DBService();
			return db.GetForgetPassFromDB(mail);
		}

		//update paassword
		public int UpdatePasswordDB(User user)
		{
			DBService db = new DBService();
			return db.UpdatePasswordDB(user);
		}

		//login with social
		public User GetUserDataFromDB(string mail)
		{
			DBService db = new DBService();
			return db.GetUserDataFromDB(mail);
		}

        //עדכון ניקוד למשתמש
        public int UpdateScoreDB(User user)
        {
            DBService db = new DBService();
            return db.UpdateScoreDB(user);
        }
        //הבאת כל המשתמשים
        public List<User> GetAllUsersFromDB()
        {
            DBService db = new DBService();
            return db.GetAllUsersFromDB();
        }
		//עדכון ערכת צבעים
		public int UpdateThemeDB(User user)//copy
		{
			DBService db = new DBService();
			return db.UpdateThemeDB(user);
		}

	}
}