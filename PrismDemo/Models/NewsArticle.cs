using System;
using System.Windows.Media;

namespace PrismDemo.Models
{
    public class NewsArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string PublishDate { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Icon { get; set; }
        public Brush IconColor { get; set; }
        public bool IsHot { get; set; }
    }
} 