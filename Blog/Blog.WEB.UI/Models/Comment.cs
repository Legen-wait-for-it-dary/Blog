using System;

namespace Blog.WEB.UI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        public string Content { get; set; }
        public string MemberEmail { get; set; }
        public string MemberAvatar { get; set; }
        public DateTime PublishDate { get; set; }
    }
}