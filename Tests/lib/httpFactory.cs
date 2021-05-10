using System;

namespace Tests.lib
{
    public static class httpFactory
    {


        public static nac.CurlHttpClient.HttpClient create(Action<nac.CurlHttpClient.model.HttpClientSetup> onSetup=null)
        {
            var options = new nac.CurlHttpClient.model.HttpClientSetup()
            {
                onNewHttpResponse = (_curlResult) =>
                {
                    System.Diagnostics.Debug.WriteLine(_curlResult.ToString());
                }
            };

            onSetup?.Invoke(options);

            var http = new nac.CurlHttpClient.HttpClient(options);
            return http;
        }

        
        
    }
}