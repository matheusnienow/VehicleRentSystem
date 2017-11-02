using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.WebSite.Security
{
    public static class SessionPersister
    {
        static string usernameSessionvar = "username";
        public static User User { get; set; }
        public static Client Client { get; set; }

        public static string Username {
            get {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
                User = Repository<User>.GetInstance().SearchFor(u => u.Login == value).FirstOrDefault();
                if (User == null)
                {
                    Client = null;
                } else
                {
                    Client = Repository<Client>.GetInstance().SearchFor(u => u.UserId == User.Id).FirstOrDefault();
                }
            }
        }
    }
}