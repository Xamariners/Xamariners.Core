// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalisationService.cs" company="">
//   
// </copyright>
// <summary>
//   The globalisation service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Security.Principal;
using PCLAppConfig;
using Xamariners.Core.Interface;
using Xamariners.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;

using Xamariners.Core.Configuration;

namespace Xamariners.Core.Services
{
    /// <summary>
    /// The globalisation service.
    /// </summary>
    public class GlobalisationService : IGlobalisationService
    {
        #region Constants

        /// <summary>
        /// The _default resx filetype key.
        /// </summary>
        private const string _defaultResxFiletypeKey = "default.resx.filetype";

        /// <summary>
        /// The _resx assembly key.
        /// </summary>
        private const string _resxAssemblyKey = "resx.assembly";

        private const string _default_resx_lang = "en";

        #endregion

        #region Static Fields

        /// <summary>
        /// The _resource managers.
        /// </summary>
        private static readonly Dictionary<string, ResourceManager> _resourceManagers =
            new Dictionary<string, ResourceManager>();
        #endregion

        #region Fields

        /// <summary>
        /// The _locale service.
        /// </summary>
        private readonly ILocaleService _localeService;

        /// <summary>
        /// The _lock.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// The _language iso code.
        /// </summary>
        private string _languageIsoCode;

        /// <summary>
        /// The _resx file name.
        /// </summary>
        private string _resxFileName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalisationService"/> class.
        /// </summary>
        /// <param name="configReader">
        /// The config.
        /// </param>
        /// <param name="localeService">
        /// The locale service.
        /// </param>
        /// <param name="alertService">
        /// The alert service.
        /// </param>
        public GlobalisationService(ILocaleService localeService)
        {
            _localeService = localeService;
            _languageIsoCode = localeService.GetLangCode();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get localised string.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="humanise">
        /// The humanise.
        /// </param>
        /// <typeparam name="TViewModel">
        /// </typeparam>
        /// <typeparam name="TParentClass"></typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetLocalisedString<TParentClass>(Enum key, bool humanise = false)
        {
            // not tried because no working viewmodels yet
            return GetLocalisedString(key, typeof(TParentClass), humanise);
        }

        public string GetLocalisedString(string type, string key)
        {
            return GetLocalisedString(type, key, null, false);
        }

        /// <summary>
        /// The get localised string.
        /// </summary>
        /// <param name="viewModelType">
        /// The view model type.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="humanise">
        /// The humanise.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public string GetLocalisedString(Enum key, Type parentType, bool humanise = false)
        {
            try
            {
                string newKey = string.Format(parentType != null ? "{0}_{1}" : "{1}", parentType.Name, key);
                return GetLocalisedString(key.GetType().Name, newKey, key);
            }
            catch (Exception e)
            {
                throw new Exception("Could not resolve type of ViewModel", e);
            }
        }

        public string GetLocalisedString(Enum key, bool humanise = false)
        {
            return GetLocalisedString(key.GetType().Name, key.ToString(), null, humanise);
        }

        /// <summary>
        /// The get localised string.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="fallbackKey"></param>
        /// <param name="humanise">
        /// The humanise.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// </exception>
        private string GetLocalisedString(string enumType, string key, Enum fallbackKey, bool humanise = false)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Argument cannot be null");

            if (string.IsNullOrEmpty(enumType))
                throw new ArgumentNullException(nameof(enumType), "Argument cannot be null");

            ResourceManager resourceManager;

            var languageIsoCode = _localeService.GetLangCode();
            var resxFileName = GetResXFileName();
            var culture = languageIsoCode != null ? new CultureInfo(FormatLanguageCode(languageIsoCode)) : CultureInfo.CurrentCulture;


            lock (_lock)
            {
                if (!_resourceManagers.ContainsKey(enumType))
                {
                    string filetype = "";
                    try
                    {
                        filetype = string.Format(resxFileName, enumType, enumType);
                        resourceManager = new ResourceManager(Type.GetType(filetype));
                        _resourceManagers.Add(enumType, resourceManager);

                    }
                    catch (Exception e)
                    {
                        throw new Exception($"{filetype}: Type not found", e);
                    }
                }
                else
                {
                    resourceManager = _resourceManagers[enumType];
                }
            }

            try
            {
                var result = resourceManager.GetString(key, culture);


                if (result != null)
                    return result;

                if (fallbackKey != null && fallbackKey.ToString() != key)
                    return GetLocalisedString(fallbackKey);

                // not found!
                throw new KeyNotFoundException($"{key}: key not found in resource file");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException($"{key}: key not found in resource file: {e.Message}", e);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The set res x file name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// </exception>
        private string GetResXFileName() //string languageIsoCode
        {
            string filename;
            string assemblyName;
            try
            {
                // hardcoded instead of in config, left as such for easy merge with MDA
                filename = ConfigurationManager.AppSettings[_defaultResxFiletypeKey];
                assemblyName = ConfigurationManager.AppSettings[_resxAssemblyKey];
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException("Config does not have key value set", e);
            }

            return $"{filename}, {assemblyName}";
        }

        private string FormatLanguageCode(string languageIsoCode)
        {
            if (languageIsoCode.Contains("-"))
                languageIsoCode = languageIsoCode.Substring(0, 2);

            if (string.IsNullOrEmpty(languageIsoCode))
                throw new ArgumentNullException(nameof(languageIsoCode), "language ISO code is null");

            if (languageIsoCode.Length != 2)
                throw new ArgumentException($"language ISO code is of invalid length: {languageIsoCode}");

            return languageIsoCode;
        }
        #endregion
    }
}