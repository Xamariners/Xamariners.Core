// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enum.cs" company="">
//   
// </copyright>
// <summary>
//   The service status.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Xamariners.Core.Common.Enum
{
    public enum MessengerTopic
    {
        PushNotification_New,
        PushNotification_Processed,
    }
    
    /// <summary>
    ///     The communication channels
    /// </summary>
    public enum CommunicationChannel
    {
        PushNotification,
        LocalNotification,
        SMS,
        Email
    }

    /// <summary>
    ///     The communication channels
    /// </summary>
    public enum CommunicationStatus
    {
        Created,
        Queued,
        Sent,
        Completed,
        Failed
    }

    /// <summary>
    ///     The image container.
    /// </summary>
    public enum MediaContainer
    {
        /// <summary>
        ///     The main.
        /// </summary>
        Main = 0,

        /// <summary>
        ///     The icon.
        /// </summary>
        Icon = 1,

        /// <summary>
        ///     The secondary.
        /// </summary>
        Secondary = 2
    }

    public enum MediaType
    {
        Image = 0
    }

    
    /// <summary>
    ///     The recurrence type.
    /// </summary>
    public enum RecurrenceType
    {

        None = 0,
        /// <summary>
        ///     Repeats every x days
        /// </summary>
        Daily = 1,

        /// <summary>
        ///     repeats every x weeks on [m|t|w|t|f|s|s]
        /// </summary>
        Weekly = 2,

        /// <summary>
        ///     repeats every x months on [day of the week|day of the month]
        /// </summary>
        Monthly = 3,

        /// <summary>
        ///     repeats every x years
        /// </summary>
        Yearly = 4
    }

    /// <summary>
    ///     The time unit.
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        ///     The second.
        /// </summary>
        Second = 0,

        /// <summary>
        ///     The minute.
        /// </summary>
        Minute = 1,

        /// <summary>
        ///     The hour.
        /// </summary>
        Hour = 2,

        /// <summary>
        ///     The day.
        /// </summary>
        Day = 3,

        /// <summary>
        ///     The week.
        /// </summary>
        Week = 4,

        /// <summary>
        ///     The month.
        /// </summary>
        Month = 5,

        /// <summary>
        ///     The year.
        /// </summary>
        Year = 6
    }
    
    /// <summary>
    ///     The service status.
    /// </summary>
    public enum NavigationDirection
    {
        /// <summary>
        ///     The Forward.
        /// </summary>
        Forward, 

        /// <summary>
        ///     The Backwards.
        /// </summary>
        Backwards, 

        /// <summary>
        ///     The InPlace.
        /// </summary>
        InPlace, 
    }

    /// <summary>
    ///     The service status.
    /// </summary>
    public enum AppStatus
    {
        /// <summary>
        ///     The JustStarted.
        /// </summary>
        Running, 

        /// <summary>
        ///     The JustStarted.
        /// </summary>
        JustStarted, 

        /// <summary>
        ///     The error.
        /// </summary>
        JustWokeUp, 

        /// <summary>
        ///     The GoingToSleep.
        /// </summary>
        GoingToSleep, 

        /// <summary>
        ///     The Sleeping.
        /// </summary>
        Sleeping, 

        Unknown
    }

    /// <summary>
    /// The cache type.
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// The no cache.
        /// </summary>
        NoCache,
        
        /// <summary>
        /// InMemoryCache
        /// </summary>
        InMemoryCache,

        /// <summary>
        /// The azure cache.
        /// </summary>
        AzureCache,
        
        /// <summary>
        /// The in process memory cache.
        /// </summary>
        InProcessMemoryCache,

        /// <summary>
        /// The out of process memory cache.
        /// </summary>
        OutOfProcessMemoryCache,

        /// <summary>
        /// The redis cache.
        /// </summary>
        RedisCache,
    }


    /// <summary>
    ///     The left button type.
    /// </summary>
    public enum LeftButtonType
    {
        /// <summary>
        ///     The home.
        /// </summary>
        Home, 

        /// <summary>
        ///     The back.
        /// </summary>
        Back, 

        /// <summary>
        ///     The none.
        /// </summary>
        None, 
    }

    /// <summary>
    ///     The keypad type.
    /// </summary>
    public enum KeypadType
    {
        /// <summary>
        ///     The default.
        /// </summary>
        Default, 

        /// <summary>
        ///     The ascii capable.
        /// </summary>
        ASCIICapable, 

        /// <summary>
        ///     The numbers and punctuation.
        /// </summary>
        NumbersAndPunctuation, 

        /// <summary>
        ///     The url.
        /// </summary>
        Url, 

        /// <summary>
        ///     The number pad.
        /// </summary>
        NumberPad, 

        /// <summary>
        ///     The phone pad.
        /// </summary>
        PhonePad, 

        /// <summary>
        ///     The name phone pad.
        /// </summary>
        NamePhonePad, 

        /// <summary>
        ///     The email address.
        /// </summary>
        EmailAddress, 

        /// <summary>
        ///     The decimal pad.
        /// </summary>
        DecimalPad, 

        /// <summary>
        ///     The twitter.
        /// </summary>
        Twitter, 

        /// <summary>
        ///     QR Support
        /// </summary>
        NumericQR, 
    }

    /// <summary>
    ///     The state key.
    /// </summary>
    public enum StateKey
    {
        /// <summary>
        ///     desc.
        /// </summary>
        Breadcrumb, 
    }

    /// <summary>
    ///     The object type.
    /// </summary>
    public enum Order
    {
        /// <summary>
        ///     desc.
        /// </summary>
        Desc = 0, 

        /// <summary>
        ///     asc.
        /// </summary>
        Asc = 1, 
    }

    /// <summary>
    ///     The activity status.
    /// </summary>
    public enum ActivityStatus
    {
        /// <summary>
        ///     The Initiated
        /// </summary>
        Initiated = 0, 

        /// <summary>
        ///     The Processing
        /// </summary>
        Processing = 1, 

        /// <summary>
        ///     The Completed.
        /// </summary>
        Completed = 2, 

        /// <summary>
        ///     The Failed.
        /// </summary>
        Failed = 3, 

        /// <summary>
        ///     The  Pending.
        /// </summary>
        Pending = 4, 

        /// <summary>
        ///     The Cancelled.
        /// </summary>
        Cancelled = 5, 
    }
    
    /// <summary>
    ///     The contact type.
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        ///     The facebook.
        /// </summary>
        Facebook,

        /// <summary>
        ///     The twitter.
        /// </summary>
        Twitter,

        /// <summary>
        ///     The pinterest.
        /// </summary>
        Pinterest,

        /// <summary>
        ///     The four square.
        /// </summary>
        FourSquare,

        /// <summary>
        ///     The gowalla.
        /// </summary>
        Gowalla,

        /// <summary>
        ///     The google.
        /// </summary>
        Google,

        /// <summary>
        ///     The xamariners.
        /// </summary>
        Xamariners,

        /// <summary>
        ///     The mobile.
        /// </summary>
        Mobile,

        /// <summary>
        ///     The landline.
        /// </summary>
        Landline,

        /// <summary>
        ///     The mac user.
        /// </summary>
        MacUser,

        /// <summary>
        ///     The email.
        /// </summary>
        Email,

        /// <summary>
        ///     The Pay Pal account.
        /// </summary>
        PayPal,

        /// <summary>
        ///     The Unknown.
        /// </summary>
        Unknown,

    }

    /// <summary>
    ///     The device type.
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        ///     The android.
        /// </summary>
        Android, 

        /// <summary>
        ///     The i os.
        /// </summary>
        iOS, 

        /// <summary>
        ///     The windows mobile.
        /// </summary>
        WindowsMobile,

        /// <summary>
        ///     The plain dontnet.
        /// </summary>
        DotNet,
    }

    /// <summary>
    ///     The ef include.
    /// </summary>
    public enum EntityInclude
    {
        /// <summary>
        ///     The none.
        /// </summary>
        None, 

        /// <summary>
        ///     The all.
        /// </summary>
        All, 

        /// <summary>
        ///     The Ignore.
        /// </summary>
        Ignore, 
    }

    /// <summary>
    ///     The log type.
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        ///     Info
        /// </summary>
        Info, 

        /// <summary>
        ///     Warning
        /// </summary>
        Warning, 

        /// <summary>
        ///     Error
        /// </summary>
        Error, 
    }
}