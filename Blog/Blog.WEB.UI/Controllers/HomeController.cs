using System.Web.Mvc;
//using Blog.DAL;
using Blog.WEB.UI.Code.Security;

namespace Blog.WEB.UI.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IMemberRepository _memberRepository;
        private readonly ISecurityManager _securityManager;

        public HomeController(/*IMemberRepository memberRepository, */ISecurityManager securityManager)
        {
            //_memberRepository = memberRepository;
            _securityManager = securityManager;
        }
        
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string email, string password)
        {
            object userEmail = null;

            if (_securityManager.Login(email, password))
            {
                userEmail = _securityManager.CurrentUser.Identity.Name;
            }

            return Json(userEmail);
        }

        public ActionResult SignUp()
        {
            return null;
        }
    }
}
