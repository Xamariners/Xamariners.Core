using System;
using Xamariners.Core.Model;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Interface
{
    public interface IMessageQueue
    {
        bool HasQueue(string queueName);
        void CreateQueue(string queueName);
        long GetMessageCount(string queueName);
        void DeleteAllMessages(string queueName);
        void Send<T>(string queueName, Message<T> message);
        void DeleteMessage(string queueName, string messageId);
        TimeSpan GetMessageTimeout(string queueName);
        Message<T> Receive<T>(string queueName);
        string GetQueueName(string queueId);
        void DeleteQueue(string queueName);
    }
}