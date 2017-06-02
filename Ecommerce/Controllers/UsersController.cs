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
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ConfirmRegister(string userName, string userPass, string confirmPass, string userEmail)
        {
            ViewBag.userName = userName;
            ViewBag.userPass = userPass;
            ViewBag.confirmPass = confirmPass;
            ViewBag.userEmail = userEmail;

            if (!ValidateRegister())
                return RedirectToAction("RegisterFail");

            return View();
        }

        // Take in the parameters and make sure the user name is not taken, the passwords match, the email is valid.
        public bool ValidateRegister()
        {
            if (ViewBag.userPass == ViewBag.confirmPass)
            {
                // Encode password to a byte[], then add user to database
                byte[] passArr = Encoding.ASCII.GetBytes(ViewBag.userPass);

                // To decode, use string str = Encoding.ASCII.GetString(byte[] arr);

                users newUser = new users();

                newUser.CreatedBy = "admin";
                newUser.DateCreated = DateTime.Now;
                newUser.DateModified = null;
                newUser.EmailAddress = ViewBag.userEmail;
                newUser.IsAdmin = false;
                newUser.ModifiedBy = null;
                newUser.Password = passArr;
                newUser.UserName = ViewBag.userName;

                db.users.Add(newUser);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public ActionResult RegisterFail()
        {
            return View();
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
