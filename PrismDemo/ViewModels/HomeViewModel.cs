using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismDemo.Models;
using PrismDemo.NavigationServer;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PrismDemo.ViewModels
{
    public class HomeViewModel : NavigationServerCore
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private string _welcomeHeader;
        private string _welcomeSubheader;

        public string WelcomeHeader
        {
            get => _welcomeHeader;
            set => SetProperty(ref _welcomeHeader, value);
        }

        public string WelcomeSubheader
        {
            get => _welcomeSubheader;
            set => SetProperty(ref _welcomeSubheader, value);
        }

        public ObservableCollection<HomeFeature> Features { get; private set; }
        public ObservableCollection<NewsPreview> RecentNews { get; private set; }

        public DelegateCommand LearnMoreCommand { get; }
        public DelegateCommand ContactUsCommand { get; }
        public DelegateCommand<NewsPreview> ReadNewsCommand { get; }

        public HomeViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;

            LearnMoreCommand = new DelegateCommand(NavigateToAbout);
            ContactUsCommand = new DelegateCommand(NavigateToContact);
            ReadNewsCommand = new DelegateCommand<NewsPreview>(ViewNewsDetail);

            InitializeContent();
        }

        private void InitializeContent()
        {
            WelcomeHeader = "欢迎使用企业演示系统";
            WelcomeSubheader = "这是一个基于Prism框架的WPF企业应用演示系统，展示了Prism的核心功能和最佳实践，包括模块化、导航、对话框服务和MVVM模式等。";

            Features = new ObservableCollection<HomeFeature>
            {
                new HomeFeature
                {
                    Title = "模块化架构",
                    Description = "基于Prism的模块化设计，实现松耦合的组件化开发，提高代码复用性和可维护性。",
                    Icon = "ViewModule",
                    Color = new SolidColorBrush(Color.FromRgb(33, 150, 243)) // Blue
                },
                new HomeFeature
                {
                    Title = "区域导航",
                    Description = "灵活的区域导航系统，支持视图切换、参数传递和导航回调，打造流畅的用户体验。",
                    Icon = "Navigation",
                    Color = new SolidColorBrush(Color.FromRgb(76, 175, 80)) // Green
                },
                new HomeFeature
                {
                    Title = "对话框服务",
                    Description = "强大的对话框服务，支持自定义对话框、数据传递和回调处理，简化交互流程。",
                    Icon = "MessageProcessing",
                    Color = new SolidColorBrush(Color.FromRgb(244, 67, 54)) // Red
                },
                new HomeFeature
                {
                    Title = "事件聚合器",
                    Description = "基于发布-订阅模式的事件聚合器，实现组件间的松耦合通信，提高系统扩展性。",
                    Icon = "Connection",
                    Color = new SolidColorBrush(Color.FromRgb(156, 39, 176)) // Purple
                }
            };

            RecentNews = new ObservableCollection<NewsPreview>
            {
                new NewsPreview
                {
                    Id = 1,
                    Title = "重要产品安全更新通知",
                    Description = "为提升产品安全性，我公司近日发布了重要的安全更新，建议所有用户及时升级以防止潜在风险。",
                    Date = "2023-10-01",
                    Icon = "ShieldCheck",
                    Color = new SolidColorBrush(Color.FromRgb(255, 152, 0)) // Orange
                },
                new NewsPreview
                {
                    Id = 2,
                    Title = "新产品发布会圆满成功",
                    Description = "我公司于上周在北京举办了新产品发布会，展示了多款创新产品，获得业界广泛好评。",
                    Date = "2023-06-22",
                    Icon = "NewBox",
                    Color = new SolidColorBrush(Color.FromRgb(0, 188, 212)) // Cyan
                }
            };
        }

        private void NavigateToAbout()
        {
            _regionManager.RequestNavigate("MainRegion", "AboutView");
        }

        private void NavigateToContact()
        {
            _regionManager.RequestNavigate("MainRegion", "ContactView");
        }

        private void ViewNewsDetail(NewsPreview news)
        {
            if (news == null) return;

            var parameters = new DialogParameters
            {
                { "title", news.Title },
                { "message", $"日期: {news.Date}\n\n{news.Description}\n\n请访问新闻页面查看完整内容。" }
            };

            _dialogService.ShowDialog("MessageDialog", parameters, _ => {
                // 导航到新闻页面
                _regionManager.RequestNavigate("MainRegion", "NewsView");
            });
        }

        //页面加载时调用
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 无需额外初始化
        }

        //页面离开时调用
        public override void OnNavigatedFrom(NavigationContext navigationContext) 
        { 
            // 无需额外清理
        }
    }
}
