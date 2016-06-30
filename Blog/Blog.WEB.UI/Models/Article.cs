using System;

namespace Blog.WEB.UI.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string ArticleCover { get; set; }
        public string Category { get; set; }
        public int MemberId { get; set; }
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
        public int NumberOfComments { get; set; }
    }
}