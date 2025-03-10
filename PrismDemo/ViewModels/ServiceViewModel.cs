using Microsoft.Extensions.Configuration;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismDemo.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Media;

namespace PrismDemo.ViewModels
{
    public class ServiceViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;
        private readonly IConfiguration configuration;

        public ObservableCollection<Service> Services { get; private set; }

        public DelegateCommand<Service> LearnMoreCommand { get; }
        public DelegateCommand ContactUsCommand { get; }

        public ServiceViewModel(IDialogService dialogService, IRegionManager regionManager)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            this.configuration = configuration;
            LearnMoreCommand = new DelegateCommand<Service>(LearnMoreAboutService);
            ContactUsCommand = new DelegateCommand(ContactUs);
            
            InitializeServices();
        }

        private void LearnMoreAboutService(Service service)
        {
            if (service == null) return;

            var parameters = new DialogParameters
            {
                { "title", service.Name },
                { "message", $"{service.Name}\n\n{service.Description}\n\n如需了解更多信息，请联系我们的客服团队。" }
            };

            _dialogService.ShowDialog("MessageDialog", parameters, _ => { });
        }

        private void ContactUs()
        {
            _regionManager.RequestNavigate("MainRegion", "ContactView");
        }

        private void InitializeServices()
        {
       
            Services = new ObservableCollection<Service>
            {
       
                new Service
                {
                    Id = 1,
                    Name = "软件开发",
                    Description = "我们提供全方位的软件开发服务，包括桌面应用、Web应用、移动应用等。团队由经验丰富的开发人员组成，确保高质量的代码和可靠的解决方案。",
                    Icon = "CodeBraces",
                    IconColor = new SolidColorBrush(Colors.Blue)
                },
                new Service
                {
                    Id = 2,
                    Name = "UI/UX设计",
                    Description = "专业的UI/UX设计团队，致力于创造美观、易用、高效的用户界面。我们注重用户体验，确保每个设计元素都有其存在的理由。",
                    Icon = "Palette",
                    IconColor = new SolidColorBrush(Colors.Orange)
                },
                new Service
                {
                    Id = 3,
                    Name = "云服务与部署",
                    Description = "提供云基础设施规划、迁移和管理服务。帮助企业安全高效地部署应用程序，优化资源使用，提高系统可靠性。",
                    Icon = "Cloud",
                    IconColor = new SolidColorBrush(Colors.SkyBlue)
                },
                new Service
                {
                    Id = 4,
                    Name = "IT咨询",
                    Description = "提供专业的IT战略咨询服务，帮助企业制定数字化转型策略，解决技术难题，优化IT流程，提高组织效率。",
                    Icon = "LightbulbOn",
                    IconColor = new SolidColorBrush(Colors.Yellow)
                },
                new Service
                {
                    Id = 5,
                    Name = "系统维护",
                    Description = "提供全面的系统维护和技术支持服务，包括故障排除、性能优化、安全加固等，确保您的系统持续稳定运行。",
                    Icon = "Wrench",
                    IconColor = new SolidColorBrush(Colors.Gray)
                }
            };
        }
    }
} 