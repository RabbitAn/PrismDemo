using DryIoc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;

using PrismDemo.NavigationServer;
using PrismDemo.ViewModels;
using PrismDemo.Views;
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

            // 构建 ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            containerRegistry.RegisterInstance(serviceCollection);

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
