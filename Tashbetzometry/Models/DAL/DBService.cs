using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Tashbetzometry.Models.DAL
{
    public class DBService
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBService()
        {

        }

        public SqlConnection Connect(string conString)
        {
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con;              // assign the connection to the command object
            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 
                                               //cmd.CommandTimeout = 1000;           // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
            return cmd;
        }


        //הבאת מפתח-מילים-פתרונות-מספר הופעות מטבלת Words
        public List<Words> GetWordsFromDB()
        {
            List<Words> AP = new List<Words>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = "select * from [dbo].[Words]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Words a = new Words(Convert.ToString(dr["Key"]), Convert.ToString(dr["Word"]), Convert.ToString(dr["Solution"]), (int)dr["Count"], Convert.ToString(dr["WordWithSpace"]));
                    AP.Add(a);
                }
                return AP;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //ספירת מספר הופעות של מילים ועדכון טבלת Words
        public int PutWordsCountToDB(Words w)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateCountCommand(w);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateCountCommand(Words w)
        {
            var str = "";
            for (int i = 0; i < w.WordsArr.Length; i++)// מעבר על מערך המילים שהכנסנו ועדכון ספירתם בדאטה
            {
                str += UpdatDB(w.KeysArr[i], w.WordsArr[i], w.CluesArr[i]);
            }
            return str;
        }
        public string UpdatDB(string K, string W, string C)
        {
            return $"UPDATE Words SET [Count] = [Count]+1" +
                   $" WHERE [Key] = '{K}'";
        }


        //הבאת פרטי משתמש עבור אימות הLOGIN
        public User GetUserFromDB(string mail, string password)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = "select * from [User] where Mail = '" + mail + "' and Password = '" + password + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                User u = new User();
                while (dr.Read())
                {
                    u.Mail = Convert.ToString(dr["Mail"]);
                    u.UserName = Convert.ToString(dr["UserName"]);
                    u.Password = Convert.ToString(dr["Password"]);
                    u.FirstName = Convert.ToString(dr["FirstName"]);
                    u.LastName = Convert.ToString(dr["LastName"]);
                    u.Image = Convert.ToString(dr["Image"]);
                    u.Score = (int)(dr["Score"]);
                    u.Theme = Convert.ToString(dr["Theme"]);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הבאת פרטי משתמש עבור התחברות באמצעות רשתות חברתיות
        public User GetUserDataFromDB(string mail)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = "select * from [User] where Mail = '" + mail + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                User u = new User();
                while (dr.Read())
                {
                    u.Mail = Convert.ToString(dr["Mail"]);
                    u.UserName = Convert.ToString(dr["UserName"]);
                    u.Password = Convert.ToString(dr["Password"]);
                    u.FirstName = Convert.ToString(dr["FirstName"]);
                    u.LastName = Convert.ToString(dr["LastName"]);
                    u.Image = Convert.ToString(dr["Image"]);
                    u.Score = (int)(dr["Score"]);
                    u.Theme = Convert.ToString(dr["Theme"]);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הבאת שם משתמש וסיסמה עבור ForgetPassword
        public User GetForgetPassFromDB(string mail)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = "select Mail, [Password] from [User] where Mail = '" + mail + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                User u = new User();
                while (dr.Read())
                {
                    u.Mail = Convert.ToString(dr["Mail"]);
                    u.Password = Convert.ToString(dr["Password"]);
                }
                return u;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //יצירת משתמש חדש והכנסתו לטבלת User
        public int InsertUserToDB(User user)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                String cStr = BuildInsertUserCommand(user);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildInsertUserCommand(User user)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"INSERT INTO [User] (FirstName, LastName, Mail, UserName, [Password], Image, Score, Theme) VALUES ('{user.FirstName}', '{user.LastName}', '{user.Mail}', '{user.UserName}', '{user.Password}', '{user.Image}', '{user.Score}', '{user.Theme}');";//copy show theme 2
            command = prefix + sb.ToString();
            return command;
        }


        //לאחר קבלת רמז הוספת המילה לטבלת Hints כולל שם מקבל הרמז
        public int PostHintToDB(Hints hint)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                String cStr = BuildInsertHintCommand(hint);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildInsertHintCommand(Hints hint)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"INSERT INTO [Hints] (UserMail, WordKey) VALUES ('{hint.UserMail}', '{hint.WordKey}');";
            command = prefix + sb.ToString();
            return command;
        }


        // הבאת המילים הקשות ומספר ההופעות שלהן
        public List<Level> GetLevelForUser()
        {
            List<Level> wl = new List<Level>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = "SELECT [Words].[Key], [Words].Word, [Words].Solution,[Words].WordWithSpace ,  [Words].[Count] as NumOfShows, COUNT([Hints].[WordKey]) AS NumOfHints FROM [Hints] inner join [Words] on [Hints].[WordKey] = [Words].[Key] group by [Words].[Key], [Words].Word, [Words].Solution, [Words].WordWithSpace, [Words].[Count]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Level a = new Level(
                        Convert.ToString(dr["Key"]),
                        Convert.ToString(dr["Word"]),
                        Convert.ToString(dr["Solution"]),
                        Convert.ToString(dr["WordWithSpace"]),
                        (int)(dr["NumOfHints"]),
                        (int)(dr["NumOfShows"]));
                    wl.Add(a);
                }
                return wl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //ספירת מספר הופעות של מילים למשתמש ועדכון טבלת WordForUser
        public int PostWordsFUCountToDB(WordForUser wfu)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateCountWFUCommand(wfu);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateCountWFUCommand(WordForUser wfu)
        {
            var str = "";
            for (int i = 0; i < wfu.KeysArr.Length; i++)
            {
                str += UpdateDB(wfu.KeysArr[i], wfu.UserMail);
            }
            return str;
        }
        public string UpdateDB(string k, string um)
        {
            return $"insert into [WordForUser] (UserMail, WordKey) values('{um}', '{k}'); ";
        }


        // הבאת המילים הקשות לכל משתמש ומספר ההופעות שלהן
        public List<WordForUser> GetLevelForUser(string mail)
        {
            List<WordForUser> wfu = new List<WordForUser>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"select distinct TW.UserMail, TW.WordKey, TW.NumOfShows, TH.NumOfHints, W.Word, W.Solution
                from (TempWordForUser TW inner join  TempHintsForUser TH on TW.UserMail = TH.UserMail and TW.WordKey = TH.WordKey) inner join Words W on TW.WordKey = W.[Key]
                where TW.UserMail = '{mail}'
                group by TW.UserMail, TW.WordKey, TW.NumOfShows, TH.NumOfHints, W.Word, W.Solution";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    WordForUser a = new WordForUser(
                        Convert.ToString(dr["UserMail"]),
                        Convert.ToString(dr["WordKey"]),
                        Convert.ToString(dr["Word"]),
                        Convert.ToString(dr["Solution"]),
                        (int)(dr["NumOfShows"]),
                        (int)(dr["NumOfHints"]));

                    wfu.Add(a);
                }
                return wfu;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //עדכון סיסמה ForgetPassword
        //ספירת מספר הופעות של מילים ועדכון טבלת Words
        public int UpdatePasswordDB(User user)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdatePasswordCommand(user);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdatePasswordCommand(User user)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE [User] SET [Password] = '{user.Password}'  WHERE mail = '{user.Mail}'";
            command = prefix + sb.ToString();
            return command;
        }


        //עדכון ניקוד למשתמש
        public int UpdateScoreDB(User user)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateScoreCommand(user);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateScoreCommand(User user)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE [User] SET [Score] = '{user.Score}'  WHERE mail = '{user.Mail}'";
            command = prefix + sb.ToString();
            return command;
        }


        //הבאת כל המשתמשים 
        public List<User> GetAllUsersFromDB()
        {
            List<User> users = new List<User>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = "select * from [User]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    User u = new User();
                    u.Mail = Convert.ToString(dr["Mail"]);
                    u.UserName = Convert.ToString(dr["UserName"]);
                    u.Password = Convert.ToString(dr["Password"]);
                    u.FirstName = Convert.ToString(dr["FirstName"]);
                    u.LastName = Convert.ToString(dr["LastName"]);
                    u.Image = Convert.ToString(dr["Image"]);
                    u.Score = (int)(dr["Score"]);
                    u.Theme = Convert.ToString(dr["Theme"]);
                    users.Add(u);
                }

                return users;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הכנסת התשבץ ששותף לטבלת SharedCross
        public int PostSharedCross(SharedCross sc)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                String cStr = BuildSharedCrossCommand(sc);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildSharedCrossCommand(SharedCross sc)
        {
            if (sc.SendTo.Length <= 1)
            {
                string command;
                StringBuilder sb = new StringBuilder();
                string prefix = $"Declare @crossNum int;" +
                                $" INSERT INTO SharedCross VALUES ('{sc.SendFrom}', '{sc.SendTo[0]}', '{sc.Grid}', '{sc.Keys}', '{sc.Words}', '{sc.Clues}', '{sc.Legend}');" +
                                $" select @crossNum = SCOPE_IDENTITY()" +
                                $" INSERT INTO Notifications VALUES ('{sc.SendFrom}', '{sc.SendTo[0]}', '{sc.Notification.Type}', '{sc.Notification.Text}', @crossNum, {"NULL"}, {"NULL"}, '{sc.Notification.Date}', {0}, {0});";
                command = prefix + sb.ToString();
                return command;
            }
            else
            {
                string str = "";
                for (int i = 0; i < sc.SendTo.Length; i++)
                {
                    str += SharedCross(sc.SendFrom, sc.SendTo[i], sc.Grid, sc.Keys, sc.Words, sc.Clues, sc.Legend, sc.Notification.Type, sc.Notification.Text, sc.Notification.Date);
                }
                return str;
            }
        }
        private string SharedCross(string sf, string st, string g, string k, string w, string c, string l, string ty, string tx, DateTime d)
        {
            return $"Declare @crossNum int;" +
                                $"INSERT INTO SharedCross VALUES ('{sf}', '{st}', '{g}', '{k}', '{w}', '{c}', '{l}');" +
                                $"select @crossNum = SCOPE_IDENTITY()" +
                                $"INSERT INTO Notifications VALUES ('{sf}', '{st}', '{ty}', '{tx}', @crossNum, {"NULL"}, {"NULL"}, '{d}', {0}, {0});";
        }


        //הבאת השתבצים ששותפו עבור המשתמש
        public SharedCross GetSharedCross(int crossNum)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = @"SELECT SC.CrossNum, SC.SendFrom, U.UserName, U.FirstName, U.LastName, U.Image, SC.SendTo, SC.Grid, SC.Keys, SC.Words, SC.Clues, SC.Legend
