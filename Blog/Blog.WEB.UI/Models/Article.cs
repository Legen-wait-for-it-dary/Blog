using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WEBUI.Frontend.Models
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string BigImagePath{ get; set; }
        public string Category { get; set; }
        public int MemberID { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsPublished { get; set; }
    }
}