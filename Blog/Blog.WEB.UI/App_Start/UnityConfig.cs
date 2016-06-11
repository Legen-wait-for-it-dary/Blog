using System.Configuration;
using System.Web.Mvc;
using Blog.DAL;
using Blog.WEB.UI.Code.Security;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace Blog.WEB.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            string connectionString = ConfigurationManager.ConnectionStrings["db_blog_con_str"].ConnectionString;

            container.RegisterType<IMemberRepository, MemberRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ICommentRepository, CommentRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<IArticleRepository, ArticleRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ICategoryRepository, CategoryRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<IMediaFileRepository, MediaFileRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ISecurityManager, FormsSecurityManager>(
                    new InjectionConstructor(new ResolvedParameter<IMemberRepository>()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}