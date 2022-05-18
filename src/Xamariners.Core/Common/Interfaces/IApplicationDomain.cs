using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;

namespace Xamariners.Core.Interface
{
    public interface IApplicationDomain
    {
        string BaseDirectory { get; set; }
        string ConfigFilePath { get; set; }
        string ExecutingAssemblyName { get; set; }
        DeviceType DeviceType { get; set; }
        bool IsTest { get; set; }
    }
}
