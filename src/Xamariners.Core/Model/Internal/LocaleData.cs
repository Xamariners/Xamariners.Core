// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocaleData.cs" company="">
//   
// </copyright>
// <summary>
//   LocaleData
//   @TODO: to implement properly
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CommonServiceLocator;
using System.Globalization;
using Xamariners.Core.Common;
using Xamariners.Core.Service.Interface;

namespace Xamariners.Core.Model.Internal
{
    /// <summary>
    ///     LocaleData
    ///     @TODO: to implement properly
    /// </summary>
    public class LocaleData
    {
        #region Public Properties

        /// <summary>
        ///     CurrentCulture
        /// </summary>
        public CultureInfo CurrentCulture { get; set; }

        /// <summary>
        ///     CurrentCulture
        /// </summary>
        public string CurrentCurrency
        {
            get
            {
                return CurrentRegion.CurrencySymbol != string.Empty ? CurrentRegion.CurrencySymbol : "$";
            }
        }

        /// <summary>
        ///     CurrentCulture
        /// </summary>
        public RegionInfo CurrentRegion
        {
            get
            {
                return UserRegion;
            }
        }

        /// <summary>
        ///     TaxRateId
        /// </summary>
        public int TaxRateId { get; set; }

        /// <summary>
        ///     TaxValue
        /// </summary>
        public double TaxValue { get; set; }

        /// <summary>
        ///     CurrentCulture
        /// </summary>
        public string UserCurrency
        {
            get
            {
                return UserRegion.CurrencySymbol != string.Empty ? UserRegion.CurrencySymbol : "$";
            }
        }

        /// <summary>
        ///     CurrentCulture
        /// </summary>
        public RegionInfo UserRegion
        {
            get
            {
                var code = ServiceLocator.Current.GetInstance<ILocaleService>().GetLangCode();
                return new RegionInfo(code);
            }
        }

        #endregion
    }
}