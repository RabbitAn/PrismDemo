using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PrismDemo.Events;
using System;
using System.Collections.ObjectModel;

namespace PrismDemo.ViewModels
{
    public class EventPublisherViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private string _senderName;
        private string _messageText;

        public string SenderName
        {
            get => _senderName;
            set => SetProperty(ref _senderName, value);
        }

        public string MessageText
        {
            get => _messageText;
            set => SetProperty(ref _messageText, value);
        }

        public ObservableCollection<MessageEventPayload> SentMessages { get; } = new ObservableCollection<MessageEventPayload>();

        public DelegateCommand SendMessageCommand { get; }
        public DelegateCommand ClearMessageCommand { get; }
        public DelegateCommand GoToSubscriberCommand { get; }

        public EventPublisherViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            SenderName = "发布者";
            
            SendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage)
                .ObservesProperty(() => SenderName)
                .ObservesProperty(() => MessageText);
            
            ClearMessageCommand = new DelegateCommand(ClearMessage);
            GoToSubscriberCommand = new DelegateCommand(NavigateToSubscriberView);
        }

        private bool CanSendMessage()
        {
            return !string.IsNullOrWhiteSpace(SenderName) && !string.IsNullOrWhiteSpace(MessageText);
        }

        private void SendMessage()
        {
            if (!CanSendMessage()) return;

            var payload = new MessageEventPayload
            {
                Sender = SenderName,
                Message = MessageText,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            // 发布消息事件
            _eventAggregator.GetEvent<MessageSentEvent>().Publish(payload);

            // 添加到本地消息列表
            SentMessages.Insert(0, payload);

            // 清空消息文本
            MessageText = string.Empty;
        }

        private void ClearMessage()
        {
            SenderName = "发布者";
            MessageText = string.Empty;
        }

        private void NavigateToSubscriberView()
        {
            _regionManager.RequestNavigate("MainRegion", "EventSubscriberView");
        }
    }
} 