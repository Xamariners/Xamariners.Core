using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Xamariners.Core.Configuration.Infrastructure
{
    public interface IRaygunSettings
    {
        string ApiKey { get; set; }
    }

    [XmlRoot("RaygunSettings")]
    public class RaygunSettings : IRaygunSettings
    {
        [XmlAttribute("apikey")]
        public string ApiKey { get; set; }
    }
}
