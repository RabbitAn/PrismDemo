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
    /// 事件订阅者的ViewModel，负责订阅和处理消息事件
    /// 演示了如何使用Prism的事件聚合器订阅事件，以及各种订阅选项
    /// </summary>
    public class EventSubscriberViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator; // Prism的事件聚合器，用于发布和订阅事件
        private bool _isSubscriptionActive; // 订阅是否激活
        private bool _useFilter; // 是否使用过滤器
        private bool _useStrongReference; // 是否使用强引用
        private string _subscriptionStatus; // 订阅状态描述
        private string _subscriptionDescription; // 订阅详细描述
        private SubscriptionToken _subscriptionToken; // 订阅令牌，用于取消订阅

        /// <summary>
        /// 订阅是否激活，与UI开关绑定
        /// 当设置新值时，会根据状态自动订阅或取消订阅
        /// </summary>
        public bool IsSubscriptionActive
        {
            get => _isSubscriptionActive;
            set
            {
                if (SetProperty(ref _isSubscriptionActive, value))
                {
                    UpdateSubscription();
                }
            }
        }

        /// <summary>
        /// 是否使用过滤器，与UI复选框绑定
        /// 当值改变且订阅激活时，会重新配置订阅
        /// </summary>
        public bool UseFilter
        {
            get => _useFilter;
            set
            {
                if (SetProperty(ref _useFilter, value) && IsSubscriptionActive)
                {
                    // 如果已激活订阅，则重新订阅以应用新设置
                    UnsubscribeFromEvent();
                    SubscribeToEvent();
                }
            }
        }

        /// <summary>
        /// 是否使用强引用，与UI复选框绑定
        /// 当值改变且订阅激活时，会重新配置订阅
        /// </summary>
        public bool UseStrongReference
        {
            get => _useStrongReference;
            set
            {
                if (SetProperty(ref _useStrongReference, value) && IsSubscriptionActive)
                {
                    // 如果已激活订阅，则重新订阅以应用新设置
                    UnsubscribeFromEvent();
                    SubscribeToEvent();
                }
            }
        }

        /// <summary>
        /// 订阅状态描述，显示在UI中
        /// </summary>
        public string SubscriptionStatus
        {
            get => _subscriptionStatus;
            set => SetProperty(ref _subscriptionStatus, value);
        }

        /// <summary>
        /// 订阅详细描述，显示在UI中
        /// </summary>
        public string SubscriptionDescription
        {
            get => _subscriptionDescription;
            set => SetProperty(ref _subscriptionDescription, value);
        }

        /// <summary>
        /// 接收到的消息集合，用于在UI中显示
        /// </summary>
        public ObservableCollection<MessageEventPayload> ReceivedMessages { get; } = new ObservableCollection<MessageEventPayload>();

        /// <summary>
        /// 清除已接收消息的命令
        /// </summary>
        public DelegateCommand ClearMessagesCommand { get; }

        /// <summary>
        /// 导航到发布者页面的命令
        /// </summary>
        public DelegateCommand GoToPublisherCommand { get; }

        /// <summary>
        /// 构造函数，注入依赖服务并初始化命令和默认值
        /// </summary>
        /// <param name="regionManager">区域管理器，用于页面导航</param>
        /// <param name="eventAggregator">事件聚合器，用于订阅事件</param>
        public EventSubscriberViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            ClearMessagesCommand = new DelegateCommand(ClearMessages);
            GoToPublisherCommand = new DelegateCommand(NavigateToPublisherView);

            // 默认设置
            UseFilter = false;
            UseStrongReference = true;
            IsSubscriptionActive = true; // 这将触发订阅
        }

        /// <summary>
        /// 根据IsSubscriptionActive属性更新订阅状态
        /// </summary>
        private void UpdateSubscription()
        {
            if (IsSubscriptionActive)
            {
                SubscribeToEvent();
            }
            else
            {
                UnsubscribeFromEvent();
            }
        }

        /// <summary>
        /// 订阅消息事件，根据当前设置（过滤器和引用类型）配置订阅
        /// </summary>
        private void SubscribeToEvent()
        {
            if (_subscriptionToken != null)
            {
                UnsubscribeFromEvent();
            }

            var messageSentEvent = _eventAggregator.GetEvent<MessageSentEvent>();

            if (UseFilter)
            {
                // 使用过滤器：只接收包含"重要"字样的消息
                // 过滤器是一个Predicate委托，用于在消息到达时进行过滤
                _subscriptionToken = messageSentEvent.Subscribe(
                    OnMessageReceived, 
                    ThreadOption.UIThread, // 指定在UI线程上执行回调
                    UseStrongReference, // 是否使用强引用（影响垃圾回收行为）
                    message => message.Message.Contains("重要")); // 过滤条件

                SubscriptionStatus = "已激活订阅（带过滤器）";
                SubscriptionDescription = "当前仅接收包含重要关键词的消息。";
            }
            else
            {
                // 无过滤器：接收所有消息
                _subscriptionToken = messageSentEvent.Subscribe(
                    OnMessageReceived, 
                    ThreadOption.UIThread, // 指定在UI线程上执行回调
                    UseStrongReference); // 是否使用强引用

                SubscriptionStatus = "已激活订阅";
                SubscriptionDescription = "当前接收所有消息。";
            }

            // 更新描述，说明引用类型的影响
            if (UseStrongReference)
            {
                SubscriptionDescription += " 使用强引用（订阅者不会被垃圾回收）。";
            }
            else
            {
                SubscriptionDescription += " 使用弱引用（如果没有其他引用，订阅者可能被垃圾回收）。";
            }
        }

        /// <summary>
        /// 取消订阅消息事件
        /// </summary>
        private void UnsubscribeFromEvent()
        {
            if (_subscriptionToken != null)
            {
                // 使用订阅令牌取消订阅
                _eventAggregator.GetEvent<MessageSentEvent>().Unsubscribe(_subscriptionToken);
                _subscriptionToken = null;
                SubscriptionStatus = "未激活订阅";
                SubscriptionDescription = "当前未接收任何消息。要接收消息，请打开订阅开关。";
            }
        }

        /// <summary>
        /// 接收到消息时的回调方法，将消息添加到接收列表
        /// </summary>
        /// <param name="payload">接收到的消息载荷</param>
        private void OnMessageReceived(MessageEventPayload payload)
        {
            // 由于指定了ThreadOption.UIThread，此方法在UI线程中执行
            ReceivedMessages.Insert(0, payload);
        }

        /// <summary>
        /// 清除已接收的消息列表
        /// </summary>
        private void ClearMessages()
        {
            ReceivedMessages.Clear();
        }

        /// <summary>
        /// 导航到发布者页面
        /// </summary>
        private void NavigateToPublisherView()
        {
            _regionManager.RequestNavigate("MainRegion", "EventPublisherView");
        }
    }
} 