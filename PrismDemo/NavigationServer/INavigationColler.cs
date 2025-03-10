using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.NavigationServer
{
 public   interface INavigationColler
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }
        public void InitalizeNavigation();
    }
}
