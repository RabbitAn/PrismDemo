using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismDemo.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace PrismDemo.ViewModels
{
    public class NewsViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private ObservableCollection<NewsArticle> _news;
        private ObservableCollection<NewsArticle> _filteredNews;
        private ObservableCollection<NewsArticle> _hotNews;
        private string _searchText;
        private string _selectedCategory;
        private List<string> _categories;

        public ObservableCollection<NewsArticle> FilteredNews
        {
            get => _filteredNews;
            set => SetProperty(ref _filteredNews, value);
        }

        public ObservableCollection<NewsArticle> HotNews
        {
            get => _hotNews;
            set => SetProperty(ref _hotNews, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterNews();
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                FilterNews();
            }
        }

        public List<string> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public DelegateCommand SearchCommand { get; }
        public DelegateCommand<NewsArticle> ReadNewsCommand { get; }

        public NewsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            SearchCommand = new DelegateCommand(FilterNews);
            ReadNewsCommand = new DelegateCommand<NewsArticle>(ReadNewsArticle);
            
            InitializeNews();
            InitializeCategories();
            FilterNews();
        }

        private void InitializeCategories()
        {
            Categories = _news.Select(n => n.Category).Distinct().ToList();
            Categories.Insert(0, "全部分类");
            SelectedCategory = Categories[0];
        }

        private void FilterNews()
        {
            var query = _news.AsQueryable();
            
            // 按分类过滤
            if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "全部分类")
            {
                query = query.Where(n => n.Category == SelectedCategory);
            }
            
            // 按搜索文本过滤
            if (!string.IsNullOrEmpty(SearchText))
            {
                string searchLower = SearchText.ToLower();
                query = query.Where(n => 
                    n.Title.ToLower().Contains(searchLower) ||
                    n.Summary.ToLower().Contains(searchLower) ||
                    n.Content.ToLower().Contains(searchLower));
            }

            FilteredNews = new ObservableCollection<NewsArticle>(query.ToList());
        }

        private void ReadNewsArticle(NewsArticle newsArticle)
        {
            if (newsArticle == null) return;

            var parameters = new DialogParameters
            {
                { "title", newsArticle.Title },
                { "message", $"发布日期: {newsArticle.PublishDate}\n作者: {newsArticle.Author}\n分类: {newsArticle.Category}\n\n{newsArticle.Content}" }
            };

            _dialogService.ShowDialog("MessageDialog", parameters, _ => { });
        }

        private void InitializeNews()
        {
            _news = new ObservableCollection<NewsArticle>
            {
                new NewsArticle
                {
                    Id = 1,
                    Title = "我公司成功完成A轮融资",
                    Category = "公司新闻",
                    PublishDate = "2023-05-15",
                    Author = "张三",
                    Summary = "我公司近日成功完成A轮融资，融资金额达1亿元人民币，将用于产品研发和市场扩张。",
                    Content = "我公司近日宣布成功完成1亿元人民币A轮融资，本轮融资由某知名投资机构领投，多家投资机构跟投。\n\n公司表示，本次融资将主要用于产品线扩展、技术研发以及全球市场布局。公司CEO表示：此次融资是对我们团队和产品的高度认可，我们将继续专注于技术创新，为客户创造更大价值。" +
                    "\n\n据悉，我公司成立于2018年，是国内领先的企业服务提供商，产品已覆盖全国多个省市，服务超过500家大中型企业。未来，公司将在现有业务基础上，进一步扩大市场份额，提升品牌影响力。",
                    Icon = "CurrencyUsd",
                    IconColor = new SolidColorBrush(Colors.Green),
                    IsHot = true
                },
                new NewsArticle
                {
                    Id = 2,
                    Title = "新产品发布会圆满成功",
                    Category = "产品动态",
                    PublishDate = "2023-06-22",
                    Author = "李四",
                    Summary = "我公司于上周在北京举办了新产品发布会，展示了多款创新产品，获得业界广泛好评。",
                    Content = "上周二，我公司在北京国际会议中心成功举办了2023年新产品发布会，会上发布了包括智能办公系统、企业数据分析平台等多款创新产品。\n\n此次发布会吸引了来自全国各地的400多位客户和合作伙伴参加。与会嘉宾对我公司的新产品表示出浓厚兴趣，认为这些产品将有效提升企业运营效率，降低管理成本。\n\n公司产品总监表示，这些新产品是公司研发团队历时两年的努力成果，融合了最前沿的技术，并针对用户实际需求进行了大量优化。预计这些产品将在年内陆续上市，为公司带来新的增长点。",
                    Icon = "NewBox",
                    IconColor = new SolidColorBrush(Colors.Blue),
                    IsHot = true
                },
                new NewsArticle
                {
                    Id = 3,
                    Title = "我公司荣获年度最佳雇主称号",
                    Category = "公司新闻",
                    PublishDate = "2023-07-10",
                    Author = "王五",
                    Summary = @"在近日举办的2023年人力资源峰会上，我公司荣获年度最佳雇主称号，彰显了公司在人才管理方面的卓越表现。",
                    Content = "在日前举办的2023年人力资源峰会上，我公司从众多参评企业中脱颖而出，荣获年度最佳雇主称号。这一奖项充分肯定了公司在人才培养、员工关怀和企业文化建设等方面的出色表现。\n\n公司人力资源总监在获奖致辞中表示，公司始终将员工视为最宝贵的资产，通过完善的培训体系、富有竞争力的薪酬福利和开放包容的企业文化，吸引和保留了大量优秀人才。未来，公司将继续优化人才战略，为员工提供更好的职业发展平台。\n\n据了解，我公司目前员工满意度达95%，远高于行业平均水平，离职率维持在较低水平。公司还定期举办各类团建活动，增强团队凝聚力，营造积极向上的工作氛围。",
                    Icon = "Trophy",
                    IconColor = new SolidColorBrush(Colors.Gold),
                    IsHot = false
                },
                new NewsArticle
                {
                    Id = 4,
                    Title = "我公司与某高校达成战略合作",
                    Category = "合作动态",
                    PublishDate = "2023-08-05",
                    Author = "赵六",
                    Summary = "近日，我公司与某知名高校签署战略合作协议，双方将在人才培养、技术研发等方面展开深入合作。",
                    Content = "本周三，我公司与某知名高校在校区签署了战略合作协议。根据协议，双方将在人才培养、科研合作、技术转化等领域开展全方位合作。\n\n具体而言，我公司将为该校提供实习岗位和就业机会，同时资助学校建立联合实验室；学校则将为公司提供技术咨询和人才推荐，并共同开展前沿技术研究。\n\n公司技术总监表示，此次合作是产学研结合的典范，将加速科研成果转化为商业应用，同时为公司引入更多创新人才。校方代表也表示，这种合作模式有利于提高学生的实践能力，促进学术研究与市场需求的有效对接。\n\n未来，双方还计划联合举办技术论坛和创新大赛，共同培养符合行业需求的高素质人才。",
                    Icon = "School",
                    IconColor = new SolidColorBrush(Colors.Purple),
                    IsHot = false
                },
                new NewsArticle
                {
                    Id = 5,
                    Title = "我公司开展员工志愿服务活动",
                    Category = "企业文化",
                    PublishDate = "2023-09-18",
                    Author = "孙七",
                    Summary = "上周末，我公司组织员工前往山区学校开展志愿服务活动，捐赠图书和教学设备，体现企业社会责任。",
                    Content = "上周末，我公司组织30名员工志愿者前往山区某小学开展公益活动，为孩子们带去了1000余册图书、20台电脑以及各类学习用品。\n\n志愿者们还为孩子们举办了一场别开生面的科技课堂，通过有趣的实验和互动游戏，激发孩子们对科学的兴趣。活动得到了学校师生的热烈欢迎，孩子们纷纷表示收获满满。\n\n公司CSR负责人表示，此次活动是公司科技筑梦公益计划的一部分，旨在通过教育帮扶缩小城乡教育差距。未来，公司将持续关注教育公益，定期组织员工参与各类社会服务，践行企业社会责任。\n\n参与此次活动的员工普遍反映，志愿服务让他们感受到了帮助他人的快乐，也增强了团队凝聚力，期待公司组织更多类似活动。",
                    Icon = "Heart",
                    IconColor = new SolidColorBrush(Colors.Red),
                    IsHot = false
                },
                new NewsArticle
                {
                    Id = 6,
                    Title = "重要产品安全更新通知",
                    Category = "产品动态",
                    PublishDate = "2023-10-01",
                    Author = "周八",
                    Summary = "为提升产品安全性，我公司近日发布了重要的安全更新，建议所有用户及时升级以防止潜在风险。",
                    Content = "我公司技术团队近日发现了产品中存在的一个安全隐患，可能会导致用户数据泄露。为保障用户信息安全，我们立即组织技术力量进行修复，并紧急发布了安全更新补丁。\n\n此次更新不仅修复了安全漏洞，还优化了系统性能，提升了用户体验。用户可通过官方网站或产品内置更新功能获取最新版本。\n\n公司安全部门负责人强调，网络安全威胁日益增加，公司始终将用户数据安全放在首位，持续投入大量资源进行安全防护和漏洞修复。我们建议用户保持软件及时更新，设置强密码，并定期备份重要数据。\n\n此外，公司还计划在近期举办网络安全公开课，向用户普及安全知识，提高安全意识。详情请关注公司官方网站和社交媒体账号。",
                    Icon = "ShieldCheck",
                    IconColor = new SolidColorBrush(Colors.Orange),
                    IsHot = true
                }
            };
            
            _hotNews = new ObservableCollection<NewsArticle>(_news.Where(n => n.IsHot).ToList());
        }
    }
} 