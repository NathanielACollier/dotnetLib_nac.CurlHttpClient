using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace nac.CurlHttpClient
{
    public class HttpClient
    {
        private model.HttpClientSetup options;
        private nac.CurlHttpClient.LowLevel.http http;
        
        public HttpClient(model.HttpClientSetup __options=null)
        {
            if (__options == null)
            {
                this.options = new nac.CurlHttpClient.model.HttpClientSetup();
            }
            else
            {
                this.options = __options;
            }
            
            this.http = new nac.CurlHttpClient.LowLevel.http(this.options);
        }

        public async Task<T> postJSONAsync<T>(string relativeUrl="",
            Dictionary<string, string> queryParameters=null,
            object data = null,
            HttpStatusCode[] successCodes = null)
        {
            if (queryParameters == null)
            {
                queryParameters = new Dictionary<string, string>();
            }
            
            var curlResult = await Task.Run(() =>
            {
                var headers = new Dictionary<string, string>()
                {
                    {"Accept", "application/json"}
                };

                var url = lib.utility.formBaseRelativeUrlWithQueryParameters(relativeUrl, queryParameters.ToList());

                string jsonData = "";
                if (data != null)
                {
                    jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                }

                return http.post(url: url,
                    headers: headers,
                    requestBody: jsonData);
            });
            
            // deserialize the json result back to an object
            return lib.utility.ProcessHttpResponse<T>(response: curlResult,
                successCodes: successCodes);
        }


        public async Task<T> getJSONAsync<T>(string relativeUrl="",
            Dictionary<string, string> queryParameters=null,
            HttpStatusCode[] successCodes = null)
        {
            if (queryParameters == null)
            {
                queryParameters = new Dictionary<string, string>();
            }
            var curlResult = await Task.Run(() =>
            {
                var headers = new Dictionary<string, string>()
                {
                    {"Accept", "application/json"}
                };

                var url = lib.utility.formBaseRelativeUrlWithQueryParameters(relativeUrl, queryParameters.ToList());

                return http.get(url: url,
                    headers: headers);
            });
            
            // deserialize the json result back to an object
            return lib.utility.ProcessHttpResponse<T>(response: curlResult,
                successCodes: successCodes);
        }
        
        
        
    }
}