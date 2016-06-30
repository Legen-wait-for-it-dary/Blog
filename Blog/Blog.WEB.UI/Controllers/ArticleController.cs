using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blog.DAL;
using Blog.Entities;
using Blog.WEB.UI.Code.Security;

namespace Blog.WEB.UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISecurityManager _securityManager;
        private readonly IMemberRepository _memberRepository;
        private readonly IMediaFileRepository _mediaFileRepository;

        public ArticleController(IMemberRepository memberRepository, IArticleRepository articleRepository, ICategoryRepository categoryRepository,
                              ICommentRepository commentRepository, IMediaFileRepository mediaFileRepository, ISecurityManager securityManager)
        {
            _memberRepository = memberRepository;
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _securityManager = securityManager;
            _mediaFileRepository = mediaFileRepository;
        }

        //
        // GET: /Article/

        [HandleError(ExceptionType = typeof(InvalidOperationException), View = "PageNotFound")]
        public ActionResult Index(int id)
        {
            if (_securityManager.IsAuthenticated)
            {
                ViewBag.email = _securityManager.CurrentUser.Identity.Name;
                ViewBag.article = GetAllArticles().First(art => art.ArticleId == id);

                    Member member = _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name);
                    ViewBag.memberAvatar = _mediaFileRepository.GetMediaFileById(member.UserPhoto).FileName;
                    ViewBag.numOfComments = _commentRepository.GetAllComments().Where(com => com.ArticleId == id).ToList().Count;
                    return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");    
            }
        }

        [HttpPost]
        public ActionResult AddComment(string content, string articleId)
        {
            _commentRepository.AddComment(new Comment()
            {
                Content = content,
                PublishDate = DateTime.Now,
                MemberId = _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).MemberId,
                ArticleId = int.Parse(articleId)
            });

            return Json(new
            {
                srcOfMemberAvatar = "src",
                memberEmail = _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).Email,
                publishDate = DateTime.Now.ToLongDateString()
            });
        }

        public ActionResult ShowCategories()
        {
            return PartialView("_Categories", GetAllCategories());
        }

        public ActionResult ShowComments(int articleId)
        {
            return PartialView("_Comments", GetAllCommentsByArticleId(articleId));
        }

        [ChildActionOnly]
        public List<Models.Category> GetAllCategories()
        {
            return Code.ModelsConverter.Convert.ConvertCategoryEntity(_categoryRepository);
        }

        [ChildActionOnly]
        public List<Models.Article> GetAllArticles()
        {
            return Code.ModelsConverter.Convert.ConvertArtilceEntity(_articleRepository, _categoryRepository, _mediaFileRepository, _commentRepository);
        }

        [ChildActionOnly]
        public List<Models.Comment> GetAllCommentsByArticleId(int articleId)
        {
            return
                Code.ModelsConverter.Convert.ConvertCommentEntity(
                    _commentRepository.GetAllCommentsByArticleId(articleId), _memberRepository, _mediaFileRepository);

        }

    }
}
