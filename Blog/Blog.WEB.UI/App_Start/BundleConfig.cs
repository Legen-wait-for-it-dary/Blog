using System.Web.Optimization;

namespace Blog.WEB.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/commonStyles")
                .Include("~/Content/Libs/bootstrap-3.3.6/bootstrap.min.css")
                .Include("~/Content/Common/common.css"));

            bundles.Add(new ScriptBundle("~/bundles/commonScripts")
                .Include("~/Scripts/Libs/jquery/jquery.min.js")
                .Include("~/Scripts/Libs/bootstrap-3.3.6/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/Libs/jquery/jquery.validate*"));

            BundleTable.EnableOptimizations = true;
        }
    }
}