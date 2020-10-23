using System.Web;
using System.Web.Optimization;

namespace RDDStaffPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));


            
            bundles.Add(new ScriptBundle("~/JS/LoginHeaderJs").Include(
            "~/js/plugin/webfont/webfont.min.js",
            "~/js/jquery-1.2.6.min.js"));

            bundles.Add(new ScriptBundle("~/JS/LoginFooterJs").Include(
            "~/js/core/jquery.3.2.1.min.js",
            "~/Scripts/sweetalret/sweetalert2.all.min.js",
            "~/Scripts/RedDotUtility.js"));

            bundles.Add(new StyleBundle("~/CSS/logincss").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/atlantis.min.css",
                     "~/Content/fonts.min.css",
                     "~/Content/reddot.css"));


            //bundles.Add(new StyleBundle("~/CSS/rddcss").Include(
            //          "~/Content/bootstrap.min.css",
            //          "~/Content/atlantis.min.css",
            //          "~/Content/fonts.min.css",
            //          "~/Scripts/multiselect/bootstrap-multiselect.css",
            //          "~/Scripts/multiselect/multiselect.css",
            //          "~/Content/reddot.css",
            //          "~/Content/jquery-ui.css",
            //          "~/Scripts/datatables/css/jquery.dataTables.min.css",
            //          "~/Scripts/datatables/css/dataTables.bootstrap.min.css",
            //          "~/Scripts/datatables/css/dataTables.bootstrap4.min.css",
            //          "~/Scripts/datatables/css/dataTables.jqueryui.min.css",
            //          "~/Scripts/datatables/css/buttons.dataTables.min.css",
            //          "~/Scripts/sweetalret/sweetalert2.min.css",
            //          "~/Scripts/dragdrop/main.css",
            //          "~/Scripts/datapicker-separate/daterangepicker.css"));


            BundleTable.EnableOptimizations = true;

        }
    }
}
