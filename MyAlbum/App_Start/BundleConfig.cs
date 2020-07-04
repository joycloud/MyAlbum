using System.Web;
using System.Web.Optimization;

namespace MyAlbum
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-3.2.0.min.js",
                       "~/Scripts/jquery.fancybox.min.js",
                       "~/Scripts/eventjs.js",
                       "~/Scripts/pictools.js",
                       //"~/Scripts/aos.js",
                       "~/Scripts/owl.carousel.min.js",
                       "~/Scripts/jquery.sticky.js",
                       "~/Scripts/jquery.easing.min.js",
                       "~/Scripts/baguetteBox.min.js",
                       "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/sitebody.css",
                      "~/Content/jquery.fancybox.min.css",
                      //"~/Content/css01.css",
                      "~/Content/reset.css",
                      "~/Content/baguetteBox.css",
                      "~/Content/bootstrap-4.3.1.min.css"
                      //"~/Content/Multi-columns.css"
                      ));
        }
    }
}
