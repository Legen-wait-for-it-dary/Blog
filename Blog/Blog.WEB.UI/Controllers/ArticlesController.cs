using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.DAL;
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
        private readonly ICommentRepository _commentRepository;

        public ArticlesController(IArticleRepository articleRepository, ICategoryRepository categoryRepository,
                               IMediaFileRepository mediaFileRepository, ISecurityManager securityManager, ICommentRepository commentRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _securityManager = securityManager;
            _mediaFileRepository = mediaFileRepository;
            _commentRepository = commentRepository;
        }

        // GET: /Articles/
        public ActionResult Index()
        {
            if (_securityManager.IsAuthenticated)
            {
                ViewBag.memberEmail = _securityManager.CurrentUser.Identity.Name;
            }
            ViewBag.categories = Convert.ConvertCategoryEntity(_categoryRepository);
            return View();
        }

        public ActionResult ShowArticles()
        {
            return PartialView("_Articles", GetAllArticles().Where(article => article.PublishDate != null));
        }

        [ChildActionOnly]
        public List<Models.Article> GetAllArticles()
        {
            return Convert.ConvertArtilceEntity(_articleRepository, _categoryRepository, _mediaFileRepository,_commentRepository);
        }

    }
}
