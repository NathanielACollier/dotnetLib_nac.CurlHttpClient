using System;
using nac.CurlHttpClient.model;

namespace Tests.lib
{
    public class shared
    {
        private static nac.CurlHttpClient.HttpClient __http;

        public static nac.CurlHttpClient.HttpClient http
        {
            get
            {
                if (__http == null)
                {
                    __http = new nac.CurlHttpClient.HttpClient(new HttpClientSetup()
                    {
                        onNewHttpResponse = (_curlResult) =>
                        {
                            System.Diagnostics.Debug.WriteLine(_curlResult.ToString());
                        }
                    });
                }

                return __http;
            }
        }
    }
}