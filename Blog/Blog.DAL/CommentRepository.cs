using Blog.Entities;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace Blog.DAL
{
    public class CommentRepository:ICommentRepository
    {
        private readonly string _connectionString;

        public CommentRepository() { }

        public CommentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public List<Comment> GetAllComments()
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Comment> commentsObjectSet = context.CreateObjectSet<Comment>();

                return commentsObjectSet.ToList();
            }
        }

        public List<Comment> GetAllCommentsByArticleId(int articleId)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Comment> commentsObjectSet = context.CreateObjectSet<Comment>();
                return commentsObjectSet.Where(com => com.ArticleId == articleId).ToList();
            }
        }

        public void AddComment(Comment comment)
        {
            using (ObjectContext context = new ObjectContext(_connectionString))
            {
                ObjectSet<Comment> commentsObjectSet = context.CreateObjectSet<Comment>();
                commentsObjectSet.AddObject(comment);
                context.SaveChanges();
            }
        }
    }
}
