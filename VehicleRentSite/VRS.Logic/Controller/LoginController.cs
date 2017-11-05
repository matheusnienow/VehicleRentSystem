using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRS.Logic.Util;
using VRS.Model;
using VRS.Model.Repository;
using VRS.Repository.DTO;

namespace VRS.Logic.Controller
{
    public class UserController
    {
        private IRepository<User> repo = Repository<User>.NewInstance();

        public User CreateUser(string username, string password, int roleId)
        {
            var salt = SecurityHelper.GenerateSalt();
            var passwordHashed = SecurityHelper.HashPassword(password, salt);

            User user = new User(username, salt, passwordHashed, roleId);
            var result = repo.Insert(user);
            if (result > 0)
            {
                return user;
            }

            return null;
        }

        public IQueryable<User> GetAll()
        {
            return repo.GetAll();
        }

        public bool VerifyUser(User user, string password)
        {
            return VerifyUser(user.Salt, user.Hash, password);
        }

        public User VerifyUser(string username, string password)
        {
            var user = repo.SearchFor(u => u.Login == username).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            var verified = VerifyUser(user.Salt, user.Hash, password);
            if (!verified)
            {
                return null;
            }

            return user;
        }

        public bool VerifyUser(byte[] salt, byte[] hash, string password)
        {
            var passwordHashed = SecurityHelper.HashPassword(password, salt);
            return passwordHashed.SequenceEqual(hash);
        }
    }
}
