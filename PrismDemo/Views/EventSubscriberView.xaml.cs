using System.Windows.Controls;

namespace PrismDemo.Views
{
    /// <summary>
    /// 事件订阅者视图的交互逻辑
    /// 这个页面订阅和显示由事件发布者发送的消息事件
    /// UI绑定到EventSubscriberViewModel，用于事件的订阅和接收
    /// 包含订阅配置选项，如过滤器和引用类型
    /// </summary>
    public partial class EventSubscriberView : UserControl
    {
        public EventSubscriberView()
        {
            InitializeComponent();
        }
    }
} 