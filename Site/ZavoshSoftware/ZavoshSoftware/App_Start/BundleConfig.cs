using System.Web;
using System.Web.Optimization;

namespace ZavoshSoftware
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
             BundleTable.EnableOptimizations = true;


            bundles.Add(new StyleBundle("~/fontawesome").Include(
                "~/Assets/fontawesome/css/font-awesome.min.css"

            ));
            bundles.Add(new StyleBundle("~/Asset/Style").Include(
              
                "~/Content/customStyle.css" 
            ));

        
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/Assets/js/jquery-2.1.3.min.js",
            //    "~/Assets/js/bootstrap.min.js",
            //    "~/Assets/js/viewport/jquery.viewport.js",
            //    "~/Assets/js/menu/hoverIntent.js",
            //    "~/Assets/js/menu/superfish.js",
            //    "~/Assets/js/fancybox/jquery.fancybox.pack.js",
            //    "~/Assets/js/revolutionslider/js/jquery.themepunch.tools.min.js",
            //    "~/Assets/js/revolutionslider/js/jquery.themepunch.revolution.min.js"
            //    , "~/Assets/js/bxslider/jquery.bxslider.min.js"
            //    , "~/Assets/js/parallax/jquery.parallax-scroll.min.js"
            //    , "~/Assets/js/isotope/imagesloaded.pkgd.min.js"
            //    , "~/Assets/js/isotope/isotope.pkgd.min.js"
            //    , "~/Assets/js/placeholders/jquery.placeholder.min.js"

            //    , "~/Assets/js/validate/jquery.validate.min.js"
            //    , "~/Assets/js/submit/jquery.form.min.js"
            //    , "~/Assets/js/placeholders/jquery.placeholder.min.js"
            //    , "~/Assets/js/placeholders/jquery.placeholder.min.js"
            //    ));
            //// jQuery Validation
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/scripts/plugins/jquery-validate/jquery.validate.min.js",
            //    "~/scripts/plugins/jquery-validate-unobtrusive/jquery.validate.unobtrusive.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/themeAdminBootstrap").Include(
            //    "~/scripts/plugins/bootstrap/bootstrap.min.js"));

            //// Admin App script
            //bundles.Add(new ScriptBundle("~/bundles/themeAdmin").Include(
            //    "~/scripts/theme-admin.js"));

            //// SlimScroll
            //bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
            //    "~/scripts/plugins/slimScroll/jquery.slimscroll.min.js"));
            //// Peity
            //bundles.Add(new ScriptBundle("~/plugins/peity").Include(
            //    "~/scripts/plugins/peity/jquery.peity.min.js"));
            //// Sparkline
            //bundles.Add(new ScriptBundle("~/plugins/sparkline").Include(
            //    "~/scripts/plugins/sparkline/jquery.sparkline.min.js"));
            //// Morriss chart css styles
            //bundles.Add(new StyleBundle("~/plugins/morrisStyles").Include(
            //    "~/Content/plugins/morris/morris-0.4.3.min.css"));

            //// Morriss chart
            //bundles.Add(new ScriptBundle("~/plugins/morris").Include(
            //    "~/scripts/plugins/morris/raphael-2.1.0.min.js",
            //    "~/scripts/plugins/morris/morris.js"));
            //// Flot chart
            //bundles.Add(new ScriptBundle("~/plugins/flot").Include(
            //    "~/scripts/plugins/flot/jquery.flot.js",
            //    "~/scripts/plugins/flot/jquery.flot.tooltip.min.js",
            //    "~/scripts/plugins/flot/jquery.flot.resize.js",
            //    "~/scripts/plugins/flot/jquery.flot.pie.js",
            //    "~/scripts/plugins/flot/jquery.flot.time.js",
            //    "~/scripts/plugins/flot/jquery.flot.spline.js"));

            //// ChartJS chart
            //bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
            //    "~/scripts/plugins/chartjs/Chart.min.js"));
            //// jQuery plugins
            //bundles.Add(new ScriptBundle("~/plugins/metsiMenu").Include(
            //    "~/scripts/plugins/metisMenu/jquery.metisMenu.js"));

            //bundles.Add(new ScriptBundle("~/plugins/pace").Include(
            //    "~/scripts/plugins/pace/pace.min.js"));

            //// CSS style (bootstrap/Css)
            //bundles.Add(new StyleBundle("~/Content/themeAdminCss").Include(
            //    "~/Content/plugins/bootstrap-3.3.7/bootstrap.min.css",
            //    "~/Content/plugins/animate/animate.css",
            //    "~/Content/theme-admin.css"));
            //// Admin Theme Override styles
            //bundles.Add(new StyleBundle("~/Content/themeAdminOverrideCss").Include(
            //    "~/Content/theme-admin-override.css"));

            //// bootstrap styles
            //bundles.Add(new StyleBundle("~/bootstrapRtl").Include(
            //    "~/Content/plugins/bootstrap-rtl/bootstrap-rtl.min.css"));

            //// bootstrap fileinput styles
            //bundles.Add(new StyleBundle("~/plugins/bootstrapFileInputCss").Include(
            //    "~/Content/plugins/bootstrap-fileinput/bootstrap-fileinput.css"));

            //// bootstrap fileinput
            //bundles.Add(new ScriptBundle("~/plugins/bootstrapFileInput").Include(
            //    "~/Scripts/plugins/bootstrap-fileinput/bootstrap-fileinput.js"));

            //// Font Awesome icons
            //bundles.Add(new StyleBundle("~/font-awesome/css").Include(
            //    "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            //// summernote styles
            //bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
            //    "~/Content/plugins/summernote/summernote.css",
            //    "~/Content/plugins/summernote/summernote-bs3.css"));
            //// summernote 
            //bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
            //    "~/scripts/plugins/summernote/summernote.min.js"));

            //// Application CSS style (bootstrap/Css)
            //bundles.Add(new StyleBundle("~/Content/applicationCss").Include(
            //    "~/Content/theme-style.css",
            //    "~/Content/style.css"));
            //// dataPicker styles
            //bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
            //    "~/Content/plugins/datapicker/datepicker3.css"));

            //// dataPicker 
            //bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
            //    "~/scripts/plugins/datapicker/bootstrap-datepicker.js"));
            ////*****************************************************
            //// Main Layout
            //bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
            //    "~/scripts/plugins/datapicker/bootstrap-datepicker.js"));

            //*****************************************************
            //Theme 2nd Generation
            // Main Layout and Application Bundles

            //bundles.Add(new ScriptBundle("~/themeBaseScripts").Include(
            //    "~/Scripts/plugins/jquery-1.11.3/jquery-1.11.3.min.js",
            //    "~/Scripts/plugins/swiper/swiper.min.js",
            //    "~/Scripts/plugins/nstSlider/jquery.nstSlider.min.js",
            //    "~/Scripts/theme.js"
            //));
            // End: Main Layout and Application Bundles
        }
    }
}