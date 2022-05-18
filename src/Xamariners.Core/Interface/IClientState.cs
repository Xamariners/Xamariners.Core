// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientState.cs" company="">
//
// </copyright>
// <summary>
//   The State interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using Xamariners.Core.Common;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Common.Model;
using Xamariners.Core.Model.Enums;
using Xamariners.Core.Model.Interface;
using Xamariners.RestClient.Models;

namespace Xamariners.Core.Interface
{
    /// <summary>
    /// The State interface.
    /// </summary>
    public interface IClientState
    {
        #region Public Properties

        Guid StateId { get; set; }

        /// <summary>
        /// This is for clearing the State implementation.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Gets or sets the api info.
        /// </summary>
        string ApiInfo { get; set; }

        /// <summary>
        ///     Gets or sets the app configuration.
        /// </summary>
        string AppConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the cache last updated.
        /// </summary>
        DateTime CacheLastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the device token.
        /// </summary>
        string DeviceToken { get; set; }

        string DeviceInstallation { get; set; }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        DeviceType DeviceType { get; set; }

        /// <summary>
        /// Gets a value indicating whether is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        IMember Member { get; set; }

        /// <summary>
        /// type to query dataupdate with
        /// </summary>
        ObjectType DataUpdateTargetType { get; set; }

        /// <summary>
        ///     Gets or sets the current search phrase.
        /// </summary>
        string CurrentSearchPhrase { get; set; }

        /// <summary>
        /// Gets or sets the push notification.
        /// </summary>
        PushNotification PushNotification { get; set; }

        /// <summary>
        /// Sets the device token.
        /// </summary>
        void SetDeviceToken(string deviceToken, DeviceType deviceType);

        AppStatus AppStatus { get; set; }

        string CountryCode { get; set; }

        DateTime LastLogoutTime { get; set; }

        bool IsNotificationGranted { get; set; }

        bool IsPushNotificationSubscribed { get; set; }

        Credential Credential { get; }

        #endregion
    }
}