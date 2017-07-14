using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;

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

            bool goodUsername = (user.UserName != null);
            if (goodUsername)
            {
                string[] splitPass = user.Password.Split(':');
                string hashedPass = db.HashPassword(password, splitPass[0]); // Does this work now?

                bool goodPassword = db.users.Where(usr => usr.Password == hashedPass).SingleOrDefault() != null;

                if (goodPassword)
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
            if (HttpContext.Current.Session["Username"] != null)
            {
                string uName = HttpContext.Current.Session["UserName"].ToString();
                users tempUser = db.users.Where(item => item.UserName == uName).Single();

                return (tempUser.IsAdmin == true);
            }
            return false;
        }

        // LoadUser(string username) will load the properties of this object based on username given
        public users LoadUser(string username)
        {
            users user = db.users.Where(usr => usr.UserName.Equals(username)).SingleOrDefault();
            return user;
        }

        // SaveUser() Updates the DB based on UserID to keep information in sync b/w membership provider and User tables.
        //      This is unclear, what does that mean and how will it work? ASK DAN.
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
            users usrObj = (from user in db.users
                            where user.UserName == username
                            select user).SingleOrDefault();

            db.users.Remove(usrObj);
            db.SaveChanges();
        }

        // Use forms authentication and should be configured using the web.config.

        // User table should be added to EF, all operations should use EF in a ---- new class called 'UserRepository' ----
        // Login/Register should be updated to use the SqlSecurityManager to login/create users
        // CustomerAdminDetails screen should use SqlSecurityManager to save all details related to the user
    }
}