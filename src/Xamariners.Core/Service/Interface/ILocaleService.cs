using System.Threading.Tasks;

namespace Xamariners.Core.Service.Interface
{
    public interface ILocaleService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get lang code.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetLangCode();

        /// <summary>
        /// The get region code.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        Task<string> GetRegionCode();

        #endregion

        void SetLangCode(string langCode);

        Task<string> GetPhoneCountryCode();
    }
}
