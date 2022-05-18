using System;

namespace Xamariners.Core.Service.Interface
{
    /// <summary>
    /// The GlobalisationService interface.
    /// </summary>
    public interface IGlobalisationService
    {
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
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetLocalisedString<TParentClass>(System.Enum key, bool humanise = false);

        
        string GetLocalisedString(string type, string key);

        /// <summary>
        /// The get localised string.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="humanise">
        /// The humanise.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetLocalisedString(System.Enum key, bool humanise = false);

        
        string GetLocalisedString(System.Enum key, Type parentType, bool humanise = false);

        #endregion
    }
}
