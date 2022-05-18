using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using Xamariners.Core.Interface;

namespace Xamariners.Core.Common.Helpers
{
    public static class ApplicationDomainHelpers
    {
        public static string GetBaseDirectory()
        {
            var baseDirectory = ServiceLocator.Current.GetInstance<IApplicationDomain>().BaseDirectory;
                if (!string.IsNullOrEmpty(baseDirectory))
                    return baseDirectory;

            var currentDomain = typeof(string).GetTypeInfo().Assembly.GetType("System.AppDomain").GetRuntimeProperty("CurrentDomain").GetMethod.Invoke(null, new object[] { });
            var baseDirectoryInfo = currentDomain.GetType().GetRuntimeProperty("BaseDirectory");
            return (string)baseDirectoryInfo.GetValue(currentDomain);
        }

        public static string GetConfigFilePath()
        {
            var configFilePath = ServiceLocator.Current.GetInstance<IApplicationDomain>().ConfigFilePath;

            if (!string.IsNullOrEmpty(configFilePath))
                return configFilePath;

            var currentDomain = typeof(string).GetTypeInfo().Assembly.GetType("System.AppDomain").GetRuntimeProperty("CurrentDomain").GetMethod.Invoke(null, new object[] { });
            var setupInformationInfo = currentDomain.GetType().GetRuntimeProperty("SetupInformation");
            var setupInformation = setupInformationInfo.GetValue(currentDomain);
            var configurationFileInfo = setupInformation.GetType().GetRuntimeProperty("ConfigurationFile");
            var configurationFile = configurationFileInfo.GetValue(setupInformation);
            return (string)configurationFile;
        }

        public static Assembly GetAssembly(string name = "")
        {
            var asmName = !string.IsNullOrEmpty(name) ? name
               : ServiceLocator.Current.GetInstance<IApplicationDomain>()
                   .ExecutingAssemblyName.ReplaceFromLastCharInstance('.', "App");

            return string.IsNullOrEmpty(asmName) ? null : Assembly.Load(new AssemblyName(asmName));
        }

        public static Assembly GetAssembly(Type type = null)
        {
            var asmName = type?.GetTypeInfo().Assembly.GetName().Name;

            asmName = !string.IsNullOrEmpty(asmName) ? asmName
               : ServiceLocator.Current.GetInstance<IApplicationDomain>()
                   .ExecutingAssemblyName.ReplaceFromLastCharInstance('.', "App");

            return string.IsNullOrEmpty(asmName) ? null : Assembly.Load(new AssemblyName(asmName));
        }
    }
}