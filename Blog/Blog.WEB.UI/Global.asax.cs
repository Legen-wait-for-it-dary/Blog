using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blog.DAL;
using Blog.WEB.UI.Code.Security;
using NLog;


namespace Blog.WEB.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.RegisterComponents();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["db_blog_con_str"].ConnectionString;
            IMemberRepository userRepository = new MemberRepository(connectionString);
            ISecurityManager securityManager = new FormsSecurityManager(userRepository);

            securityManager.RefreshPrincipal();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Response.Clear();
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Log(LogLevel.Error, ex.ToString());

            string action = "InternalServerError";
            if (ex is HttpException)
            {
                HttpException hex = ex as HttpException;
                if (hex.GetHttpCode() == 404)
                {
                    action = "PageNotFound";
                }
            }
            Response.Redirect(String.Format("~/Error/{0}", action));
        }
    }
}