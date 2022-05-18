using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Interface;

namespace Xamariners.Core.Common
{
    public class FakeNotificationHub : INotificationHub
    {
        public async Task CreateOrUpdateInstallationAsync(string installationId, string pushChannel, DeviceType deviceType)
        {
        }
    }
}
