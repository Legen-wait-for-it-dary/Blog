using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WEBUI.Frontend.Code.Security
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
