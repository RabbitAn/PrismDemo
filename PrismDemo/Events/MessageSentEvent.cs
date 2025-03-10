using Prism.Events;

namespace PrismDemo.Events
{
    /// <summary>
    /// 消息发送事件类，继承自Prism的PubSubEvent泛型类
    /// PubSubEvent是Prism事件聚合器中的基础类，用于定义可发布和订阅的事件
    /// 泛型参数MessageEventPayload定义了事件携带的数据类型
    /// </summary>
    public class MessageSentEvent : PubSubEvent<MessageEventPayload>
    {
        // 这个类不需要额外的实现，PubSubEvent基类已提供了发布和订阅的功能
    }

    /// <summary>
    /// 消息事件的负载数据类，包含消息的详细信息
    /// 作为事件发布时传递的数据对象
    /// </summary>
    public class MessageEventPayload
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息发送者名称
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 消息发送时间戳
        /// </summary>
        public string Timestamp { get; set; }
    }
} 