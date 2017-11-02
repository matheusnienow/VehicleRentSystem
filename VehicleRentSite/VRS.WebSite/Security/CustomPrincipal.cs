using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.WebSite.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private User User;
        public IIdentity Identity { get; set; }

        public CustomPrincipal(User user)
        {
            User = user;
            this.Identity = new GenericIdentity(user.Login);
        }

        public bool IsInRole(string role)
        {
            string[] roles = role.Split(',');
            return roles.Any(r => r.Equals(User.Role.Name));
        }
    }
}