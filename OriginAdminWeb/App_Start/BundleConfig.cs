using System.Web.Optimization;

namespace OriginAdminWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region base js css
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-1.10.2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-form").Include("~/Scripts/jquery.form.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-2.6.2.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new StyleBundle("~/Content/adminlte").Include("~/Content/AdminLTE/css/AdminLTE.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/adminlte").Include("~/Scripts/AdminLTE/adminlte.js"));
            bundles.Add(new StyleBundle("~/Content/easyui").Include("~/Scripts/jquery.easyui/easyui.css"));
            bundles.Add(new ScriptBundle("~/Scripts/easyui").Include("~/Scripts/jquery.easyui/jquery.easyui.min.js",
                "~/Scripts/jquery.easyui/easyui-lang-zh_CN.js"));
            #endregion

            #region plugins 

            bundles.Add(new StyleBundle("~/Content/select2").Include("~/Content/select2/select2.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/select2").Include("~/Scripts/select2/select2.min.js"));
            #endregion
            bundles.Add(new StyleBundle("~/Content/ListStyle").Include("~/Content/ListStyle.css"));
            bundles.Add(new ScriptBundle("~/bundles/base-modal").Include(
                        "~/Scripts/base-modal.js"));
            bundles.Add(new StyleBundle("~/Content/font").Include(
"~/Content/css/font-awesome.css", "~/Content/css/ionicons.css"));



            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
    "~/Scripts/moment-with-locales.min.js"));


            bundles.Add(new StyleBundle("~/Content/bootstrap-dialog").Include(
    "~/Content/bootstrap-dialog.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-dialog").Include(
                "~/Scripts/bootstrap-dialog.js"));

            bundles.Add(new StyleBundle("~/Content/daterangepicker").Include(
                "~/Content/daterangepicker*"));

            bundles.Add(new ScriptBundle("~/bundles/daterangepicker").Include(
                "~/Scripts/daterangepicker.js", "~/Scripts/daterangepicker-lang-zh_CN.js"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/fancybox").Include("~/Scripts/plugins/fancyBox/jquery.fancybox.css"));
            bundles.Add(new ScriptBundle("~/Scripts/fancybox").Include("~/Scripts/plugins/fancyBox/jquery.fancybox.js"));
            bundles.Add(new StyleBundle("~/Content/skins/skin-blue").Include("~/Content/AdminLTE/css/skins/skin-blue.min.css"));
            #region bootstrap plugins
            bundles.Add(new StyleBundle("~/Content/bootstrap-treeview").Include("~/Scripts/bootstrap-treeview/bootstrap-treeview.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-treeview").Include("~/Scripts/bootstrap-treeview/bootstrap-treeview.min.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap-table").Include("~/Content/bootstrap-table/bootstrap-table.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-table").Include(
                "~/Scripts/bootstrap-table/bootstrap-table.min.js",
                "~/Scripts/bootstrap-table/bootstrap-table-locale-all.min.js",
                "~/Scripts/bootstrap-table/extensions/editable/bootstrap-table-editable.min.js",
                "~/Scripts/bootstrap-table/extensions/export/bootstrap-table-export.min.js",
                 "~/Scripts/tableExport/tableExport.min.js"
                ));
            #endregion
        }
    }
}
