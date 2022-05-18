using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xamariners.Core.Common;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model.Attributes;

namespace Xamariners.Core.Model
{
    public partial class MemberBase
    {
        private IList<Tuple<DeviceType, string>> _deviceTokens;

        [JsonSerialise(UserRole.Member)]
        [DataMember]
        public string DeviceTokensData { get; set; }

        [JsonSerialise(UserRole.None)]
        public IEnumerable<Tuple<DeviceType, string>> DeviceTokens
        {
            get
            {
                if (_deviceTokens != null && _deviceTokens.Any())
                {
                    // we already have some device token
                    return _deviceTokens;
                }
                else if (!string.IsNullOrEmpty(DeviceTokensData))
                {
                    // we have data, but not serialized yet
                    _deviceTokens = (JsonSerialiser.Deserialise<List<Tuple<DeviceType, string>>>(DeviceTokensData));
                    return _deviceTokens;
                }
                else
                {
                    // we have no data, let's return an empty list
                    _deviceTokens = new List<Tuple<DeviceType, string>>();
                    return _deviceTokens;
                }
            }
        }
        
        public void AddDeviceToken(DeviceType deviceType, string deviceToken)
        {
            if (_deviceTokens == null)
                _deviceTokens = JsonSerialiser.Deserialise<List<Tuple<DeviceType, string>>>(DeviceTokensData) ?? new List<Tuple<DeviceType, string>>();

            _deviceTokens.Add(Tuple.Create(deviceType, deviceToken));
            DeviceTokensData = JsonSerialiser.Serialise(_deviceTokens);
        }

        public void RemoveDeviceToken(DeviceType deviceType, string deviceToken)
        {
            if (_deviceTokens == null)
                _deviceTokens = JsonSerialiser.Deserialise<List<Tuple<DeviceType, string>>>(DeviceTokensData) ?? new List<Tuple<DeviceType, string>>();

            _deviceTokens.Remove(Tuple.Create(deviceType, deviceToken));
            DeviceTokensData = JsonSerialiser.Serialise(_deviceTokens);
        }
    }
}
