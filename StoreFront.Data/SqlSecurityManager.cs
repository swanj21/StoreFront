using System;
using System.Linq;
using System.Web;

namespace StoreFront.Data
{
    public class SqlSecurityManager
    {
        // Will utilize a new class and will use the information with the User table.
        StoreFrontDataModel db = new StoreFrontDataModel();

        // AuthenticateUser(string username, string password) will return true/false depending on whether or not the user and pass are valid.
        //    If so, username should be aded to Session w/ key of UserName(change mine, which is currently Username).
        public bool AuthenticateUser(string username, string password)
        {
            users user = db.users.Where(usr => usr.UserName == username).SingleOrDefault();
            if (user == null)
                return false;

            if (user.UserName != null)
            {
                string[] splitPass = user.Password.Split(':');
                string hashedPass = db.HashPassword(password, splitPass[0]);

                // If password is OK
                if (db.users.Where(usr => usr.Password == hashedPass).SingleOrDefault() != null)
                {
                    HttpContext.Current.Session.Add("UserName", username);
                    return true;
                }
            }
            return false;
        }

        // IsAdmin() will utilize EF to check if username stored in session is admin and return a boolean value
        public bool IsAdmin()
        {
            if (HttpContext.Current.Session["UserName"] != null)
            {
                string uName = HttpContext.Current.Session["UserName"].ToString();// LINQ does not accept using HttpContext within a .Where expression
                users tempUser = db.users.Where(item => item.UserName == uName).Single();
                return (tempUser.IsAdmin == true);
            }
            return false;
        }

        // LoadUser(string username) will load the properties of this object based on username given
        public users LoadUser(string username)
        {
            return db.users.Where(usr => usr.UserName.Equals(username)).SingleOrDefault();
        }

        // SaveUser() Updates the DB based on UserID to keep information in sync b/w membership provider and User tables.
        //      This is unclear, what does that mean and how will it work?
        public void SaveUser()
        {
            db.SaveChanges();
        }

        // RegisterUser()
        //     Create user account in appropriate table. No admin role for newly created users.
        public bool RegisterUser(users user)
        {
            // Check if the username is taken
            if (db.UserAlreadyExist(user))
            {
                return false;
            }

            user.CreatedBy = user.UserName;
            user.DateCreated = DateTime.Now;
            user.IsAdmin = false;
            user.Password = db.HashPassword(user.Password);
            user.ConfirmPass = user.Password;
            db.users.Add(user);
            db.SaveChanges();

            return true;
        }

        // DeleteUser(string username) 
        //     Remove user from database
        public void DeleteUser(string username)
        {
            db.users.Remove((from user in db.users
                             where user.UserName == username
                             select user).SingleOrDefault());
            db.SaveChanges();
        }
    }
}