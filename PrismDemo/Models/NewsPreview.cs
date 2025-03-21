using System.Windows.Media;

namespace PrismDemo.Models
{
    public class NewsPreview
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Icon { get; set; }
        public Brush Color { get; set; }
    }
} 