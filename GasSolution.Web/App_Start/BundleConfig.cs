using System.Web.Optimization;

namespace GasSolution.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                    .Include("~/Content/themes/base/all.css", new CssRewriteUrlTransform())
                    .Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/bootstrap-cosmo.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/toastr.min.css")
                    .Include("~/Scripts/sweetalert/sweet-alert.css")
                    .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                    .Include("~/css/gas.css", new CssRewriteUrlTransform())
                );

            //~/Bundles/vendor/js/top (These scripts should be included in the head of the page)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/top")
                    .Include(
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/modernizr-2.8.3.js"
                    )
                );

            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js")
                    .Include(
                        "~/Scripts/json2.min.js",

                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js",

                        "~/Scripts/bootstrap.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js"
                    )
                );

            //APPLICATION RESOURCES

            //~/Bundles/css
            bundles.Add(
                new StyleBundle("~/Bundles/css")
                    .Include("~/css/main.css")
                );

            //~/Bundles/js
            bundles.Add(
                new ScriptBundle("~/Bundles/js")
                    .Include("~/js/main.js")
                );

            #region bootstrop

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrap")
                    .Include("~/scripts/bootstrap.js")
                );
            #endregion


            #region Area / Admin 
            bundles.Add(
                new StyleBundle("~/admin/css")
                    .Include("~/admin-lte/css/AdminLTE.css")
                    .Include("~/admin-lte/css/skins/_all-skins.css")
                    .Include("~/Content/font-awesome.css")
                    .Include("~/Content/bootstrap.css")
                    .Include("~/Content/kendo/2014.1.318/kendo.common.min.css")
                    .Include("~/Content/kendo/2014.1.318/kendo.default.min.css")
                    .Include("~/Content/admin.css")
                );
            bundles.Add(
                new ScriptBundle("~/admin/jquery")
                    .Include("~/Abp/Framework/scripts/abp.js")
                    .Include("~/js/main.js")
                    .Include("~/Scripts/jquery-3.1.1.js")
                    .Include("~/admin-lte/js/adminlte.js")
                    .Include("~/Scripts/admin.common.js")
                );


            bundles.Add(
                new ScriptBundle("~/admin/kendo")
                    .Include("~/Scripts/kendo/2014.1.318/kendo.web.min.js")
                    .Include("~/Scripts/kendo/2014.1.318/cultures/kendo.culture.zh-CN.min.js")
                );
            #endregion

        }
    }
}