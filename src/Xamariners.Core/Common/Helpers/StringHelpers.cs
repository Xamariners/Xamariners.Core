// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringHelpers.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 24 01 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Web;
using Newtonsoft.Json;

namespace Xamariners.Core.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    using Newtonsoft.Json.Linq;

    using Xamariners.Core.Common.Attributes;

    /// <summary>
    ///     The string helpers.
    /// </summary>
    public static class StringHelpers
    {
        #region Static Fields

        /// <summary>
        ///     The hex chars.
        /// </summary>
        private static readonly char[] hexChars = "0123456789abcdef".ToCharArray();

        #endregion

        #region Public Methods and Operators


        public static string[] SplitAtLastIndex(this string source, char separator)
        {
            int index = source.LastIndexOf(separator);
            var result = index <=0 ? new string[2] { source, string.Empty } : 
                new string[2] { source.Substring(0, index), source.Substring(index + 1)};
            return result;
        }

        public static string UppercaseFirst(this string source)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(source[0]) + source.Substring(1);
        }

        public static string LowercaseFirst(this string source)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToLower(source[0]) + source.Substring(1);
        }

        /// <summary>
        /// Converts nullable double number to string
        /// </summary>
        /// <returns>
        /// The double to string.
        /// </returns>
        /// <param name="number">
        /// Number.
        /// </param>
        /// <param name="zeroToEmptyString">
        /// If set to <c>true</c> zero to empty string.
        /// </param>
        public static string ConvertNullableDoubleToString(double? number, bool zeroToEmptyString)
        {
            string strDouble = Convert.ToString(number);

            if (zeroToEmptyString && strDouble == "0")
            {
                strDouble = String.Empty;
            }

            return strDouble;
        }

        /// <summary>
        /// Returns day number with two character suffix text for the
        ///     day number component in the given date value
        /// </summary>
        /// <param name="date">
        /// Date
        /// </param>
        /// <returns>
        /// String of day number including suffix (1st, 2nd, 3rd, 4th etc.)
        /// </returns>
        public static string FormatDayNumberWithSuffixAndMonthAbbr(DateTime date)
        {
            string dateFormatted = String.Empty;
            if (date != default(DateTime))
            {
                dateFormatted = String.Format(
                    CultureInfo.InvariantCulture, 
                    "{0}{1} {2:MMM}", 
                    date.ToString(" d", CultureInfo.InvariantCulture), 
                    GetDayNumberSuffix(date), 
                    date);
            }

            return dateFormatted.Trim();
        }

        /// <summary>
        /// Generates the alphanumeric string.
        /// </summary>
        /// <returns>
        /// The alphanumeric string.
        /// </returns>
        /// <param name="numberOfChars">
        /// Number of chars.
        /// </param>
        public static string GenerateAlphanumericString(int numberOfChars)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result =
                new String(Enumerable.Repeat(chars, numberOfChars).Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }



        /// <summary>
        /// Gets the corresponding data values for requested json data keys.
        /// </summary>
        /// <returns>
        /// The data values.
        /// </returns>
        /// <param name="data">
        /// Json data string.
        /// </param>
        /// <param name="keys">
        /// Json element keys.
        /// </param>
        public static T GetDataValue<T>(this string data, string key)
        {
            JToken value;
            JObject json = JObject.Parse(data);
            var isValue = json.TryGetValue(key, out value);
            return  isValue ? value.Value<T>() : default(T);
        }


        /// <summary>
        /// Gets the corresponding data values for requested json data keys.
        /// </summary>
        /// <returns>
        /// The data values.
        /// </returns>
        /// <param name="data">
        /// Json data string.
        /// </param>
        /// <param name="keys">
        /// Json element keys.
        /// </param>
        public static object[] GetDataValues(this string data, params string[] keys)
        {
            var selectedValues = new List<object>();

            if (!String.IsNullOrEmpty(data))
            {
                var selectedKeys = new List<string>(keys);

                JObject json = JObject.Parse(data);
                foreach (var token in json)
                {
                    if (selectedKeys.Contains(token.Key))
                    {
                        selectedValues.Add(token.Value.Value<object>());
                    }
                    else
                    {
                        selectedValues.Add("MISSING DATA");
                    }
                }
            }

            return selectedValues.ToArray();
        }

        public static Dictionary<string, object> GetDataValues(this string jsonString)
        {
            var data = new Dictionary<string, object>();
            JObject json = JObject.Parse(jsonString);

            foreach (var token in json)
                data.Add(token.Key, token.Value.Value<object>());

            return data;
        }

        /// <summary>
        /// add elements to an array
        /// </summary>
        /// <param name="original"></param>
        /// <param name="newElements"></param>
        /// <returns></returns>
        public static string[] AddItems(this string[] array, string[] items  )
        {
            if (array == null || array.Length == 0) return items;

            int initSize = array.Length;
            int addSize = items.Length;
            int newsize = initSize + addSize;
            Array.Resize(ref array, newsize);
            for (int i = initSize; i < newsize; i++) array[i] = items[i - initSize];

            return array;
        }

        /// <summary>
        /// Returns just the two character suffix text for the
        ///     day number component in the given date value
        /// </summary>
        /// <param name="date">
        /// Date
        /// </param>
        /// <returns>
        /// String of day number suffix (st, nd, rd or th)
        /// </returns>
        public static string GetDayNumberSuffix(DateTime date)
        {
            if (date == default(DateTime))
            {
                return String.Empty;
            }

            int day = date.Day;

            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";

                case 2:
                case 22:
                    return "nd";

                case 3:
                case 23:
                    return "rd";

                default:
                    return "th";
            }
        }

        /// <summary>
        /// The get enum short string value non localized.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetEnumShortStringValueNonLocalized(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetRuntimeField(value.ToString());
            var attrs1 = fi.GetCustomAttributes(typeof(ShortStringValue), false) as ShortStringValue[];
            if (attrs1 != null && attrs1.Length > 0)
            {
                output = attrs1[0].Value;
            }
            else
            {
                var attrs2 = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
                output = attrs2 != null && attrs2.Length > 0 ? attrs2[0].Value : value.ToString();
            }

            return output;
        }

        /// <summary>
        /// Gets the first alphabetical char normalized due to accent.
        ///     If not found, return empty char.
        /// </summary>
        /// <param name="word">
        /// Word.
        /// </param>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        public static char GetFirstAlphabeticalChar(string word)
        {
            char firstChar = ' ';
            const string pattern = @"^[a-zA-Z]$";
            char[] chars = word.ToCharArray();
            foreach (char ch in chars.Where(ch => Regex.IsMatch(ch.ToString(), pattern)))
            {
                firstChar = ch;
                break;
            }

            return firstChar;
        }

        public static string ReplaceLast(this string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);

            if (place == -1) return source;

            return source.Remove(place, find.Length).Insert(place, replace);
        }

        public static string ReplaceStart(this string source, string find, string replace)
        {
            int place = source.IndexOf(find);

            if (place == -1 || place > 0)
                return source;

            return source.Remove(0, find.Length).Insert(0, replace);
        }

        /// <summary>
        /// Returns formatted text with substring as parameter shortened to maximum allowed length
        /// </summary>
        /// <param name="pattern">
        /// Pattern string
        /// </param>
        /// <param name="parameterToShorten">
        /// Substring which is intended to be shortened if needed
        /// </param>
        /// <param name="maxLength">
        /// Maximum length of entire string
        /// </param>
        /// <param name="otherParams">
        /// other optional substrings
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFormattedTextWithShortenedParameter(
            string pattern, 
            string parameterToShorten, 
            int maxLength, 
            params object[] otherParams)
        {
            string textToShorten = parameterToShorten;
            var textParams = new object[] { textToShorten };
            if (otherParams != null && otherParams.Any())
            {
                textParams = textParams.Concat(otherParams).ToArray();
            }

            string formattedText = String.Format(pattern, textParams);

            if (pattern.Length < maxLength)
            {
                int charsOverLimit = formattedText.Length - maxLength;
                if (charsOverLimit > 0)
                {
                    textToShorten = textToShorten.Remove(textToShorten.Length - charsOverLimit - 3) + "...";
                    textParams[0] = textToShorten;
                    formattedText = String.Format(pattern, textParams);
                }
            }

            return formattedText;
        }

        /// <summary>
        /// Get the error description from an HTTPResponseMessage
        /// </summary>
        /// <returns>
        /// The error description.
        /// </returns>
        /// <param name="responseBody">
        /// responseBody.
        /// </param>
        public static string GetErrorHttpResponseMessage(string responseBody)
        {
            try
            {
                string[] errors = responseBody.Split(',');
                string[] error1 = errors[0].Split(':');
                string[] error2 = errors[1].Split(':');
                string messageResult = error2[1].Replace(@"\", "").Replace(@"}", "").Replace("\"", "");
                return messageResult;
            }
            catch
            {
                return responseBody;
            }
        }

        /// <summary>
        /// The url encode.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string UrlEncode(string str)
        { 
            return System.Net.WebUtility.UrlEncode(str);
        }

        /// <summary>
        /// The url encode.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <param name="Enc">
        /// The enc.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string UrlEncode(string s, Encoding Enc)
        {
            if (s == null)
            {
                return null;
            }

            if (s == String.Empty)
            {
                return String.Empty;
            }

            bool flag = false;
            int length = s.Length;
            for (int index = 0; index < length; ++index)
            {
                char c = s[index];
                if ((c < 48 || c < 65 && c > 57 || (c > 90 && c < 97 || c > 122)) && !NotEncoded(c))
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                return s;
            }

            var bytes1 = new byte[Enc.GetMaxByteCount(s.Length)];
            int bytes2 = Enc.GetBytes(s, 0, s.Length, bytes1, 0);
            return Encoding.UTF8.GetString(UrlEncodeToBytes(bytes1, 0, bytes2),0, bytes2); //ASCII
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// The not encoded.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool NotEncoded(char c)
        {
            if (c != 33 && c != 40 && (c != 41 && c != 42) && (c != 45 && c != 46))
            {
                return c == 95;
            }

            return true;
        }

        /// <summary>
        /// The url encode char.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="isUnicode">
        /// The is unicode.
        /// </param>
        private static void UrlEncodeChar(char c, Stream result, bool isUnicode)
        {
            if (c > Byte.MaxValue)
            {
                int num = c;
                result.WriteByte(37);
                result.WriteByte(117);
                int index1 = num >> 12;
                result.WriteByte((byte)hexChars[index1]);
                int index2 = num >> 8 & 15;
                result.WriteByte((byte)hexChars[index2]);
                int index3 = num >> 4 & 15;
                result.WriteByte((byte)hexChars[index3]);
                int index4 = num & 15;
                result.WriteByte((byte)hexChars[index4]);
            }
            else if (c > 32 && NotEncoded(c))
            {
                result.WriteByte((byte)c);
            }
            else if (c == 32)
            {
                result.WriteByte(43);
            }
            else if (c < 48 || c < 65 && c > 57 || (c > 90 && c < 97 || c > 122))
            {
                if (isUnicode && c > SByte.MaxValue)
                {
                    result.WriteByte(37);
                    result.WriteByte(117);
                    result.WriteByte(48);
                    result.WriteByte(48);
                }
                else
                {
                    result.WriteByte(37);
                }

                int index1 = c >> 4;
                result.WriteByte((byte)hexChars[index1]);
                int index2 = c & 15;
                result.WriteByte((byte)hexChars[index2]);
            }
            else
            {
                result.WriteByte((byte)c);
            }
        }

        /// <summary>
        /// The url encode to bytes.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            int length = bytes.Length;
            if (length == 0)
            {
                return new byte[0];
            }

            if (offset < 0 || offset >= length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            if (count < 0 || count > length - offset)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            var memoryStream = new MemoryStream(count);
            int num = offset + count;
            for (int index = offset; index < num; ++index)
            {
                UrlEncodeChar((char)bytes[index], memoryStream, false);
            }

            return memoryStream.ToArray();
        }

        /// <summary>
        /// Gets right indefinite article for word.
        /// </summary>
        /// <returns>
        /// The indefinite article.
        /// </returns>
        /// <param name="word">
        /// Word.
        /// </param>
        public static string GetIndefiniteArticle(string word)
        {
            string indefiniteArticle = String.Empty;

            if (!String.IsNullOrEmpty(word))
            {
                switch (word.ToLower().ToCharArray()[0])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        indefiniteArticle = "an";
                        break;
                    default:
                        indefiniteArticle = "a";
                        break;
                }
            }

            return indefiniteArticle;
        }

        public static Uri GetHost(string uri)
        {
            var apiUri = new Uri(uri);

            if (apiUri.PathAndQuery.Length > 1)
                return new Uri(apiUri.OriginalString.Replace(apiUri.PathAndQuery, String.Empty));
            else
                return apiUri;
        }
       
        #endregion

        #region Constants and Fields

        /// <summary>
        /// The chars.
        /// </summary>
        private const string chars = "1234567890";

        /// <summary>
        /// The rng.
        /// </summary>
        private static readonly Random rng = new Random();

        #endregion

        #region Public Methods

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(str, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
        }

        public static bool IsAllUpper(this string str)
        {
            return str.Cast<char>().All(t => !char.IsLetter(t) || char.IsUpper(t));
        }

        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The get string.
        /// </returns>
        public static string GetNumericCode(int size)
        {
            var buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = chars[rng.Next(chars.Length)];
            }

            return new string(buffer);
        }

        public static string SafeToLower(this string value)
        {
            if (value == null)
                return null;

            return value.ToLower();
        }

        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// The validate string.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The validate string.
        /// </returns>
        public static bool ValidateString(string format)
        {
            foreach (char c in format)
            {
                if (!chars.Contains(c.ToString()))
                {
                    return false;
                }
            }

            return true;
        }
        
        public static string TrimString(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                value = value.Trim();

            return value;
        }

        public static string TrimLowerString(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                value = value.Trim().ToLower();

            return value;
        }

        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        public static string ReplaceFromLastCharInstance(this string str, char ch, string text)
        {
			if (str == null)
				return string.Empty;
			
            return str.Remove(str.LastIndexOf('.') + 1) + text;
        }

        public static double ExtractDouble(string text)
        {
            var result = Regex.Split(text, @"[^0-9\.]+").FirstOrDefault(c => c != "." && c.Trim() != "");
            return double.TryParse(result, out var dbl) ? dbl : 0;
        }

        public static string StripHTML(string html)
        {
            string noHTML = Regex.Replace(html, @"<[^>]+>|&nbsp;", "");
            return Regex.Replace(noHTML, @"\s{2,}", " ");
        }

        #endregion
    }

    /// <summary>
    /// The base twelve generator.
    /// </summary>
    public class BaseTwelveGenerator
    {
        #region Constants and Fields

        /// <summary>
        /// The chars.
        /// </summary>
        private const string CHARS = "1234567890";

        /// <summary>
        /// The rng.
        /// </summary>
        private readonly Random _random = new Random();

        #endregion

        #region Public Methods

        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The get string.
        /// </returns>
        public string GetString(int size)
        {
            var buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = CHARS[_random.Next(CHARS.Length)];
            }

            return new string(buffer);
        }

        /// <summary>
        /// The validate string.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The validate string.
        /// </returns>
        public bool ValidateString(string format)
        {
            foreach (char c in format)
            {
                if (!CHARS.Contains(c.ToString()))
                {
                    return false;
                }
            }

            return true;
        }


        #endregion
    }
}