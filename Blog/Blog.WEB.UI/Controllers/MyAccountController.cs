using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.DAL;
using Blog.Entities;
using Blog.WEB.UI.Code;
using Blog.WEB.UI.Code.Security;
using Newtonsoft.Json;
using Article = Blog.WEB.UI.Models.Article;

namespace Blog.WEB.UI.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISecurityManager _securityManager;
        private readonly IMemberRepository _memberRepository;
        private readonly IMediaFileRepository _mediaFileRepository;

        public MyAccountController(IMemberRepository memberRepository, IArticleRepository articleRepository, ICategoryRepository categoryRepository,
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
        // GET: /MyAccount/

        public ActionResult Index()
        {
            if (_securityManager.IsAuthenticated)
            {
                ViewBag.email = _securityManager.CurrentUser.Identity.Name;
                ViewBag.numberOfPublishedPosts = _articleRepository.GetAllArticles().Count(art => art.PublishDate != null && art.MemberId == _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).MemberId);
                ViewBag.numberOfRoughCopies = _articleRepository.GetAllArticles().Count(art => art.PublishDate == null && art.MemberId == _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).MemberId);

                return View();
            }
            return RedirectToAction("Index", "Home");

        }

        // GET: /MyAccount/GetMyPublishedPosts
        [HttpGet]
        public ActionResult GetMyPublishedPosts()
        {
            List<Article> articleList = Code.ModelsConverter.Convert.ConvertArtilceEntity(_articleRepository, _categoryRepository,
                _mediaFileRepository)
                .Where(
                    art =>
                        art.PublishDate != null &&
                        art.MemberId == _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).MemberId).ToList();
            string response = null;
            try
            {
                response = JsonConvert.SerializeObject(articleList.ToDictionary(a => a.ArticleId, s => s.Title));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            
            return Json(new { data = response },JsonRequestBehavior.AllowGet); 
        }

        // GET: /MyAccount/GetMyRoughCopies
        [HttpGet]
        public ActionResult GetMyRoughCopies()
        {
            List<Article> articleList = Code.ModelsConverter.Convert.ConvertArtilceEntity(_articleRepository, _categoryRepository,
                _mediaFileRepository)
                .Where(
                    art =>
                        art.PublishDate == null &&
                        art.MemberId == _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).MemberId).ToList();
            string response = null;
            try
            {
                response = JsonConvert.SerializeObject(articleList.ToDictionary(a => a.ArticleId, s => s.Title));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }

            return Json(new { data = response }, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult ShowAddArticle()
        {
            return PartialView("_AddNewArticle", Code.ModelsConverter.Convert.ConvertCategoryEntity(_categoryRepository));
        }

        // POST: /MyAccount/DeleteArticle
        [HttpPost]
        public ActionResult DeleteArticle(int articleId)
        {
            if (_articleRepository.GetAllArticles().Exists(art => art.ArticleId == articleId))
            {
                var idOfBigImageForDelete = _articleRepository.GetAllArticles().FirstOrDefault(art => art.ArticleId == articleId).ArticleCover;
                if (idOfBigImageForDelete != null)
                {
                    _mediaFileRepository.DeleteMediaFile((int)idOfBigImageForDelete);
                }
                _articleRepository.DeleteArticle(articleId);
            }
            return Json(new object());
        }

        // POST: /MyAccount/UploadArticleCoverImage
        [HttpPost]
        public ActionResult UploadArticleCoverImage(HttpPostedFileBase files)
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var picture = System.Web.HttpContext.Current.Request.Files["ArticleCover"];

                if (picture != null)
                    using (var binaryReader = new BinaryReader(picture.InputStream))
                    {
                        var nextIndenitityForMediaFile = _mediaFileRepository.GetNextIndenitityForMediaFile();
                        if (nextIndenitityForMediaFile != null)
                            _mediaFileRepository.AddMediaFile(new MediaFile()
                            {
                                FileName = ImageSaver.SaveImage((int)nextIndenitityForMediaFile, binaryReader.ReadBytes(picture.ContentLength))
                            });
                    }
            }

            return Json(new { mediaFileId = _mediaFileRepository.GetNextIndenitityForMediaFile() - 1 });
        }

        // POST: /MyAccount/UploadArticle
        [HttpPost]
        public ActionResult UploadArticle(string formattedText, string mediaFileId, string title, string category, bool iSForPublishing)
        {
            var decodedText = Server.UrlDecode(formattedText);
            var categoryOfArticle = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Name == category);
            if (categoryOfArticle != null)
            {
                int iMediaFileId;
                _articleRepository.AddArticle(new Entities.Article()
            {
                Title = title,
                Content = decodedText,
                MemberId = _memberRepository.GetMember(_securityManager.CurrentUser.Identity.Name).MemberId,
                PublishDate = (iSForPublishing)?DateTime.Now:(DateTime?) null,
                CategoryId = categoryOfArticle.CategoryId,
                ArticleCover = (int.TryParse(mediaFileId, out iMediaFileId))?(int?)iMediaFileId:null
            });
            }
            return Json(new object());
        }
    }
}
