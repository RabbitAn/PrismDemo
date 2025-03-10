using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismDemo.Events;
using System;
using System.Collections.ObjectModel;

namespace PrismDemo.ViewModels
{
    public class EventSubscriberViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private bool _isSubscriptionActive;
        private bool _useFilter;
        private bool _useStrongReference;
        private string _subscriptionStatus;
        private string _subscriptionDescription;
        private SubscriptionToken _subscriptionToken;

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

        public string SubscriptionStatus
        {
            get => _subscriptionStatus;
            set => SetProperty(ref _subscriptionStatus, value);
        }

        public string SubscriptionDescription
        {
            get => _subscriptionDescription;
            set => SetProperty(ref _subscriptionDescription, value);
        }

        public ObservableCollection<MessageEventPayload> ReceivedMessages { get; } = new ObservableCollection<MessageEventPayload>();

        public DelegateCommand ClearMessagesCommand { get; }
        public DelegateCommand GoToPublisherCommand { get; }

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
                _subscriptionToken = messageSentEvent.Subscribe(
                    OnMessageReceived, 
                    ThreadOption.UIThread, 
                    UseStrongReference, 
                    message => message.Message.Contains("重要"));

                SubscriptionStatus = "已激活订阅（带过滤器）";
                SubscriptionDescription = "当前仅接收包含重要关键词的消息。";
            }
            else
            {
                // 无过滤器：接收所有消息
                _subscriptionToken = messageSentEvent.Subscribe(
                    OnMessageReceived, 
                    ThreadOption.UIThread, 
                    UseStrongReference);

                SubscriptionStatus = "已激活订阅";
                SubscriptionDescription = "当前接收所有消息。";
            }

            if (UseStrongReference)
            {
                SubscriptionDescription += " 使用强引用（订阅者不会被垃圾回收）。";
            }
            else
            {
                SubscriptionDescription += " 使用弱引用（如果没有其他引用，订阅者可能被垃圾回收）。";
            }
        }

        private void UnsubscribeFromEvent()
        {
            if (_subscriptionToken != null)
            {
                _eventAggregator.GetEvent<MessageSentEvent>().Unsubscribe(_subscriptionToken);
                _subscriptionToken = null;
                SubscriptionStatus = "未激活订阅";
                SubscriptionDescription = "当前未接收任何消息。要接收消息，请打开订阅开关。";
            }
        }

        private void OnMessageReceived(MessageEventPayload payload)
        {
            // 在UI线程中接收消息
            ReceivedMessages.Insert(0, payload);
        }

        private void ClearMessages()
        {
            ReceivedMessages.Clear();
        }

        private void NavigateToPublisherView()
        {
            _regionManager.RequestNavigate("MainRegion", "EventPublisherView");
        }
    }
} 