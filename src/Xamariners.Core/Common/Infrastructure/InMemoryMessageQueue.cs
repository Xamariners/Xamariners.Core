using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamariners.Core.Common.Helpers;
using Xamariners.Core.Interface;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Common.Infrastructure
{
    public class InMemoryMessageQueue : IMessageQueue
    {
        Dictionary<string, List<MessageWrapper>> _queues = new Dictionary<string, List<MessageWrapper>>();
        private readonly object _lock = new object();
        private TimeSpan _defaultMessageTimeout = TimeSpan.FromSeconds(5);
        private Timer _timer;

        public InMemoryMessageQueue()
        {
            _timer = new Timer(ResetExpiredMessages, null, 0, 1000);
        }

        private void ResetExpiredMessages(object state)
        {
            lock (_lock)
            {
                foreach (var queue in _queues)
                {
                    foreach (var message in queue.Value)
                    {
                        var messageLongevity = DateTime.UtcNow - message.LastReadTime;
                        //Trace.WriteLine(messageLongevity.TotalSeconds);
                        if (messageLongevity >= _defaultMessageTimeout)
                            message.IsHidden = false;
                    }
                }
            }
        }

        public bool HasQueue(string queueName)
        {
            return _queues.ContainsKey(queueName);
        }

        public void CreateQueue(string queueName)
        {
            lock (_lock)
            {
                List<MessageWrapper> queue;
                if (_queues.TryGetValue(queueName, out queue) == false)
                {
                    queue = new List<MessageWrapper>();
                    _queues.Add(queueName, queue);
                }
            }
        }

        public long GetMessageCount(string queueName)
        {
            VerifyQueue(queueName);
            lock (_lock)
            {
                var queue = _queues[queueName];
                return queue.Count(wrapper => wrapper.IsHidden == false);
            }
        }

        public void DeleteAllMessages(string queueName)
        {
            VerifyQueue(queueName);
            lock (_lock)
            {
                var queue = _queues[queueName];
                queue.Clear();
            }
        }

        public void Send<T>(string queueName, Message<T> message)
        {
            VerifyQueue(queueName);

            if (String.IsNullOrWhiteSpace(message.MessageId))
                message.MessageId = Guid.NewGuid().ToString();

            var queue = _queues[queueName];
            var wrapper = new MessageWrapper();
            wrapper.SetMessage(message);

            lock (_lock)
            {
                queue.Add(wrapper);
            }
        }

        public void DeleteMessage(string queueName, string messageId)
        {
            VerifyQueue(queueName);
            var queue = _queues[queueName];

            lock (_lock)
            {
                var message = queue.FirstOrDefault(m => m.MessageId == messageId);

                if (message == null)
                    throw new Exception(String.Format("Message with ID={0} not found in queue '{1}'", messageId, queueName));

                queue.Remove(message);
            }
        }

        public TimeSpan GetMessageTimeout(string queueName)
        {
            VerifyQueue(queueName);
            return _defaultMessageTimeout;
        }

        public Message<T> Receive<T>(string queueName)
        {
            VerifyQueue(queueName);
            var queue = _queues[queueName];

            lock (_lock)
            {
                var messageWrapper = queue.FirstOrDefault(wrapper => wrapper.IsHidden == false);

                if (messageWrapper != null)
                {
                    messageWrapper.IsHidden = true;
                    messageWrapper.LastReadTime = DateTime.UtcNow;
                    return messageWrapper.GetMessage<T>();
                }

                return null;
            }
        }

        public string GetQueueName(string queueId)
        {
            return queueId;
        }

        public void DeleteQueue(string queueName)
        {
            lock (_lock)
            {
                _queues.Remove(queueName);
            }
        }

        public void VerifyQueue(string queueName)
        {
            if (!HasQueue(queueName))
                CreateQueue(queueName);
        }

        public void ThrowIfNotFound(string queueName)
        {
            if (!HasQueue(queueName))
                throw new Exception(String.Format("Queue not found: {0}", queueName));
        }
    }
}
