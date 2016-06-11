using Blog.Entities;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace Blog.DAL
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly string _connectionString;

        public ArticleRepository() { }

        public ArticleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Article> GetAllArticles()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                var articlesObjectSet = context.CreateObjectSet<Article>();
                return articlesObjectSet.ToList();
            }
        }


        public void UpdateArticle(Article article)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Article> articlesObjectSet = context.CreateObjectSet<Article>();
                var articleForUpdate = articlesObjectSet.FirstOrDefault(
                    art => art.ArticleID == article.ArticleID);

                if (articleForUpdate != null)
                {
                    if (articleForUpdate.MemberID != article.MemberID)
                        articleForUpdate.MemberID = article.MemberID;

                    if (articleForUpdate.CategoryID != article.CategoryID)
                        articleForUpdate.CategoryID = article.CategoryID;

                    if (articleForUpdate.Content != article.Content)
                        articleForUpdate.Content = article.Content;

                    if (articleForUpdate.CreateDate != article.CreateDate)
                        articleForUpdate.CreateDate = article.CreateDate;

                    if (articleForUpdate.PostedDate != article.PostedDate)
                        articleForUpdate.PostedDate = article.PostedDate;

                    if (articleForUpdate.Title != article.Title)
                        articleForUpdate.Title = article.Title;

                    if (articleForUpdate.BigImage != article.BigImage)
                        articleForUpdate.BigImage = article.BigImage;

                    if (articleForUpdate.isPublished != article.isPublished)
                        articleForUpdate.isPublished = article.isPublished;

                    context.SaveChanges();
                }
            }
        }

        public void AddArticle(Article article)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Article> articlesObjectSet = context.CreateObjectSet<Article>();
                articlesObjectSet.AddObject(article);
                context.SaveChanges();
            }
        }



        public void DeleteArticle(int idOfArticle)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Article> articlesObjectSet = context.CreateObjectSet<Article>();
                articlesObjectSet.DeleteObject(articlesObjectSet.Single(art => art.ArticleID == idOfArticle));
                context.SaveChanges();
            }
        }
    }
}
