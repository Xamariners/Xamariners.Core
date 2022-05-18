using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Interface;

namespace Xamariners.Core.Common.Infrastructure
{
    public class ApplicationDomain : IApplicationDomain
    {
        public string BaseDirectory { get; set; }
        public string ConfigFilePath { get; set; }
        public string ExecutingAssemblyName { get; set; }
        public DeviceType DeviceType { get; set; }
        public bool IsTest { get; set; }
    }
}