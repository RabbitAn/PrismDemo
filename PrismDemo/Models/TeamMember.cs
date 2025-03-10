using System.Windows.Media;

namespace PrismDemo.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Icon { get; set; }
        public Brush Color { get; set; }
    }
} 