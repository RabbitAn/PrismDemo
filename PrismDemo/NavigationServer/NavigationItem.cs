using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.NavigationServer
{
 public   class NavigationItem
    {
        public NavigationItem(string name, string icon, string viewName)
        {
            Name = name;
            Icon = icon;
            ViewName = viewName;
        }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ViewName { get; set; }

        public List<NavigationItem> Children { get; set; }
    }
}
