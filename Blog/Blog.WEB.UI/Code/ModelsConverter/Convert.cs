using System.Collections.Generic;
using System.Linq;
using Blog.DAL;
using Blog.WEB.UI.Models;

namespace Blog.WEB.UI.Code.ModelsConverter
{
    public static class Convert
    {
        public static List<Article> ConvertArtilceEntity(IArticleRepository articleRepository, ICategoryRepository categoryRepository, IMediaFileRepository mediaFileRepository, ICommentRepository commentRepository)
        {
            var convertedArticles = (from article in articleRepository.GetAllArticles()
                                             select new Article()
                                             {
                                                 ArticleId = article.ArticleId,
                                                 Title = article.Title,
                                                 ArticleCover = (article.ArticleCover == null) ? "" : mediaFileRepository.GetMediaFileById((int)article.ArticleCover).FileName,
                                                 Category = categoryRepository.GetCategoryById(article.CategoryId).Name,
                                                 Content = article.Content,
                                                 PublishDate = article.PublishDate,
                                                 MemberId = article.MemberId,
                                                 NumberOfComments = commentRepository.GetAllCommentsByArticleId(article.ArticleId).Count
                                             }).ToList();

            return convertedArticles;
        }

        public static List<Category> ConvertCategoryEntity(ICategoryRepository categoryRepository)
        {
            List<Category> categories = (from category in categoryRepository.GetAllCategories()
                                                select new Category()
                                                {
                                                    CategoryId = category.CategoryId,
                                                    Name = category.Name
                                                }).ToList();
            return categories;
        }

        public static List<Comment> ConvertCommentEntity(List<Entities.Comment> comments, IMemberRepository memberRepository, IMediaFileRepository mediaFileRepository)
        {
            List<Comment> convertedComments = (from comment in comments
                                             select new Comment()
                                             {
                                                 ArticleId = comment.ArticleId,
                                                 CommentId = comment.CommentId,
                                                 Content = comment.Content,
                                                 MemberEmail = memberRepository.GetAllMembers().First(mem => mem.MemberId == comment.MemberId).Email,
                                                 PublishDate = comment.PublishDate,
                                                 MemberAvatar =  mediaFileRepository.GetMediaFileById(memberRepository.GetAllMembers().First(mem => mem.MemberId == comment.MemberId).UserPhoto).FileName
                                             }).ToList();
            return convertedComments;
        }

    }
}