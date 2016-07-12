using System.Web.Mvc;
using Blog.DAL;
using Blog.Entities;
using Blog.WEB.UI.Code.Security;

namespace Blog.WEB.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ISecurityManager _securityManager;

        public HomeController(IMemberRepository memberRepository, ISecurityManager securityManager)
        {
            _memberRepository = memberRepository;
            _securityManager = securityManager;
        }
        
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Models.LoginMemberModel member)
        {
            if (ModelState.IsValid)
            {
                if (_securityManager.Login(member.Email, member.Password))
                {
                    return RedirectToAction("Index", "Articles");
                }
            }
            ModelState.AddModelError("","Incorrect login or password");
            return View("Index");
        }

        public ActionResult Logout()
        {
            if (_securityManager.IsAuthenticated)
            {
                _securityManager.Logout();
            }
            return View("Index");
        }

        public ActionResult SignUp(Models.RegisterMemberModel member)
        {
            if (ModelState.IsValid)
            {
                if (!_memberRepository.GetAllMembers().Exists(m => m.Email == member.Email))
                {
                    _memberRepository.AddMember(new Member()
                    {
                        Email = member.Email,
                        Password = member.Password,
                        IsAdmin = false,
                        IsEnabled = true,
                        UserPhoto = 1
                    });

                    if (_securityManager.Login(member.Email, member.Password))
                    {
                        return RedirectToAction("Index", "Articles");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User with this email already exists");
                }
            }
            else
            {
                ModelState.AddModelError("", "Registration data is invalid");
            }
            return View("Index");
        }
    }
}
