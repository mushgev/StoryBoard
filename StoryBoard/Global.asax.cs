using StoryBoard.Business;
using StoryBoard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StoryBoard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var logic = DependencyResolver.Current.GetService<UserLogic>();

                var name = User.Identity.Name;
                var user = logic.Get(name);

                var identity = new StoryBoardIdentity(user.UserId, user.Name);
                var principal = new StoryBoardPrincipal(identity);

                HttpContext.Current.User = principal;
            }
        }
    }
}
