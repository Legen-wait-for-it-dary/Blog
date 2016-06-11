using System.Security.Principal;

namespace Blog.WEB.UI.Code.Security
{
    public interface ISecurityManager
    {
        bool Login(string userName, string password);
        void Logout();
        bool IsAuthenticated { get; }
        IPrincipal CurrentUser { get; }
        void RefreshPrincipal();
    }
}
