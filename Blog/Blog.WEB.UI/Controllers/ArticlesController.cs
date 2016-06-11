using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Blog.DAL;
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

        public string GetMediaFilePath(int mediaFileId)
        {
            using (var ms = new MemoryStream(_mediaFileRepository.GetMediaFileById(mediaFileId).Data))
            {
                using (var img = Image.FromStream(ms))
                {
                    string extension = "";
                    if (img.RawFormat.Equals(ImageFormat.Bmp)) extension = "bmp";
                    if (img.RawFormat.Equals(ImageFormat.Gif)) extension = "gif";
                    if (img.RawFormat.Equals(ImageFormat.Icon)) extension = "vnd.microsoft.icon";
                    if (img.RawFormat.Equals(ImageFormat.Jpeg)) extension = "jpeg";
                    if (img.RawFormat.Equals(ImageFormat.Png)) extension = "png";
                    if (img.RawFormat.Equals(ImageFormat.Tiff)) extension = "tiff";
                    if (img.RawFormat.Equals(ImageFormat.Wmf)) extension = "wmf";

                    string fileName = mediaFileId + "." + extension;
                    string path = Server.MapPath("/Images/" + fileName);

                    using (var fs = new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)))
                    {
                        fs.Write(_mediaFileRepository.GetMediaFileById(mediaFileId).Data);
                        fs.Close();
                    }
                    return fileName;
                }
            }
        }

        [ChildActionOnly]
        public List<Models.Category> GetAllCategories()
        {
            List<Models.Category> categories = (from category in _categoryRepository.GetAllCategories()
                                                select new Models.Category()
                                                {
                                                    CategoryId = category.CategoryID,
                                                    Name = category.Name
                                                }).ToList();
            return categories;
        }

        [ChildActionOnly]
        public List<Models.Article> GetAllArticles()
        {
            List<Models.Article> articles = (from article in _articleRepository.GetAllArticles()
                                             select new Models.Article()
                                                                                              {
                                                                                                  ArticleId = article.ArticleID,
                                                                                                  Title = article.Title,
                                                                                                  BigImagePath = (article.BigImage == null) ? "" : GetMediaFilePath((int)article.BigImage),
                                                                                                  Category = _categoryRepository.GetCategoryById(article.CategoryID).Name,
                                                                                                  Content = article.Content,
                                                                                                  PostedDate = article.PostedDate,
                                                                                                  CreateDate = article.CreateDate,
                                                                                              }).ToList();

            return articles;
        }

    }
}
