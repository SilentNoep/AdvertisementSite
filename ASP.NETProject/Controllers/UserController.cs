using ASP.NETProject.Models;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ASP.NETProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User


        public ActionResult RegisterUser()
        {
            return View("RegisterUser");
        }

        public ActionResult UserRegistered(ViewModelUser myUser)
        {
            if (ModelState.IsValid)
            {
                RepositoryForUser rep = new RepositoryForUser();
                rep.AddUser(myUser.UserName, myUser.FirstName, myUser.LastName, myUser.Email, myUser.BirthDate, myUser.Password);
                AddCookie(myUser.UserName);
                TempData["message"] = "<script>alert('User Registered Successfully!');</script>";
                return RedirectToAction("Index", "Home");
            }
            return View("RegisterUser");
        }

        public ActionResult UpdateUser()
        {
            if ((Request.Cookies["user"] != null))
            {
                RepositoryForUser rep = new RepositoryForUser();
                var dBuser = rep.ReturnUserByUserName(Request.Cookies["user"]["UserName"]);
                ViewModelUser myUser = new ViewModelUser()
                {
                    BirthDate = dBuser.BirthDate,
                    Email = dBuser.Email,
                    FirstName = dBuser.FirstName,
                    LastName = dBuser.LastName,
                    UserName = dBuser.UserName
                };
                return View("UpdateUser", myUser);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserUpdated(ViewModelUser myuser)
        {
            ModelState.Remove("Username");
            if (ModelState.IsValid)
            {
                
                RepositoryForUser rep = new RepositoryForUser();
                User user = rep.ReturnUserByUserName(Request.Cookies["user"]["UserName"]);
                rep.UpdateInfo(user.Id,myuser.FirstName, myuser.LastName, myuser.Email, myuser.BirthDate, myuser.Password);
                TempData["message"] = "<script>alert('User Successfully Updated!');</script>";
                return RedirectToAction("Index", "Home");
            }

            return View("UpdateUser", myuser);
        }


        public void AddCookie(string userName)
        {
            HttpCookie myCookie = new HttpCookie("user");
            myCookie["UserName"] = userName;
            myCookie.Expires = DateTime.Now.AddDays(10);
            Response.Cookies.Add(myCookie);
        }
        [HttpPost]
        public ActionResult Signin(LoginViewModel MYUSER)
        {
            if (ModelState.IsValid)
            {
                RepositoryForUser rep = new RepositoryForUser();
                if (rep.LoginUser(MYUSER.UserNameMeh, MYUSER.PasswordMeh) != null)
                {
                    AddCookie(MYUSER.UserNameMeh);
                }
            }
            return View("LoginView");
        }
        public ActionResult DeleteCookie()
        {
            Session["User"] = null;
            HttpCookie myCookie = new HttpCookie("user");
            myCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(myCookie);
            return RedirectToAction("Index", "Home");
        }

     

    }
}