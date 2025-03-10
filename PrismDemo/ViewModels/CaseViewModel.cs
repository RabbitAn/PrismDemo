using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PrismDemo.ViewModels
{
    public class CaseViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;

        public ObservableCollection<Case> Cases { get; private set; }

        public DelegateCommand<Case> ViewCaseCommand { get; }

        public CaseViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            ViewCaseCommand = new DelegateCommand<Case>(ViewCaseDetails);
            
            InitializeCases();
        }

        private void ViewCaseDetails(Case caseStudy)
        {
            if (caseStudy == null) return;

            var parameters = new DialogParameters
            {
                { "title", caseStudy.Title },
                { "message", $"客户: {caseStudy.Client}\n\n{caseStudy.FullDescription}\n\n技术: {caseStudy.Technologies}" }
            };

            _dialogService.ShowDialog("MessageDialog", parameters, _ => { });
        }

        private void InitializeCases()
        {
            Cases = new ObservableCollection<Case>
            {
                new Case
                {
                    Id = 1,
                    Title = "企业管理系统重构",
                    Client = "某大型制造企业",
                    Description = "为客户重构企业管理系统，提高系统性能，优化用户体验，简化业务流程。",
                    Technologies = "WPF, Prism, SQL Server, Entity Framework",
                    Icon = "Factory",
                    Color = new SolidColorBrush(Color.FromRgb(41, 128, 185)),
                    FullDescription = "客户的原有企业管理系统使用了过时的技术，系统性能差，用户体验不佳。我们使用现代技术栈重构了整个系统，把单体应用改造为模块化架构，大幅提高了系统的可维护性和性能。\n\n新系统显著提高了员工的工作效率，减少了培训时间，降低了维护成本。客户报告经过重构后，企业整体运营效率提高了30%。"
                },
                new Case
                {
                    Id = 2,
                    Title = "医疗数据分析平台",
                    Client = "某医疗科技公司",
                    Description = "开发医疗数据分析平台，帮助医疗机构分析患者数据，提高诊断准确率和治疗效果。",
                    Technologies = "WPF, Prism, Azure, ML.NET, Microservices",
                    Icon = "Hospital",
                    Color = new SolidColorBrush(Color.FromRgb(231, 76, 60)),
                    FullDescription = "客户需要一个强大的数据分析平台，用于处理海量医疗数据，并从中提取有价值的见解。我们设计了一套微服务架构的分析平台，利用机器学习算法帮助医生更准确地诊断疾病。\n\n该系统能够处理多种格式的医疗数据，并提供直观的可视化界面。自实施以来，该系统已帮助医院提高诊断准确率15%，缩短患者等待时间23%。"
                },
                new Case
                {
                    Id = 3,
                    Title = "智能家居控制中心",
                    Client = "某智能家居公司",
                    Description = "开发了一套智能家居控制中心，用户可通过桌面应用集中控制家中所有智能设备。",
                    Technologies = "WPF, Prism, IoT Protocols, MQTT, REST APIs",
                    Icon = "HomeAutomation",
                    Color = new SolidColorBrush(Color.FromRgb(46, 204, 113)),
                    FullDescription = "客户需要一个集中控制平台，允许用户管理和控制家中的所有智能设备。我们开发了一个高度可配置的控制中心，支持市场上主流的智能家居设备和协议。\n\n该控制中心提供直观的界面，用户可以创建自动化场景、设置定时任务，并通过语音控制设备。产品上市后3个月内，用户满意度达到95%，成为市场上领先的智能家居控制解决方案。"
                },
                new Case
                {
                    Id = 4,
                    Title = "金融投资分析工具",
                    Client = "某投资管理公司",
                    Description = "开发了一套金融投资分析工具，帮助分析师快速评估投资风险和回报，优化投资组合。",
                    Technologies = "WPF, Prism, Financial Libraries, Real-time Data Processing",
                    Icon = "ChartLine",
                    Color = new SolidColorBrush(Color.FromRgb(155, 89, 182)),
                    FullDescription = "客户需要一个能处理大量金融数据的分析工具，帮助分析师做出更好的投资决策。我们开发了一个专业的金融分析平台，可以实时处理市场数据，进行复杂的投资组合分析。\n\n该工具集成了多种金融模型和算法，提供丰富的数据可视化功能。自使用该工具以来，客户的投资组合表现优于市场平均水平12%，分析效率提高了40%。"
                }
            };
        }
    }
} 