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

namespace Ecommerce.Controllers
{
    public class UsersController : Controller
    {
        private CustomerBaseViewModel db = new CustomerBaseViewModel();

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
        public ActionResult Login(users user)
        {
            users dbUser = user.FindUser(user.UserName);
            if (dbUser == null)
            {
                ViewBag.LoginMessage = "Error: Login failed.";
                return View();
            }
            // User was found
            string comparedPass = user.Password; // Not hashed pw
            string dbPass = dbUser.Password; // Hashed pw

            string[] salt = dbPass.Split(':');
            if (HashPassword(user.Password, salt[0]).Equals(dbPass))
            {
                Session.Add("ItemsInCart", user.GetItemsInCart(dbUser)); // Save items in cart to session
                Session.Add("Username", user.UserName); // Save username to session
                Session.Add("LogInValid", "True"); // Save the valid login to session
                return RedirectToAction("LoginSuccessful");
            }
            else
                ViewBag.LoginMessage = "Error: Login failed.";

            return View();
        }

        // GET: LoginSuccessful
        public ActionResult LoginSuccessful()
        {
            return View();
        }
        // GET: Register
        public ActionResult Register()
        {// Error gets thrown here if I pass either a user object or the db object.
            return View();
        }

        // POST: Register
        [HttpPost]
        public ActionResult Register(users user)
        {
            // Check if username is taken
            if (user.UserAlreadyExist(user.UserName))
            {
                ViewBag.UserExists = "Username already exists.";
                return View();
            }

            if (ModelState.IsValid)
            {
                user.CreatedBy = "admin";
                user.DateCreated = DateTime.Now;
                user.IsAdmin = false;
                user.Password = HashPassword(user.Password);
                user.ConfirmPass = user.Password;
                db.users.Add(user);
                db.SaveChanges();

                return RedirectToAction("ConfirmRegister");
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}