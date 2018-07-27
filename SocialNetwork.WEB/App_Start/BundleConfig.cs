using System.Web;
using System.Web.Optimization;

namespace SocialNetwork
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js", "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            //-------------------------------------------------------------------------------------------------------------------------------------------//

            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include("~/Scripts/jquery.signalR-2.2.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/LogReg").Include("~/Scripts/scripts.css/logreg-ccs.js"));

            bundles.Add(new ScriptBundle("~/bundles/Friends").Include(
                "~/Scripts/scripts.css/friends-ccs.js",
                "~/Scripts/scripts.server/friends-server.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/User").Include("~/Scripts/scripts.css/userpage-css.js", "~/Scripts/scripts.server/userPage.js"));

            bundles.Add(new ScriptBundle("~/bundles/Messages").Include(
                "~/Scripts/scripts.css/messages-css.js",
                "~/Scripts/scripts.server/messages-server.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Global").Include(
                "~/Scripts/scripts.css/header-ccs.js", 
                "~/Scripts/scripts.server/menu-server.js",
                "~/Scripts/scripts.server/signalR.js"
                ));

            //-------------------------------------------------------------------------------------------------------------------------------------------//

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/font-awesome/css/font-awesome.min.css",
                "~/Content/MyCSS/site.css"
            ));
        
            bundles.Add(new StyleBundle("~/Content/Messages").Include("~/Content/MyCSS/messages.css"));
            bundles.Add(new StyleBundle("~/Content/LogReg").Include("~/Content/MyCSS/logreg.css"));
            bundles.Add(new StyleBundle("~/Content/Friends").Include("~/Content/MyCSS/friendslist.css"));
            bundles.Add(new StyleBundle("~/Content/User").Include("~/Content/MyCSS/userpage.css"));
        }
    }
}
