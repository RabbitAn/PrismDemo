using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Regions;
using PrismDemo.NavigationServer;
using System.Collections.ObjectModel;

namespace PrismDemo.ViewModels
{
    public class MainWindowViewModel : NavigationServerCore
    {
        private string _title = "Prism Demo";
        private readonly IConfiguration configuration;
        private readonly INavigationColler navigationCollector;
        private readonly IRegionManager regionManager;
        private readonly ILogger<MainWindowViewModel> logger;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<NavigationItem> _navigationItems;
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { SetProperty(ref _navigationItems, value); }
        }

        public MainWindowViewModel(IConfiguration configuration, INavigationColler navigationCollector, IRegionManager regionManager,ILogger<MainWindowViewModel> logger)
        {
            this.configuration = configuration;
            this.navigationCollector = navigationCollector;
            this.regionManager = regionManager;
            this.logger = logger;
            _navigationItems = new ObservableCollection<NavigationItem>();
            navigationCollector.InitalizeNavigation();
            NavigationItems = navigationCollector.NavigationItems;
            _selectionChangedCommand = new(SelectionChanged);
            this.logger.LogInformation("MainWindowViewModel created.");
        }
        private DelegateCommand<object> _selectionChangedCommand;
        public DelegateCommand<object> SelectionChangedCommand
        {
            get { return _selectionChangedCommand; }
            set { SetProperty(ref _selectionChangedCommand, value); }
        }

        public void SelectionChanged(object obj)
        {
            if (obj != null && obj is NavigationItem item)
            {
                regionManager.RequestNavigate("MainRegion", item.ViewName);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
     
        }
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }
    }
}
