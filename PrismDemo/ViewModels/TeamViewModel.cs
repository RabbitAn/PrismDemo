using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PrismDemo.ViewModels
{
    public class TeamViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;

        public ObservableCollection<TeamMember> ManagementTeam { get; private set; }
        public ObservableCollection<TeamMember> TechnicalTeam { get; private set; }

        public DelegateCommand<TeamMember> ContactTeamMemberCommand { get; }
        public DelegateCommand ViewJobsCommand { get; }

        public TeamViewModel(IDialogService dialogService, IRegionManager regionManager)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            
            ContactTeamMemberCommand = new DelegateCommand<TeamMember>(ContactTeamMember);
            ViewJobsCommand = new DelegateCommand(ViewJobs);
            
            InitializeTeamMembers();
        }

        private void ContactTeamMember(TeamMember member)
        {
            if (member == null) return;

            var parameters = new DialogParameters
            {
                { "title", $"联系 {member.Name}" },
                { "message", $"请发送邮件至: {member.Email}\n\n{member.Name} - {member.Position}\n\n我们会尽快回复您的咨询。" }
            };

            _dialogService.ShowDialog("MessageDialog", parameters, _ => { });
        }

        private void ViewJobs()
        {
            var parameters = new DialogParameters
            {
                { "title", "招聘职位" },
                { "message", "目前我们正在招聘以下职位：\n\n1. 高级软件工程师\n   - 5年以上开发经验\n   - 精通C#, WPF, .NET\n\n2. UI/UX设计师\n   - 3年以上设计经验\n   - 熟悉Adobe设计工具\n\n3. 项目经理\n   - 5年以上项目管理经验\n   - PMP认证\n\n如有意向，请将简历发送至: hr@example.com" }
            };

            _dialogService.ShowDialog("MessageDialog", parameters, _ => { });
        }

        private void InitializeTeamMembers()
        {
            ManagementTeam = new ObservableCollection<TeamMember>
            {
                new TeamMember
                {
                    Id = 1,
                    Name = "张明",
                    Position = "首席执行官 (CEO)",
                    Description = "拥有20年IT行业经验，曾在多家知名科技公司担任高管职位。擅长战略规划和团队建设。",
                    Email = "ceo@example.com",
                    Icon = "Account",
                    Color = new SolidColorBrush(Color.FromRgb(33, 150, 243))
                },
                new TeamMember
                {
                    Id = 2,
                    Name = "王芳",
                    Position = "首席技术官 (CTO)",
                    Description = "技术专家，拥有15年软件架构经验，精通多种编程语言和框架，曾主导过多个大型项目的技术架构设计。",
                    Email = "cto@example.com",
                    Icon = "CodeTags",
                    Color = new SolidColorBrush(Color.FromRgb(156, 39, 176))
                },
                new TeamMember
                {
                    Id = 3,
                    Name = "李强",
                    Position = "首席运营官 (COO)",
                    Description = "负责公司的日常运营管理，拥有12年运营管理经验，善于优化业务流程，提高运营效率。",
                    Email = "coo@example.com",
                    Icon = "ChartLine",
                    Color = new SolidColorBrush(Color.FromRgb(76, 175, 80))
                }
            };

            TechnicalTeam = new ObservableCollection<TeamMember>
            {
                new TeamMember
                {
                    Id = 4,
                    Name = "赵伟",
                    Position = "首席架构师",
                    Description = "10年软件架构经验，专注于分布式系统和高性能应用设计，主导了公司核心产品的架构升级。",
                    Email = "architect@example.com",
                    Icon = "ViewQuilt",
                    Color = new SolidColorBrush(Color.FromRgb(255, 152, 0))
                },
                new TeamMember
                {
                    Id = 5,
                    Name = "陈静",
                    Position = "用户体验主管",
                    Description = "负责产品的用户体验设计，拥有8年UI/UX设计经验，致力于创造简洁、易用的用户界面。",
                    Email = "ux@example.com",
                    Icon = "Palette",
                    Color = new SolidColorBrush(Color.FromRgb(244, 67, 54))
                },
                new TeamMember
                {
                    Id = 6,
                    Name = "刘洋",
                    Position = "研发主管",
                    Description = "管理研发团队，协调各项目进度，制定技术标准和规范，保证产品质量和开发效率。",
                    Email = "dev@example.com",
                    Icon = "Developer",
                    Color = new SolidColorBrush(Color.FromRgb(121, 85, 72))
                },
                new TeamMember
                {
                    Id = 7,
                    Name = "杨丽",
                    Position = "质量保障经理",
                    Description = "负责产品的质量控制和测试，制定测试策略和流程，确保产品的可靠性和稳定性。",
                    Email = "qa@example.com",
                    Icon = "CheckboxMultipleMarked",
                    Color = new SolidColorBrush(Color.FromRgb(0, 150, 136))
                }
            };
        }
    }
} 