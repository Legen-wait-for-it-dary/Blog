using System;
using System.Collections.Generic;
using System.EnterpriseServices;
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

        // GET: /Article/
        public ActionResult Index(int id)
        {
            if (_securityManager.IsAuthenticated)
            {
                ViewBag.memberEmail = _securityManager.CurrentUser.Identity.Name;
                Member member = _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name);
                ViewBag.memberAvatar = _mediaFileRepository.GetMediaFileById(member.UserPhoto).FileName;
            }

            if (_articleRepository.GetAllArticles().Exists(a => a.ArticleId == id))
            {

                ViewBag.categories = Code.ModelsConverter.Convert.ConvertCategoryEntity(_categoryRepository);
                ViewBag.numOfComments =
                    _commentRepository.GetAllComments().Where(com => com.ArticleId == id).ToList().Count;
                ViewBag.article =
                    Code.ModelsConverter.Convert.ConvertArtilceEntity(_articleRepository, _categoryRepository,
                        _mediaFileRepository, _commentRepository).First(art => art.ArticleId == id);
                return View();
            }
            return RedirectToAction("PageNotFound", "Error");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(string content, string articleId)
        {
            int iArticleId;
            if (string.IsNullOrEmpty(content))
            {
                return Json(new { error = "Can't post empty comment" });
            }

            if (string.IsNullOrEmpty(articleId) || !int.TryParse(articleId, out iArticleId))
            {
                return Json(new { error = "An error occured while adding comment" });
            }

            _commentRepository.AddComment(new Comment()
            {
                Content = content,
                PublishDate = DateTime.Now,
                MemberId = _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).MemberId,
                ArticleId = int.Parse(articleId)
            });

            return Json(new
            {
                srcOfMemberAvatar = "/Images/" + _mediaFileRepository.GetMediaFileById(_memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).UserPhoto).FileName,
                memberEmail = _securityManager.CurrentUser.Identity.Name,
                publishDate = DateTime.Now.ToLongDateString()
            });

        }

        public ActionResult ShowComments(int articleId)
        {
            return PartialView("_Comments", Code.ModelsConverter.Convert.ConvertCommentEntity(
                    _commentRepository.GetAllCommentsByArticleId(articleId), _memberRepository, _mediaFileRepository));
        }
    }
}
