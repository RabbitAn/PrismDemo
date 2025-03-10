using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismDemo.Events;
using System;
using System.Collections.ObjectModel;

namespace PrismDemo.ViewModels
{
    /// <summary>
    /// 事件发布者的ViewModel，负责发布消息事件
    /// 演示了如何使用Prism的事件聚合器发布事件
    /// </summary>
    public class EventPublisherViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator; // Prism的事件聚合器，用于发布和订阅事件
        private string _senderName;
        private string _messageText;

        /// <summary>
        /// 发送者姓名，绑定到UI
        /// </summary>
        public string SenderName
        {
            get => _senderName;
            set => SetProperty(ref _senderName, value);
        }

        /// <summary>
        /// 消息内容，绑定到UI
        /// </summary>
        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }

        /// <summary>
        /// 已发送消息的集合，用于在UI中显示
        /// </summary>
        public ObservableCollection<MessageEventPayload> SentMessages { get; } = new ObservableCollection<MessageEventPayload>();

        /// <summary>
        /// 发送消息的命令
        /// </summary>
        public DelegateCommand SendMessageCommand { get; }

        /// <summary>
        /// 清除消息表单的命令
        /// </summary>
        public DelegateCommand ClearMessageCommand { get; }

        /// <summary>
        /// 导航到订阅者页面的命令
        /// </summary>
        public DelegateCommand GoToSubscriberCommand { get; }

        /// <summary>
        /// 构造函数，注入依赖服务并初始化命令和默认值
        /// </summary>
        /// <param name="regionManager">区域管理器，用于页面导航</param>
        /// <param name="eventAggregator">事件聚合器，用于发布事件</param>
        public EventPublisherViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            SenderName = "发布者"; // 默认发送者名称
            
            // 初始化命令
            // ObservesProperty使命令在属性变化时自动重新评估CanExecute
            SendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage)
                .ObservesProperty(() => SenderName)
                .ObservesProperty(() => MessageText);
            
            ClearMessageCommand = new DelegateCommand(ClearMessage);
            GoToSubscriberCommand = new DelegateCommand(NavigateToSubscriberView);
        }

        /// <summary>
        /// 判断是否可以发送消息
        /// 要求发送者名称和消息内容都不为空
        /// </summary>
        private bool CanSendMessage()
        {
            return !string.IsNullOrWhiteSpace(SenderName) && !string.IsNullOrWhiteSpace(MessageText);
        }

        /// <summary>
        /// 发送消息的方法，创建消息载荷并通过事件聚合器发布
        /// </summary>
        private void SendMessage()
        {
            if (!CanSendMessage()) return;

            // 创建消息载荷
            var payload = new MessageEventPayload
            {
                Sender = SenderName,
                Message = MessageText,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            // 通过事件聚合器发布消息事件
            // 这个操作会通知所有订阅了MessageSentEvent的订阅者
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(payload);

            // 更新本地已发送消息列表，以在UI中显示
            SentMessages.Insert(0, payload);

            // 清空消息文本，准备发送下一条消息
            MessageText = string.Empty;
        }

        /// <summary>
        /// 清除消息表单，重置为默认值
        /// </summary>
        private void ClearMessage()
        {
            SenderName = "发布者";
            MessageText = string.Empty;
        }

        /// <summary>
        /// 导航到订阅者页面，以查看接收到的消息
        /// </summary>
        private void NavigateToSubscriberView()
        {
            _regionManager.RequestNavigate("MainRegion", "EventSubscriberView");
        }
    }
} 