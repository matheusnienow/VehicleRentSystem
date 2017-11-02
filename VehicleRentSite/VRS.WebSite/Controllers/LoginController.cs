using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VRS.Model;
using VRS.WebSite.Models;
using VRS.ApiHelper;
using VRS.Model.Repository;
using VRS.WebSite.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using VRS.WebSite.Security;

namespace VRS.WebSite.Controllers
{
    public class LoginController : Controller
    {
        private IRepository<User> repo = Repository<User>.GetInstance();
        private VRS.Logic.Controller.LoginController controller = new Logic.Controller.LoginController();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }

            User user = repo.SearchFor(u => u.Login == userDto.Username).FirstOrDefault();
            bool verified = controller.VerifyUser(user, userDto.Password);
            Client client = Repository<Client>.GetInstance().SearchFor(c => c.UserId == user.Id).FirstOrDefault();
            if (verified)
            {
                AppConfig.LogIn(user, client);
                return RedirectToAction("Index", "Rents", new { area = "" });
            }
            return Index();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                Console.Write("error");
                return Register();
            }

            VRS.Logic.Controller.ClientController clientController = new Logic.Controller.ClientController();
            
            User createdUser = controller.CreateUser(user.Username, user.Password, 3);
            Client createdClient = clientController.CreateClient(user.Name, user.Surname, user.Sex, user.Phone, user.City, user.BirthDate, createdUser.Id);
            if (createdUser != null && createdClient != null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            return Register();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            AppConfig.LogOff();
            return RedirectToAction("Index");
        }
    }
}