using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using VRS.Logic;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private IRepository<User> repo = Repository<User>.GetInstance();
        Logic.Controller.LoginController controller = new Logic.Controller.LoginController();

        // GET: api/Users
        public IQueryable<User> GetUser()
        {
            return repo.GetAll();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = repo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DoLogin(string login, string password)
        {
            User user = repo.SearchFor(u => u.Login == login).First();
            if (user == null)
            {
                return NotFound();
            }

            bool verified = controller.VerifyUser(user, password);
            if (verified)
            {
                return Ok(user);

            }

            return Unauthorized();
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(string username, string password, int roleId)
        {
            User user = controller.CreateUser(username, password, roleId);

            repo.Insert(user);
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = repo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            repo.Delete(user);
            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return repo.SearchFor(e => e.Id == id).Count() > 0;
        }
    }
}