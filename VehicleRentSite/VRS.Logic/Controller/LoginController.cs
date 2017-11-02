using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRS.Logic.Util;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.Logic.Controller
{
    public class LoginController
    {
        private IRepository<User> repo = Repository<User>.GetInstance();

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
        
        public bool VerifyUser(User user, string password)
        {
            var passwordHashed = SecurityHelper.HashPassword(password, user.Salt);
            return passwordHashed.SequenceEqual(user.Hash);
        }
    }
}
