using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xamariners.Core.Requests
{
    public class RequestBase
    {
        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public int Start { get; set; }

        [DataMember]
        public string Order { get; set; }

        [DataMember]
        public IEnumerable<string> Includes { get; set; }

        public Dictionary<string, IEnumerable<string>> ExplicitProperties { get; set; }
    }
}