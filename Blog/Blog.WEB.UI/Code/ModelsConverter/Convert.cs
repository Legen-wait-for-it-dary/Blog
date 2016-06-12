using System.Collections.Generic;
using System.Linq;
using Blog.DAL;
using Blog.WEB.UI.Models;

namespace Blog.WEB.UI.Code.ModelsConverter
{
    public static class Convert
    {
        public static List<Article> ConvertArtilceEntity(IArticleRepository articleRepository, ICategoryRepository categoryRepository, IMediaFileRepository mediaFileRepository)
        {
            List<Article> convertedArticles = (from article in articleRepository.GetAllArticles()
                                             select new Article()
                                             {
                                                 ArticleId = article.ArticleID,
                                                 Title = article.Title,
                                                 BigImagePath = (article.BigImage == null) ? "" : ImageSaver.SaveImage((int) article.BigImage, mediaFileRepository.GetMediaFileById((int)article.BigImage).Data),
                                                 Category = categoryRepository.GetCategoryById(article.CategoryID).Name,
                                                 Content = article.Content,
                                                 PostedDate = article.PostedDate,
                                                 CreateDate = article.CreateDate,
                                             }).ToList();

            return convertedArticles;
        }

        public static List<Category> ConvertCategoryEntity(ICategoryRepository categoryRepository)
        {
            List<Category> categories = (from category in categoryRepository.GetAllCategories()
                                                select new Category()
                                                {
                                                    CategoryId = category.CategoryID,
                                                    Name = category.Name
                                                }).ToList();
            return categories;
        }

        public static List<Comment> ConvertCommentEntity(List<Entities.Comment> comments, IMemberRepository memberRepository, IMediaFileRepository mediaFileRepository)
        {
            List<Comment> convertedComments = (from comment in comments
                                             select new Comment()
                                             {
                                                 ArticleId = comment.ArticleID,
                                                 CommentId = comment.CommentID,
                                                 Content = comment.Content,
                                                 MemberEmail = memberRepository.GetAllMembers().First(mem => mem.MemberID == comment.MemberID).Email,
                                                 PublishDate = comment.PublishDate,
                                                 MemberAvatar = ImageSaver.SaveImage(memberRepository.GetAllMembers().First(mem => mem.MemberID == comment.MemberID).Avatar, mediaFileRepository.GetMediaFileById(memberRepository.GetAllMembers().First(mem => mem.MemberID == comment.MemberID).Avatar).Data)
                                             }).ToList();
            return convertedComments;
        }

    }
}