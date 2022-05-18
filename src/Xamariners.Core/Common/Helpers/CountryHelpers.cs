using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CommonServiceLocator;
using Newtonsoft.Json.Linq;
using Xamariners.Core.Interface;
using Xamariners.RestClient.Helpers.Extensions;
using Xamariners.RestClient.Infrastructure;
using Xamariners.RestClient.Interfaces;

namespace Xamariners.Core.Common.Helpers
{
    public class CountryHelpers
    {
        /// <summary>
        /// Gets the numeric country code online (+65, +94, +1)
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCountryCodeOnline()
        {
            if (ServiceLocator.Current.GetInstance<IClientState>().CountryCode == null)
            {
                try
                {
                    var response = await ServiceLocator.Current.GetInstance<IRestClient>()
                        .ExecuteAsync<Dictionary<string, object>>(
                            requestVerb: HttpVerb.GET,
                            parameters: new Dictionary<string, object>()
                            {
                                {"access_key", "21e1efdf2aa1f46046e31cb3960183bd"},
                                {"format", "1"}
                            },
                            skipHostCheck : true,
                            apiRoutePrefix: "http://api.ipstack.com/",
                            action: $"check",
                            paramMode: HttpParamMode.QUERYSTRING);

                    if (response.IsOK() && response.GetData() != null)
                    {
                        if (response.GetData().ContainsKey("location") && response.Data["location"] != null)
                        {
                            var locationData = ((JObject)(response.Data["location"])).ToObject<Dictionary<string, object>>();

                            if (locationData.ContainsKey("calling_code") && locationData["calling_code"] != null)
                            {
                                var phoneCode = (string)(locationData["calling_code"]);

                                ServiceLocator.Current.GetInstance<IClientState>().CountryCode = phoneCode;
                                TraceHelpers.WriteToTrace("Retrieved Country Code online from IP address : " + phoneCode);

                                return phoneCode;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    // ignored
                }

                TraceHelpers.WriteToTrace("Retrieving Country Code online from IP address failed");
                return null;
            }
            else
            {
                TraceHelpers.WriteToTrace(
                    "Retrieved Country Code online from IP address : "
                    + ServiceLocator.Current.GetInstance<IClientState>().CountryCode);
                return ServiceLocator.Current.GetInstance<IClientState>().CountryCode;
            }
        }

        /// <summary>
        ///  Gets the Country code 'string' online (SG, US, etc...)
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCountryCodeStringOnline()
        {
            try
            {
                var response = await ServiceLocator.Current.GetInstance<IRestClient>()
                    .ExecuteAsync<Dictionary<string, object>>(
                        requestVerb: HttpVerb.GET,
                        parameters: new Dictionary<string, object>()
                        {
                            { "access_key", "21e1efdf2aa1f46046e31cb3960183bd" },
                            { "format", "1" }
                        },
                        skipHostCheck: true,
                        apiRoutePrefix: "http://api.ipstack.com/",
                        action: $"check",
                        paramMode: HttpParamMode.QUERYSTRING);

                if (response.IsOK() && response.GetData() != null)
                {
                    if (response.GetData().ContainsKey("country_code") && response.Data["country_code"] != null)
                    {
                        var country_code = (string)(response.Data["country_code"]);

                        TraceHelpers.WriteToTrace("Retrieved Country Code online from IP address : " + country_code);

                        return country_code;
                    }
                }
            }
            catch (Exception ex)
            {
                // ignored
            }

            TraceHelpers.WriteToTrace("Retrieving Country Code string online from IP address failed");
            return null;
        }

        //public static Dictionary<int, string> GetCountries()
        //{
        //    // see https://msdn.microsoft.com/en-us/library/ms912047(v=winembedded.10).aspx
        //    var countries = new Dictionary<int, string>();
        //    CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
        //    foreach (CultureInfo cul in cinfo)
        //    {
        //        RegionInfo ri = null;
        //        try
        //        {
        //            ri = new RegionInfo(cul.Name);
        //            string TwoLetterISORegionName = ri.TwoLetterISORegionName;
        //            string RegionName = cul.Name;
        //            int id = cul.LCID;
        //            countries.Add(id, RegionName);
        //        }
        //        catch
        //        {
        //            continue;
        //        }
        //    }

        //    return countries;
        //}

        public static string GetCountryName(string code)
        {
            var ri = new RegionInfo(code);
            return ri.DisplayName;
        }

        public static string GetCountryCode(string code)
        {
            var ri = new RegionInfo(code);
            return ri.TwoLetterISORegionName;
        }
    }
}
