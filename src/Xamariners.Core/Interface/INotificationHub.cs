using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;

namespace Xamariners.Core.Interface
{
    public interface INotificationHub
    {
        Task CreateOrUpdateInstallationAsync(string installationId, string pushChannel, DeviceType deviceType);
    }
}