FROM [User] as U
INNER JOIN SharedCross as SC
ON U.Mail = SC.SendFrom
WHERE SC.CrossNum = '" + crossNum + "';";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                SharedCross sc = new SharedCross();
                while (dr.Read())
                {
                    sc.CrossNum = (int)(dr["CrossNum"]);
                    sc.SendFrom = Convert.ToString(dr["SendFrom"]);
                    sc.UserName = Convert.ToString(dr["UserName"]);
                    sc.FirstName = Convert.ToString(dr["FirstName"]);
                    sc.LastName = Convert.ToString(dr["LastName"]);
                    sc.Image = Convert.ToString(dr["Image"]);
                    sc.SendToGet = Convert.ToString(dr["SendTo"]);
                    sc.Grid = Convert.ToString(dr["Grid"]);
                    sc.Keys = Convert.ToString(dr["Keys"]);
                    sc.Words = Convert.ToString(dr["Words"]);
                    sc.Clues = Convert.ToString(dr["Clues"]);
                    sc.Legend = Convert.ToString(dr["Legend"]);
                }
                return sc;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הכנסת הגדרה חדשה שהוכנסה על ידי משתמש
        public int PostAddWord(AddWord ad)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                String cStr = BuildAddWordCommand(ad);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildAddWordCommand(AddWord ad)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"INSERT INTO AddWord VALUES ('{ad.UserMail}', '{ad.WordKey}', '{ad.NumOfLike}');";
            command = prefix + sb.ToString();
            return command;
        }


        //הבאת המילים החדשות שנוספו על ידי המשתמשים
        public List<AddWord> GetAddWordFromDB()
        {
            List<AddWord> AD = new List<AddWord>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = "select[AddWord].WordKey, [AddWord].NumOfLike, [User].FirstName, [User].LastName from[dbo].[AddWord] inner join[User] on[AddWord].UserMail = [User].Mail";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    AddWord ad = new AddWord(Convert.ToString(dr["WordKey"]), (int)dr["NumOfLike"], Convert.ToString(dr["FirstName"]), Convert.ToString(dr["LastName"]));
                    AD.Add(ad);
                }
                return AD;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //עדכון לייקים על הגדרה חדשה שנוספה על ידי משתמש
        public int UpdateLikeDB(AddWord ad)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateLikeCommand(ad);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateLikeCommand(AddWord ad)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE [AddWord] SET [NumOfLike] = '{ad.NumOfLike}'  WHERE WordKey = '{ad.WordKey}'";
            command = prefix + sb.ToString();
            return command;
        }


        //מחיקת הגדרות שהוצעו להוספה וקיבלו מעל 10 לייקים
        public int UpdateTenDB()
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = $"delete from [dbo].[AddWord]" +
                   $" WHERE [NumOfLike] >= 10 ";
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //עדכון ערכת צבעים למשתמש
        public int UpdateThemeDB(User user)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateThemeCommand(user);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateThemeCommand(User user)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE [User] SET [Theme] = '{user.Theme}'  WHERE mail = '{user.Mail}'";
            command = prefix + sb.ToString();
            return command;
        }


        //עדכון מילים בעלות עשר לייקים בטבלת words
        public int UpdateNewWordsToDB(Words w)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateNewWordsCommand(w);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateNewWordsCommand(Words w)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"if NOT EXISTS(select * from [Words] where [Key] = '{w.Key}' and Word = '{w.Word}' and Solution = '{w.Clue}') begin insert into [Words] ([Key], Word, Solution) values('{w.Key}', '{w.Word}', '{w.Clue}') end";
            command = prefix + sb.ToString();
            return command;
        }


        // הבאת מספר התשבצים ששיתף משתמש מסוים
        public int GetSharedWithForUFromDB(string mail)
        {
            int num = 0;
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"  select count(SendFrom) as 'count'
          from [bgroup11_prod].[dbo].[SharedCross]
        where SendFrom='{mail}'  ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    num = (int)(dr["count"]);
                    return num;
                }
                return num;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        // הבאת מספר התשבצים ששותפו עם משתמש מסוים
        public int GetSharedFromForUFromDB(string mail)
        {
            int num = 0;
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"  select count(SendFrom) as 'count'
          from [bgroup11_prod].[dbo].[SharedCross]
        where SendTo='{mail}'  ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    num = (int)(dr["count"]);
                    return num;
                }
                return num;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        // הבאת מספר התשבצים ששותפו עם משתמש מסוים
        public int GetCountHintsFromDB(string mail)
        {
            int num = 0;
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"  select count(UserMail) as 'count'
          from [bgroup11_prod].[dbo].[Hints]
        where UserMail='{mail}'  ";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    num = (int)(dr["count"]);
                    return num;
                }
                return num;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }


        // הבאת המילים הקשות לכל משתמש ומספר ההופעות שלהן
        public List<WordForUser> GetUseHardWordsFromDB(string mail)
        {
            List<WordForUser> wfu = new List<WordForUser>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"  select distinct Words.WordWithSpace, Words.Solution
  from [dbo].[Hints] inner join Words on [dbo].[Hints].Wordkey= Words.[Key]
 where [dbo].[Hints].UserMail = '{mail}'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    WordForUser a = new WordForUser(

                        Convert.ToString(dr["WordWithSpace"]),
                        Convert.ToString(dr["Solution"])
                       );

                    wfu.Add(a);
                }
                return wfu;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הבאת השתבצים ששותפו עבור המשתמש
        public List<SharedCross> GetAllSharedCross(string mail)
        {
            List<SharedCross> lsc = new List<SharedCross>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"SELECT SC.CrossNum, SC.SendFrom, U.UserName, U.FirstName, U.LastName, U.Image, SC.SendTo, SC.Grid, SC.Keys, SC.Words, SC.Clues, SC.Legend
FROM [User] as U
INNER JOIN SharedCross as SC
ON U.Mail = SC.SendFrom
WHERE SC.SendFrom='{mail}' or SC.SendTo='{mail}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                SharedCross sc = new SharedCross();
                while (dr.Read())
                {
                    sc = new SharedCross();
                    sc.CrossNum = (int)(dr["CrossNum"]);
                    sc.SendFrom = Convert.ToString(dr["SendFrom"]);
                    sc.UserName = Convert.ToString(dr["UserName"]);
                    sc.FirstName = Convert.ToString(dr["FirstName"]);
                    sc.LastName = Convert.ToString(dr["LastName"]);
                    sc.Image = Convert.ToString(dr["Image"]);
                    sc.SendToGet = Convert.ToString(dr["SendTo"]);
                    sc.Grid = Convert.ToString(dr["Grid"]);
                    sc.Keys = Convert.ToString(dr["Keys"]);
                    sc.Words = Convert.ToString(dr["Words"]);
                    sc.Clues = Convert.ToString(dr["Clues"]);
                    sc.Legend = Convert.ToString(dr["Legend"]);
                    lsc.Add(sc);
                }
                return lsc;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הכנסת תשבץ שיצר משתמש מסוים לטבלה
        public int PostUserCreateCross(CreateCross UCC)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                String cStr = BuildUserCreateCrossCommand(UCC);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUserCreateCrossCommand(CreateCross UCC)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"Declare @crossNum int;" + $"INSERT INTO UserCreateCross VALUES ('{UCC.UserMail}', '{UCC.Grid}', '{UCC.Keys}', '{UCC.Words}', '{UCC.Clues}', '{UCC.Legend}');";
            command = prefix + sb.ToString();
            return command;
        }


        //הבאת השתבצים שייצר המשתמש
        public List<CreateCross> GetAllCreateCross(string mail)
        {
            List<CreateCross> lUCC = new List<CreateCross>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"SELECT * FROM UserCreateCross 
WHERE UserMail='{mail}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    CreateCross UCC = new CreateCross();
                    UCC.CrossNum = (int)(dr["CrossNum"]);
                    UCC.Grid = Convert.ToString(dr["Grid"]);
                    UCC.Keys = Convert.ToString(dr["Keys"]);
                    UCC.Words = Convert.ToString(dr["Words"]);
                    UCC.Clues = Convert.ToString(dr["Clues"]);
                    UCC.Legend = Convert.ToString(dr["Legend"]);
                    lUCC.Add(UCC);
                }
                return lUCC;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //הכנסת עזרה מחבר לטבלת HelpFromFriend
        public int PostHelpFromFriend(HelpFromFriend hff, string keyWordNoSpace)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                String cStr = BuildHelpFromFriendCommand(hff, keyWordNoSpace);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildHelpFromFriendCommand(HelpFromFriend hff, string keyWordNoSpace)
        {
            if (hff.SendTo.Length <= 1)
            {
                string command;
                StringBuilder sb = new StringBuilder();
                string prefix = $"Declare @HelpNum int;" +
                                $" INSERT INTO HelpFromFriend VALUES ('{hff.SendFrom}', '{hff.SendTo[0]}', '{keyWordNoSpace}', '{hff.IsHelped}');" +
                                $" select @HelpNum = SCOPE_IDENTITY()" +
                                $" INSERT INTO Notifications VALUES ('{hff.SendFrom}', '{hff.SendTo[0]}', '{hff.Notification.Type}', '{hff.Notification.Text}', {"NULL"}, @HelpNum, {"NULL"}, '{hff.Notification.Date}', {0}, {0});";
                command = prefix + sb.ToString();
                return command;
            }
            else
            {
                string str = "";
                for (int i = 0; i < hff.SendTo.Length; i++)
                {
                    str += HelpFromFriend(hff.SendFrom, hff.SendTo[i], hff.KeyWord, hff.IsHelped, hff.Notification.Type, hff.Notification.Text, hff.Notification.Date);
                }
                return str;
            }
        }
        private string HelpFromFriend(string sf, string st, string kw, bool ih, string ty, string tx, DateTime d)
        {
            return $"Declare @HelplNum int;" +
                                $"INSERT INTO HelpFromFriend VALUES ('{sf}', '{st}', '{kw}', '{ih}');" +
                                $"select @HelplNum = SCOPE_IDENTITY()" +
                                $"INSERT INTO Notifications VALUES ('{sf}', '{st}', '{ty}', '{tx}', {"NULL"}, @HelplNum, {"NULL"}, '{d}', {0}, {0});";
        }


        //הבאת כל ההתראות מטבלת Notifications
        public List<Notifications> GetNotifications(string mail)
        {
            List<Notifications> notifications = new List<Notifications>();
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = @"SELECT TOP 15 N.SerialNum, N.SendFrom, U.UserName, U.FirstName, U.LastName, U.Image, N.SendTo, N.Type, N.Text, N.CrossNum, N.HelpNum, N.ContestNum, N.Date, N.IsRead, N.HasDone
FROM [User] as U
INNER JOIN Notifications as N
ON U.Mail = N.SendFrom
WHERE N.SendTo = '" + mail + "' ORDER BY N.Date DESC";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Notifications n = new Notifications();
                    n.SerialNum = (int)(dr["SerialNum"]);
                    n.SendFrom = Convert.ToString(dr["SendFrom"]);
                    n.UserName = Convert.ToString(dr["UserName"]);
                    n.FirstName = Convert.ToString(dr["FirstName"]);
                    n.LastName = Convert.ToString(dr["LastName"]);
                    n.Image = Convert.ToString(dr["Image"]);
                    n.SendToGet = Convert.ToString(dr["SendTo"]);
                    n.Type = Convert.ToString(dr["Type"]);
                    n.Text = Convert.ToString(dr["Text"]);
                    if (!Convert.IsDBNull((dr["CrossNum"])))
                    {
                        n.CrossNum = Convert.ToInt32((dr["CrossNum"]));
                    }
                    else
                    {
                        n.CrossNum = 0;
                    }
                    if (!Convert.IsDBNull((dr["HelpNum"])))
                    {
                        n.HelpNum = Convert.ToInt32((dr["HelpNum"]));
                    }
                    else
                    {
                        n.HelpNum = 0;
                    }
                    if (!Convert.IsDBNull((dr["ContestNum"])))
                    {
                        n.ContestNum = Convert.ToInt32((dr["ContestNum"]));
                    }
                    else
                    {
                        n.ContestNum = 0;
                    }
                    n.Date = (DateTime)(dr["Date"]);
                    n.IsRead = (bool)(dr["IsRead"]);
                    n.HasDone = (bool)(dr["HasDone"]);
                    notifications.Add(n);
                }
                return notifications;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        //עדכון טבלת התראות אם המשתמש קרא את ההתראה
        public int UpdateIsReadNotification(string mail)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateIsReadCommand(mail);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateIsReadCommand(string mail)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE Notifications SET IsRead = 1 WHERE IsRead = 0 AND SendTo = '{mail}';";
            command = prefix + sb.ToString();
            return command;
        }


        // עדכון טבלת התראות אם המשתמש טיפל בהתראת שתף תשבץ
        public int UpdateHasDoneNotification(int crossNum)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateHasDoneCommand(crossNum);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateHasDoneCommand(int crossNum)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE Notifications SET HasDone = 1 WHERE HasDone = 0 AND CrossNum = {crossNum};";
            command = prefix + sb.ToString();
            return command;
        }


        // עדכון טבלת התראות אם המשתמש טיפל בהתראת עזרה מחבר
        public int UpdateHasDoneHelpNotification(int helpNum)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateHasDoneHelpCommand(helpNum);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateHasDoneHelpCommand(int helpNum)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE Notifications SET HasDone = 1 WHERE HasDone = 0 AND HelpNum = {helpNum};";
            command = prefix + sb.ToString();
            return command;
        }


        // עדכון טבלת התראות אם המשתמש טיפל בהתראת בקש תחרות
        public int UpdateHasDoneContesNotification(int ContestNum)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildUpdateHasDoneContestCommand(ContestNum);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildUpdateHasDoneContestCommand(int ContestNum)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"UPDATE Notifications SET HasDone = 1 WHERE HasDone = 0 AND ContestNum = {ContestNum};";
            command = prefix + sb.ToString();
            return command;
        }


        //מחיקת התראה מטבלת Notifications
        public int DeleteNotification(int serialNum)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                string cStr = BuildDeleteNotificationCommand(serialNum);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildDeleteNotificationCommand(int serialNum)
        {
            string command;
            StringBuilder sb = new StringBuilder();
            string prefix = $"DELETE FROM Notifications WHERE SerialNum = {serialNum};";
            command = prefix + sb.ToString();
            return command;
        }


        //הבאת השתבצים ששותפו עבור המשתמש
        public HelpFromFriend GetHelpFromFriend(int helpNum)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DBConnectionString");
                String selectSTR = $@"SELECT H.HelpNum, H.SendFrom, U.FirstName, U.LastName, U.Image, H.SendTo, H.KeyWord, W.Word, W.Solution, H.IsHalped
