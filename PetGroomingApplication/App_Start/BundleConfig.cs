using System.Web;
using System.Web.Optimization;

namespace PetGroomingApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Jquery validator & unobstrusive ajax
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/bino/jquery.magnific-popup.js",
                    "~/Scripts/bino/jquery.mixitup.min.js",
                    "~/Scripts/bino/jquery.easing.1.3.js",
                    "~/Scripts/bino/jquery.masonry.min.js",
                    "~/Content/themes/bino/css/slick/slick.js",
                    "~/Content/themes/bino/css/slick/slick.min.js",
                    "~/Scripts/bino/plugins.js",
                    "~/Scripts/bino/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/themes/bino/css/iconfont.css",
                    "~/Content/themes/bino/css/slick/slick.css",
                    "~/Content/themes/bino/css/slick/slick-theme.css",
                    "~/Content/themes/bino/css/font-awesome.min.css",
                    "~/Content/themes/bino/css/jquery.fancybox.css",
                    "~/Content/themes/bino/css/bootstrap.css",
                    "~/Content/themes/bino/css/bootstrap.min.css",
                    "~/Content/themes/bino/css/magnific-popup.css",
                    "~/Content/themes/bino/css/plugins.css",
                    "~/Content/themes/bino/css/style.css",
                    "~/Content/themes/bino/css/responsive.css"));
        }
    }
}
