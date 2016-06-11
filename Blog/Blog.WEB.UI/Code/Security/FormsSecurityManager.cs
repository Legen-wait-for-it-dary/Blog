using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Blog.DAL;
using Blog.Entities;


namespace Blog.WEBUI.Frontend.Code.Security
{
    public class FormsSecurityManager: ISecurityManager
    {
        #region Fields

        private readonly IMemberRepository _memberRepository;

        #endregion

        #region Constructors

        public FormsSecurityManager(IMemberRepository userRepository)
        {
            this._memberRepository = userRepository;
        }

        #endregion

        #region ISecurityManager

        public bool Login(string userName, string password)
        {
            Member user = this._memberRepository.GetMember(userName, password);

            if (user == null)
            {
                return false;
            }

            this.RefreshPrincipal();

            FormsAuthentication.SetAuthCookie(userName, false);

            return true;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public bool IsAuthenticated
        {
            get 
            {
                return HttpContext.Current.User != null
                    && HttpContext.Current.User.Identity != null
                    && HttpContext.Current.User.Identity.IsAuthenticated == true;
            }
        }

        public void RefreshPrincipal()
        {
            IPrincipal incomingPrincipal =  HttpContext.Current.User;
            if (this.IsAuthenticated == true)
            {
                Member member = this._memberRepository.GetMember(incomingPrincipal.Identity.Name);
                HttpContext.Current.User = this.CreatePrincipal(member);
            }
        }

        public IPrincipal CurrentUser
        {
            get 
            {
                return HttpContext.Current.User;
            }
        }

        #endregion

        #region Helpers

        private ClaimsPrincipal CreatePrincipal(Member member)
        {
            string userName = member.Email;
            List<string> roles = new List<string>();
            roles.Add("user");
            if (member.isAdmin)
            {
                roles.Add("admin");
            }
            GenericIdentity identity = new GenericIdentity(member.Email);
            GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());

            return principal;
        }

        #endregion

    }
}