FROM HelpFromFriend H
inner join [User] U on H.SendFrom = U.Mail
inner join Words W on H.KeyWord = W.[Key]
WHERE H.HelpNum = {helpNum};";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                HelpFromFriend hff = new HelpFromFriend();
                while (dr.Read())
                {
                    hff.HelpNum = (int)(dr["HelpNum"]);
                    hff.SendFrom = Convert.ToString(dr["SendFrom"]);
                    hff.FirstName = Convert.ToString(dr["FirstName"]);
                    hff.LastName = Convert.ToString(dr["LastName"]);
                    hff.Image = Convert.ToString(dr["Image"]);
                    hff.SendToGet = Convert.ToString(dr["SendTo"]);
                    hff.KeyWord = Convert.ToString(dr["KeyWord"]);
                    hff.Word = Convert.ToString(dr["Word"]);
                    hff.Solution = Convert.ToString(dr["Solution"]);
                    hff.IsHelped = (bool)(dr["IsHalped"]);
                }
                return hff;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //עדכון טבלת תשחץ תחרות
        public int PostCompetitions(Competitions competitions)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = Connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            try
            {
                String cStr = BuildCompetitionsCommand(competitions);
                cmd = CreateCommand(cStr, con);
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        private string BuildCompetitionsCommand(Competitions c)
        {
            if (c.SendTo.Length <= 1)
            {
                string command;
                StringBuilder sb = new StringBuilder();
                string prefix = $"Declare @ContestNum int;" +
                                $" INSERT INTO CompetitionsCross VALUES ('{c.SendFrom}', '{c.SendFromTimer}', '{c.SendTo[0]}', '{c.SendToTimer}','{c.Grid}', '{c.Keys}', '{c.Word}', '{c.Clues}', '{c.Legend}');" +
                                $" select @ContestNum = SCOPE_IDENTITY()" +
                                $" INSERT INTO Notifications VALUES ('{c.SendFrom}', '{c.SendTo[0]}', '{c.Notification.Type}', '{c.Notification.Text}', {"NULL"},{"NULL"}, @ContestNum, '{c.Notification.Date}', {0}, {0});";
                command = prefix + sb.ToString();
                return command;
            }
            else
            {
                string str = "";
                for (int i = 0; i < c.SendTo.Length; i++)
                {
                    str += Competitions(c.SendFrom, c.SendFromTimer, c.SendTo[i], c.SendToTimer, c.Grid, c.Keys, c.Word, c.Clues, c.Legend, c.Notification.Type, c.Notification.Text, c.Notification.Date);
                }
                return str;
            }
        }
        private string Competitions(string sendFrom, int sendFromTimer, string sendTo, int sendToTimer, string grid, string keys, string word, string clues, string legend, string type, string text, DateTime d)
        {
            return $"Declare @ContestNum int;" +
                                $"INSERT INTO CompetitionsCross VALUES ('{sendFrom}', '{sendFromTimer}', '{sendTo[0]}', '{sendToTimer}','{grid}', '{keys}', '{word}', '{clues}', '{legend}');" +
                                $"select @ContestNum = SCOPE_IDENTITY()" +
                                $"INSERT INTO Notifications VALUES ('{sendFrom}', '{sendFromTimer}', '{sendTo[0]}', '{sendToTimer}','{grid}', '{keys}', '{word}', '{clues}', '{legend}', {"NULL"},{"NULL"}, @ContestNum, {d}, {0}, {0});";
        }
    }

}



