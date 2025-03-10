using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.NavigationServer
{
    public class NavigationColler : INavigationColler
    {
        public NavigationColler()
        {
            NavigationItems = new ObservableCollection<NavigationItem>();
        }

        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public void InitalizeNavigation()
        {
            NavigationItems.Add(new NavigationItem(name: "主页", icon: "Home", viewName: "HomeView"));
            NavigationItems.Add(new NavigationItem(name: "表单演示", icon: "FormSelect", viewName: "FormDemoView"));
            NavigationItems.Add(new NavigationItem(name: "对话框演示", icon: "MessageText", viewName: "DialogDemoView"));
            NavigationItems.Add(new NavigationItem(name: "事件发布", icon: "Send", viewName: "EventPublisherView"));
            NavigationItems.Add(new NavigationItem(name: "事件订阅", icon: "Antenna", viewName: "EventSubscriberView"));
            NavigationItems.Add(new NavigationItem(name: "产品", icon: "Reproduction", viewName: "ProductView"));            
            NavigationItems.Add(new NavigationItem(name: "服务", icon: "Lifebuoy", viewName: "ServiceView"));
            NavigationItems.Add(new NavigationItem(name: "案例", icon: "BriefcaseCheck", viewName: "CaseView"));
            NavigationItems.Add(new NavigationItem(name: "新闻", icon: "NewspaperVariant", viewName: "NewsView"));
            NavigationItems.Add(new NavigationItem(name: "团队", icon: "Handshake", viewName: "TeamView"));
            NavigationItems.Add(new NavigationItem(name: "联系", icon: "Contact", viewName: "ContactView"));
            NavigationItems.Add(new NavigationItem(name: "关于", icon: "About", viewName: "AboutView"));  
        }
    }
}
