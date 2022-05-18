using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Model.Enums;

namespace Xamariners.Core.Service.Interface.Requests
{
    public class DataUpdateRequest
    {
        public DateTime Timestamp { get; set; }

        public Guid TargetId { get; set; }

        public ObjectType TargetType { get; set; }
    }
}
