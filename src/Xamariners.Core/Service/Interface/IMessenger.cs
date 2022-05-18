// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessenger.cs" company="">
//   
// </copyright>
// <summary>
//   Interface for a Publish / ToObservable Messenger
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Xamariners.Core.Service.Interface
{
    using System;

    /// <summary>
    ///     Interface for a Publish / ToObservable Messenger
    /// </summary>
    public interface IMessenger
    {
        #region Public Methods and Operators

        /// <summary>
        /// Publishes the specified value.
        /// </summary>
        /// <typeparam name="T">
        /// The Type of message to publish
        /// </typeparam>
        /// <param name="value">
        /// The value to publish
        /// </param>
        void Publish<T>(T value);

        /// <summary>
        /// Publishes the specified value against a specified key.
        /// </summary>
        /// <typeparam name="T">
        /// The Type of value to publish
        /// </typeparam>
        /// <param name="key">
        /// The key for the published value
        /// </param>
        /// <param name="value">
        /// The value to publish
        /// </param>
        void Publish<T>(string key, T value);

        /// <summary>
        ///     Returns the Messenger as an Observable which can be subscribed to.
        /// </summary>
        /// <typeparam name="T">The Type of message to subscribe</typeparam>
        /// <returns>An IObservable of type T</returns>
        IObservable<T> ToObservable<T>();

        /// <summary>
        /// Returns the Messenger as an Observable which can be subscribed to for a specified key.
        /// </summary>
        /// <typeparam name="T">
        /// The Type of message to subscribe.
        /// </typeparam>
        /// <param name="key">
        /// The key for the value to subscribe.
        /// </param>
        /// <returns>
        /// An IObservable of type T
        /// </returns>
        IObservable<T> ToObservable<T>(string key);

        #endregion
    }
}