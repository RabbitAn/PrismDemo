using System.Windows.Media;

namespace PrismDemo.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Client { get; set; }
        public string Description { get; set; }
        public string Technologies { get; set; }
        public string Icon { get; set; }
        public Brush Color { get; set; }
        public string FullDescription { get; set; }
    }
} 