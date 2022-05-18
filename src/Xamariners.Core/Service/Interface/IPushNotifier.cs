using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;

namespace Xamariners.Core.Service.Interface
{
    public interface IPushNotifier
    {
        Task<Dictionary<Tuple<DeviceType, string>, bool>>  SendNotification(Guid memberId, Guid communicationId,
            string message, Dictionary<string, object> customItems);

        void SendNotificationToAll(Guid communicationId, string message, Dictionary<string, object> customItems);
    }
}
