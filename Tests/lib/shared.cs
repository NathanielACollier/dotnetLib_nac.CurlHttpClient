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
                    __http = lib.httpFactory.create();
                }

                return __http;
            }
        }


        private static nac.CurlHttpClient.HttpClient __http_HttpBIn;

        public static nac.CurlHttpClient.HttpClient http_HttpBin
        {
            get
            {
                if (__http_HttpBIn == null)
                {
                    __http_HttpBIn = lib.httpFactory.create(options =>
                    {
                        options.baseAddress = "http://httpbin.org/";
                    });
                }

                return __http_HttpBIn;
            }
        }





    }
}