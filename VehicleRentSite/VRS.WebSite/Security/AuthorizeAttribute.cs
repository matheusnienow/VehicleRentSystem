using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VRS.Model;
using VRS.Model.Repository;

namespace VRS.WebSite.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionPersister.Username))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    (new { controller = "Login", action = "Index" }));
            } else
            {
                User user = Repository<User>.GetInstance().SearchFor(u => u.Login == SessionPersister.Username).FirstOrDefault();
                CustomPrincipal cp = new CustomPrincipal(user);
                if (!cp.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                        (new { controller = "AccessDenied", action = "Index" }));
                }
            }
        }
    }
}