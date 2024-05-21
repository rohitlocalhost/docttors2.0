using Docttors_portal.DependencyResolution.DependencyResolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Docttors_portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyRegistrar.StructureMapper.Run();
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 60;    //Clean-up Code
        }
        protected void Session_End(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
        }

        /// <summary>
        /// Check URL and redirect URL according to the match case
        /// Commented to run in local
        /// Uncomment to run on live server of skytax
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //string url = Request.Url.ToString();

            //if (url == UrlResourceHelper.SkytaxWithoutSSL || url == UrlResourceHelper.SkytaxWithSSL || url == UrlResourceHelper.SkytaxWithWWW)
            //{
            //    Response.Redirect(UrlResourceHelper.ApplicationURL);
            //}
        }
    }
}
