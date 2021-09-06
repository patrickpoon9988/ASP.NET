using System.Web;
using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-3.4.1.js",
                        "~/Scripts/jquery.validate*"
                        ,"~/Scripts/vue.js"
                        ,"~/Scripts/vue.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));
        }
    }
}
