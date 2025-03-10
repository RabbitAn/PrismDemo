using Prism.Events;

namespace PrismDemo.Events
{
    public class MessageSentEvent : PubSubEvent<MessageEventPayload>
    {
    }

    public class MessageEventPayload
    {
        public string Message { get; set; }
        public string Sender { get; set; }
        public string Timestamp { get; set; }
    }
} 