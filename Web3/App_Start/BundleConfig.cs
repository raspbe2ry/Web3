﻿using System.Web;
using System.Web.Optimization;

namespace Web3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/select2.js",
                        "~/Scripts/notify.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/jsTree3/jstree.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/simpleJS").Include(
                        "~/Scripts/moment.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.plugins.js",
                        "~/Scripts/DataTables/extendDatatables.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jsTree/themes/default/style.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                "~/Content/DataTables/css/dataTables.bootstrap.css",
                "~/Content/DataTables/css/dataTables.jqueryui.css",
                "~/Content/DataTables/css/jquery.dataTables_themeroller.css"
                ));

        }
    }
}
