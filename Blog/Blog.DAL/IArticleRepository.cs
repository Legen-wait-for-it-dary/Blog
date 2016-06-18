using Blog.Entities;
using System.Collections.Generic;

namespace Blog.DAL
{
    public interface IArticleRepository
    {
        List<Article> GetAllArticles();
        void UpdateArticle(Article article);
        void AddArticle(Article article);
        void DeleteArticle(int articleId);
    }
}
