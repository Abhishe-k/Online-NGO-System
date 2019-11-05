using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trial3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

     /*       routes.MapRoute(
"ViewEvents",
"ViewEvents/Create/{id}/{id1}",
new
{
controller = "ViewEvents",
action = "Create",
id = UrlParameter.Optional,
id1 = UrlParameter.Optional
});*/


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
         
        }
    }
}
