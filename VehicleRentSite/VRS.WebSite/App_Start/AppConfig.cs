using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VRS.Model;
using VRS.WebSite.Security;

namespace VRS.WebSite.App_Start
{
    public class AppConfig
    {
        internal static void LogIn(User user, Client client)
        {
            SessionPersister.Client = client;
            SessionPersister.User = user;
            SessionPersister.Username = user.Login;
        }

        internal static void LogOff()
        {
            SessionPersister.Client = null;
            SessionPersister.User = null;
            SessionPersister.Username = string.Empty;
        }

        private void AuthConfig()
        {
        }
    }
}