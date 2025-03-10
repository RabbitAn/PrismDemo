using DryIoc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using PrismDemo.Data;
using PrismDemo.NavigationServer;
using PrismDemo.ViewModels;
using PrismDemo.Views;
using System;
using System.Configuration;
using System.Windows;

namespace PrismDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                var dbContext = Container.Resolve<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库初始化失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();
            containerRegistry.RegisterInstance<IConfiguration>(configuration);

            // 配置 NLog
            LogManager.Configuration = new XmlLoggingConfiguration("NLog.config");
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(log =>
            {
                log.ClearProviders();
                log.AddNLog();            
            });

            // 配置DbContext选项
            string connectionString = configuration.GetConnectionString("PrismDemoDb");
            var serverVersion = new MySqlServerVersion(new Version(6, 0, 3));

            // 创建DbContext选项构建器
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion, mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

            // 注册DbContext
            containerRegistry.RegisterInstance<DbContextOptions<AppDbContext>>(optionsBuilder.Options);
            containerRegistry.Register<AppDbContext>();

            // 构建 ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            containerRegistry.RegisterInstance(serviceProvider);

            // 获取 ILoggerFactory 并注册到 Prism
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            containerRegistry.RegisterInstance(loggerFactory);

            // 让 Prism 的 DI 支持 ILogger<T>
            containerRegistry.RegisterSingleton(typeof(ILogger<>), typeof(Logger<>));

            containerRegistry.RegisterSingleton<INavigationColler, NavigationColler>();
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>("AboutView");
            containerRegistry.RegisterForNavigation<FormDemoView, FormDemoViewModel>("FormDemoView");
            containerRegistry.RegisterForNavigation<DialogDemoView, DialogDemoViewModel>("DialogDemoView");
            containerRegistry.RegisterForNavigation<EventPublisherView, EventPublisherViewModel>("EventPublisherView");
            containerRegistry.RegisterForNavigation<EventSubscriberView, EventSubscriberViewModel>("EventSubscriberView");
            containerRegistry.RegisterForNavigation<DataAccessDemoView, DataAccessDemoViewModel>("DataAccessDemoView");
            containerRegistry.RegisterForNavigation<ProductView, ProductViewModel>("ProductView");
            containerRegistry.RegisterForNavigation<ServiceView, ServiceViewModel>("ServiceView");
            containerRegistry.RegisterForNavigation<CaseView, CaseViewModel>("CaseView");
            containerRegistry.RegisterForNavigation<ContactView, ContactViewModel>("ContactView");
            containerRegistry.RegisterForNavigation<NewsView, NewsViewModel>("NewsView");
            containerRegistry.RegisterForNavigation<TeamView, TeamViewModel>("TeamView");

            // Register dialogs
            containerRegistry.RegisterDialog<CustomDialogView, CustomDialogViewModel>("CustomDialog");
            containerRegistry.RegisterDialog<ConfirmationDialogView, ConfirmationDialogViewModel>("ConfirmationDialog");
            containerRegistry.RegisterDialog<MessageDialogView, MessageDialogViewModel>("MessageDialog");
        }
    }
}
