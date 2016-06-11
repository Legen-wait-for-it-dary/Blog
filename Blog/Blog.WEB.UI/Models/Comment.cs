using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WEBUI.Frontend.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ArticleID { get; set; }
        public string Content { get; set; }
        public string MemberEmail { get; set; }
        public string MemberAvatar { get; set; }
        public DateTime PublishDate { get; set; }
    }
}