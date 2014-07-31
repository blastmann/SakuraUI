using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SakuraUI.Utilities
{
    public interface IMessageSubscriber
    {
        void OnMessageReceived(string message, object param);
    }

    public class MessagePublisher
    {
        private static MessagePublisher _default;
        private readonly Dictionary<string, List<IMessageSubscriber>> _subscribersDictionary;

        public MessagePublisher()
        {
            _subscribersDictionary = new Dictionary<string, List<IMessageSubscriber>>();
        }

        public static MessagePublisher Default
        {
            get
            {
                return _default ?? (_default = new MessagePublisher());
            }
        }

        public void Register(string message, IMessageSubscriber subscriber)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (subscriber == null) return;

            lock (_subscribersDictionary)
            {
                List<IMessageSubscriber> subscribers;
                if (_subscribersDictionary.TryGetValue(message, out subscribers))
                {
                    if (subscribers == null) subscribers = new List<IMessageSubscriber>();
                    subscribers.Add(subscriber);
                }
                else
                {
                    _subscribersDictionary[message] = new List<IMessageSubscriber> { subscriber };
                }
            }
        }

        public void UnRegister(string message, IMessageSubscriber subscriber)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (subscriber == null) return;

            lock (_subscribersDictionary)
            {
                if (!_subscribersDictionary.ContainsKey(message)) return;
                var subscribers = _subscribersDictionary[message];
                if (!subscribers.Contains(subscriber)) return;
                subscribers.Remove(subscriber);
            }
        }

        public void PublishMessage(string message, object param = null)
        {
            if (string.IsNullOrEmpty(message)) return;

            lock (_subscribersDictionary)
            {
                if (!_subscribersDictionary.ContainsKey(message)) return;
                var subscribers = _subscribersDictionary[message];
                if (subscribers == null || subscribers.Count == 0) return;
                foreach (var messageSubscriber in subscribers)
                {
                    try
                    {
                        if (messageSubscriber == null) continue;
                        messageSubscriber.OnMessageReceived(message, param);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void PublishMessageAsync(string message, object param = null)
        {
            Task.Run(() => PublishMessage(message, param));
        }
    }
}