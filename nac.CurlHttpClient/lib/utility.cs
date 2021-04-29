using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace nac.CurlHttpClient.lib
{
    public static class utility
    {
        
        /**
         <summary>
            If there are no parameters either on relativeUrl and queryParameters is null it will return a url without parameters on it
         </summary>
         */
        public static string formBaseRelativeUrlWithQueryParameters(string relativeUrl, List<KeyValuePair<string,string>> queryParameters ){
            // note this: https://stackoverflow.com/questions/3865975/namevaluecollection-to-url-query

            string baseQuery = "";
            string baseRelativeUrl = "";

            if (relativeUrl.Contains("?"))
            {
                var pieces = relativeUrl.Split('?');
                if (pieces.Length != 2)
                {
                    throw new Exception($"Relative URL: [{relativeUrl}] is malformed, maybe it contains more than one '?' character?  Something is wrong with it...");
                }

                baseQuery = pieces[1];
                baseRelativeUrl = pieces[0];
            }
            else
            {
                baseRelativeUrl = relativeUrl;
            }

            // returns an implementation of NameValueCollection
            // which in fact is HttpValueCollection
            var values = System.Web.HttpUtility.ParseQueryString(baseQuery);
            if( queryParameters != null ){
                foreach (var pair in queryParameters)
                {
                    values.Add(pair.Key, pair.Value);
                }
            }

            if( values.Count > 0){
                return baseRelativeUrl + '?' + values.ToString();
            }
            else {
                return baseRelativeUrl;
            }
            
        }


        public static T ProcessHttpResponse<T>(nac.CurlHttpClient.LowLevel.model.CurlExecResult response, System.Net.HttpStatusCode[] successCodes =null )
        {
            if( successCodes == null || successCodes.Length < 1)
            {
                successCodes = new[] { HttpStatusCode.OK, HttpStatusCode.NoContent };
            }

            if (response.ResponseCode == HttpStatusCode.NoContent)
            {
                if (typeof(T) == typeof(string))
                {
                    return (T) Convert.ChangeType("", typeof(T)); // have to do this even though we know T is string
                }
                else
                {
                    return default(T);
                }
            }
            
            if (typeof(T) == typeof(byte[]) &&
                successCodes.Contains(response.ResponseCode)
                )
            {
                var data = response.ResponseStream.ToArray();
                return (T)(object)data;
            }
            else
            {
                // process it as string
                var responseStr = System.Text.Encoding.UTF8.GetString(response.ResponseStream.ToArray());
                if (successCodes.Contains(response.ResponseCode))
                {
                    if (typeof(T) == typeof(string))
                    {
                        // try and convert it
                        try
                        {
                            return JsonConvert.DeserializeObject<T>(responseStr);
                        }
                        catch (Exception ex)
                        {
                            // odd trick we have to do to get it to return string when we know it's string and T is string
                            return (T)Convert.ChangeType(responseStr, typeof(T));
                        }

                    }
                    else
                    {
                        T result = JsonConvert.DeserializeObject<T>(responseStr);
                        return result;
                    }


                }
                else
                {
                    throw new model.HttpException(response.ResponseCode, responseStr);
                }
            }


        }
        
        
    }
}