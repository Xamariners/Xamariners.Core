using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Common.Helpers
{
    internal class MessageWrapper
    {
        private object _message;

        public void SetMessage<T>(Message<T> message)
        {
            _message = message;
            MessageId = message.MessageId;
        }

        public Message<TBody> GetMessage<TBody>()
        {
            return (Message<TBody>)_message;
        }

        public bool IsHidden { get; set; }
        public DateTime LastReadTime { get; set; }
        public string MessageId { get; private set; }
    }
}
