using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using StoreFront.Data;

namespace Ecommerce.Controllers
{
    public class UsersController : Controller
    {
        SqlSecurityManager sqlSM = new SqlSecurityManager();

        // GET: Users 
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(StoreFront.Data.users user)
        {// works with new data source
            StoreFront.Data.users dbUser = sqlSM.LoadUser(user.UserName);

            if (!sqlSM.AuthenticateUser(user.UserName, user.Password))
            {// If user was not authenticated
                ViewBag.LoginMessage = "Error: Login failed.";
                return View();
            }
            
                Session.Add("ItemsInCart", Convert.ToInt32(user.GetItemsInCart(dbUser))); // Save items in cart to session
                Session.Add("UserID", user.UserID); // Save userID to session
                Session.Add("LogInValid", "True"); // Save the valid login to session
                return RedirectToAction("LoginSuccessful");
        }

        // GET: LoginSuccessful
        public ActionResult LoginSuccessful()
        {
            return View();
        }
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(StoreFront.Data.users user)
        {
            if (sqlSM.RegisterUser(user))
                return RedirectToAction("ConfirmRegister");

            ViewBag.UserExists = "User already exists";
            return View();
        }

        public ActionResult ConfirmRegister()
        {
            return View();
        }

        // Encrypt the password and return it back to the user object.
        public static string HashPassword(string pass)
        {
            // Generate hash, automatic 32 byte salt
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(pass, 10, 1000);
            byte[] hash = rfc.GetBytes(20); // Generate 20 bytes
            byte[] salt = rfc.Salt; // Gets the salt

            // Returns the salt and hash w/ a colon delimiter.
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public static string HashPassword(string pass, string salt)
        {
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(pass, Convert.FromBase64String(salt), 1000);
            byte[] hash = rfc.GetBytes(20);
            byte[] newSalt = rfc.Salt;
            return Convert.ToBase64String(newSalt) + ":" + Convert.ToBase64String(hash);
        }
    }
}