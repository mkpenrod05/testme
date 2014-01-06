using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using testme;
using testme.Models;

namespace testme
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        public void Session_OnStart()
        {
            //Create the user session here.
            User user = new User();
            Session["user"] = user;

            //After the user logs in, set the attributes of the user.
            //This will happen on another page.
            User LoggedInUser = new User();
            LoggedInUser = (User)Session["user"];


        }

        public void Session_OnEnd()
        {

        }
    }
}
