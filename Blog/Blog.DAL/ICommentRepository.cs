using Blog.Entities;
using System.Collections.Generic;

namespace Blog.DAL
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        List<Comment> GetAllCommentsByArticleId(int articleId);
        void AddComment(Comment comment);
    }
}
