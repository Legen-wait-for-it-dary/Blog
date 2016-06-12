using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.DAL;
using Blog.WEB.UI.Code;
using Blog.WEB.UI.Code.ModelsConverter;
using Blog.WEB.UI.Code.Security;

namespace Blog.WEB.UI.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISecurityManager _securityManager;
        private readonly IMediaFileRepository _mediaFileRepository;

        public ArticlesController(IArticleRepository articleRepository, ICategoryRepository categoryRepository,
                               IMediaFileRepository mediaFileRepository, ISecurityManager securityManager)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _securityManager = securityManager;
            _mediaFileRepository = mediaFileRepository;
        }

        //
        // GET: /Articles/

        public ActionResult Index()
        {

            if (_securityManager.IsAuthenticated)
            {
                ViewBag.email = _securityManager.CurrentUser.Identity.Name;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ShowCategories()
        {
            return PartialView("_Categories", GetAllCategories());
        }

        public ActionResult ShowArticles()
        {
            return PartialView("_Articles", GetAllArticles());
        }


        [ChildActionOnly]
        public List<Models.Category> GetAllCategories()
        {
            return Convert.ConvertCategoryEntity(_categoryRepository);
        }

        [ChildActionOnly]
        public List<Models.Article> GetAllArticles()
        {
            return Convert.ConvertArtilceEntity(_articleRepository, _categoryRepository, _mediaFileRepository);
        }

    }
}
