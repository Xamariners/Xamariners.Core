// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiscHelpers.cs" company="">
//   
// </copyright>
// <summary>
//   The misc helpers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Helpers
{
    /// <summary>
    ///     The misc helpers.
    /// </summary>
    public static class MiscHelpers
    {
        #region Public Methods and Operators

        
        public static bool ValidateEmail(string username)
        {
            var isValidEmail = false;

            if (!String.IsNullOrEmpty(username))
            {
                isValidEmail = Regex.IsMatch(username,
                    @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                    RegexOptions.IgnoreCase);
            }

            return isValidEmail;
        }

        public static bool ValidatePassword(string password)
        {
            // why is that
            return true;
        }

        /// <summary>
        /// The GetExceptionContent exception.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetExceptionContent(Exception e)
        {
            var sb = new StringBuilder();
            try
            {
                if (!String.IsNullOrEmpty(e.Message))
                {
                    sb.AppendFormat("Message: {0};", e.Message);
                }

                if (!String.IsNullOrEmpty(e.Source))
                {
                    sb.AppendFormat("Source: {0};", e.Source);
                }

                if (!String.IsNullOrEmpty(e.StackTrace))
                {
                    sb.AppendFormat("Stack: {0};", e.StackTrace);
                }
            }
            catch
            {
            }

            if (e.InnerException != null)
            {
                GetExceptionContent(e.InnerException);
            }

            return sb.ToString();
        }

        /// <summary>
        /// The get order by lambda.
        /// </summary>
        /// <param name="sortOn">
        /// The sort on.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<T, object>> GetOrderByLambda<T>(string sortOn)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "p");
            string[] parts = sortOn.Split('.');

            Expression parent = param;

            foreach (string part in parts)
            {
                parent = Expression.Property(parent, part);
            }

            Expression<Func<T, object>> sortExpression = Expression.Lambda<Func<T, object>>(parent, param);

            return sortExpression;
        }
        
        /// <summary>
        /// The spit exception.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public static void SpitException(Exception e)
        {
            try
            {
                TraceHelpers.WriteToTrace("Message: {0}", e.Message);
                TraceHelpers.WriteToTrace("Source: {0}", e.Source);
                TraceHelpers.WriteToTrace("Stack: {0}", e.StackTrace);
                TraceHelpers.WriteToTrace("Source: {0}", e.Source);
            }
            catch
            {
            }

            if (e.InnerException != null)
            {
                SpitException(e.InnerException);
            }
        }

        public static void ThrowIfNull<T>(Expression<Func<T>> propertyExpression, string message = null)
        {
            MemberExpression body;
            body = propertyExpression.Body as MemberExpression;

            if (body == null)
                body = (propertyExpression.Body as UnaryExpression).Operand as MemberExpression;

            T value = propertyExpression.Compile().Invoke();

            if (value == null)
            {
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentNullException(body.Member.Name);
                else
                    throw new ArgumentNullException(body.Member.Name, message);
            }
        }

        public static void ThrowIfNull(params Expression<Func<object>>[] propertyExpressions)
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                ThrowIfNull<object>(propertyExpression);
            }
        }

        #endregion

        public static string[] FullnameSplit(string fullname)
        {
            string firstName;
            string lastName;
            MiscHelpers.FullnameSplit(fullname, out firstName, out lastName);
            return new string[2]
            {
        firstName,
        lastName
            };
        }

        public static void FullnameSplit(string fullname, out string firstName, out string lastName)
        {
            if (string.IsNullOrEmpty(fullname) || !fullname.Contains(" "))
            {
                firstName = fullname;
                lastName = string.Empty;
                return;
            }

            string[] strArray = fullname?.Trim().Split(new char[1]{' '}, 2);

            if (strArray.Length == 1)
            {
                firstName = "";
                lastName = strArray[0];
            }
            else
            {
                firstName = strArray[0];
                lastName = strArray[1];
            }
        }


        /// <summary>
        /// The get string.
        /// 
        /// </summary>
        /// <param name="parameters">The parameters.
        ///             </param><param name="key">The key.
        ///             </param><param name="defaultValue">The default value.
        ///             </param>
        /// <returns>
        /// The <see cref="T:System.String"/>.
        /// 
        /// </returns>
        public static string GetString(this Dictionary<string, string> parameters, string key, string defaultValue = null)
        {
            string str;
            if (parameters.TryGetValue(key, out str))
                return str;
            return defaultValue;
        }

        /// <summary>
        /// retry an action
        /// </summary>
        /// <param name="action">what to retry</param>
        /// <param name="interval">interval in ms</param>
        /// <param name="condition">keep trying until this is met</param>
        /// <param name="retryCount">try x times</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        public static bool Retry(Func<bool> action, int interval, int retryCount = 3)
        {
            bool result = false;
            var exceptions = new List<Exception>();
            var retryInterval = TimeSpan.FromMilliseconds(interval);

            for (var retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    result = action.Invoke();

                    if (!result)
                        Task.Delay(retryInterval).Wait();
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    if (exceptions.All(x => x.Message != ex.Message))
                        exceptions.Add(ex);

                    Task.Delay(retryInterval).Wait();
                }
            }

            if (!result && !exceptions.Any())
                return false;

            if (exceptions.Count() == 1) throw exceptions[0];

            if (exceptions.Count() > 1)
                throw new AggregateException(exceptions);

            return result;
        }

        public static async Task<bool> Retry(Func<Task<bool>> action, int interval, int retryCount = 3)
        {
            bool flag = false;
            List<Exception> source = new List<Exception>();
            TimeSpan delay = TimeSpan.FromMilliseconds((double)interval);
            for (int index = 0; index < retryCount; ++index)
            {
                try
                {
                    flag = await action();
                    if (flag)
                        return true;
                    Task.Delay(delay).Wait();
                }
                catch (Exception ex)
                {
                    if (source.All<Exception>((Func<Exception, bool>)(x => x.Message != ex.Message)))
                        source.Add(ex);
                    Task.Delay(delay).Wait();
                }
            }
            if (!flag && !source.Any<Exception>())
                return false;
            if (source.Count<Exception>() == 1)
                throw source[0];
            if (source.Count<Exception>() > 1)
                throw new AggregateException((IEnumerable<Exception>)source);
            return flag;
        }
    }
}