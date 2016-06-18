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
                    art => art.ArticleId == article.ArticleId);

                if (articleForUpdate != null)
                {
                    if (articleForUpdate.MemberId != article.MemberId)
                        articleForUpdate.MemberId = article.MemberId;

                    if (articleForUpdate.CategoryId != article.CategoryId)
                        articleForUpdate.CategoryId = article.CategoryId;

                    if (articleForUpdate.Content != article.Content)
                        articleForUpdate.Content = article.Content;

                    if (articleForUpdate.PublishDate != article.PublishDate)
                        articleForUpdate.PublishDate = article.PublishDate;

                    if (articleForUpdate.Title != article.Title)
                        articleForUpdate.Title = article.Title;

                    if (articleForUpdate.ArticleCover != article.ArticleCover)
                        articleForUpdate.ArticleCover = article.ArticleCover;

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



        public void DeleteArticle(int articleId)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Article> articlesObjectSet = context.CreateObjectSet<Article>();
                articlesObjectSet.DeleteObject(articlesObjectSet.Single(art => art.ArticleId == articleId));
                context.SaveChanges();
            }
        }
    }
}
