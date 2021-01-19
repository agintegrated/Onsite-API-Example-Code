using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Onsite_API_Example_Code
{
    /// <summary>
    /// Here we do any one-time setup for the entire example project. git 
    /// Initialize the the HTTP client used to make calls to it.
    /// </summary>
    public class Api
    {
        /*
         * The url below is for the Data Exchange 2.0 Sandbox test environment. This is the environment that should be used for developing against the Data Exchange 2.0 API.
         * Our production url is at https://qc-pro.onsiteag.com/api and should only be used when deploying code to your own production environment. Data on our production 
         * environment is live data that should not be used for testing purposes.
        */
        private static string BaseUrl = "https://sandbox-dataexchange.onsiteag.com/api/";

        /// <summary>
        /// Creates an HttpClient that can be called on-demand.
        /// </summary>
        public static readonly Lazy<HttpClient> LazyClient = new Lazy<HttpClient>(() =>
        {
            var client = new HttpClient();
            return client;
        });

        public static HttpClient Client
        {
            get { return LazyClient.Value; }
        }

        /// <summary>
        ///  Creates an HTTP request that includes the headers and URL.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="endpoint"></param>
        private static HttpRequestMessage GetHttpRequest(Dictionary<string, string> headers, string endpoint)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(BaseUrl + endpoint)
            };
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            return request;
        }

        /// <summary>
        /// Methods that return an HTTP response for the specific request types (GET, POST, etc.).
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="headers"></param>
        /// 
        #region Http Method helpers

        public static async Task<HttpResponseMessage> Get(string endpoint, Dictionary<string, string> headers)
        {
            var request = GetHttpRequest(headers, endpoint);
            request.Method = HttpMethod.Get;
            var response = await Client.SendAsync(request);
            return response;
        }

        /// <summary>
        /// Same as <see cref="Get(string, Dictionary{string, string})\"/> with
        /// empty headers.
        /// </summary>
        public static async Task<HttpResponseMessage> Get(string endpoint)
        {
            return await Get(endpoint, new Dictionary<string, string>());
        }

        public static async Task<HttpResponseMessage> Get(string endpoint, Dictionary<string, string> headers, Dictionary<string, string> queryParams = null)
        {
            var request = GetHttpRequest(headers, endpoint);
            request.Method = HttpMethod.Get;

            if (queryParams != null && queryParams.Count > 0)
            {
                // Reformat this get request so we can append the params to the uri
                UriBuilder baseUri = new UriBuilder(request.RequestUri);
                foreach (var item in queryParams)
                {
                    string queryToAppend = $"{item.Key}={item.Value}";

                    if (baseUri.Query != null && baseUri.Query.Length > 1)
                        baseUri.Query = baseUri.Query.Substring(1) + "&" + queryToAppend;
                    else
                        baseUri.Query = queryToAppend;
                }

                request.RequestUri = baseUri.Uri;
            }

            var response = await Client.SendAsync(request);
            return response;
        }

        public static async Task<HttpResponseMessage> Post(string endpoint, Dictionary<string, string> headers, HttpContent content)
        {
            var request = GetHttpRequest(headers, endpoint);
            request.Content = content;
            request.Method = HttpMethod.Post;
            var response = await Client.SendAsync(request);
            return response;
        }

        /// <summary>
        /// Like <see cref="Post( string,Dictionary{string, string}, HttpContent)"/> except
        /// the Content of the request is an empty string.
        /// </summary>
        public static async Task<HttpResponseMessage> Post(string endpoint, Dictionary<string, string> headers)
        {
            return await Post(endpoint, headers, new StringContent(""));
        }

        public static async Task<HttpResponseMessage> Put(string endpoint, Dictionary<string, string> headers, HttpContent content)
        {
            var request = GetHttpRequest(headers, endpoint);
            request.Content = content;
            request.Method = HttpMethod.Put;
            var response = await Client.SendAsync(request);
            return response;
        }

        /// <summary>
        /// Like <see cref="Put( string,Dictionary{string, string}, HttpContent)"/> except
        /// the Content of the request is an empty string.
        /// </summary>
        public static async Task<HttpResponseMessage> Put(string endpoint, Dictionary<string, string> headers)
        {
            return await Put(endpoint, headers, new StringContent(""));
        }

        public static async Task<HttpResponseMessage> Delete(string endpoint, Dictionary<string, string> headers)
        {
            var request = GetHttpRequest(headers, endpoint);
            request.Method = HttpMethod.Delete;
            var response = await Client.SendAsync(request);
            return response;
        }

        public static async Task<HttpResponseMessage> Delete(string endpoint, Dictionary<string, string> headers, HttpContent content)
        {
            var request = GetHttpRequest(headers, endpoint);
            request.Content = content;
            request.Method = HttpMethod.Delete;
            var response = await Client.SendAsync(request);
            return response;
        }

        public static async Task<HttpResponseMessage> Delete(string endpoint, Dictionary<string, string> headers, Dictionary<string, string> queryParams = null)
        {
            var request = GetHttpRequest(headers, endpoint);
            request.Method = HttpMethod.Delete;

            if (queryParams != null && queryParams.Count > 0)
            {
                // Reformat this get request so we can append the params to the uri
                UriBuilder baseUri = new UriBuilder(request.RequestUri);
                foreach (var item in queryParams)
                {
                    string queryToAppend = $"{item.Key}={item.Value}";

                    if (baseUri.Query != null && baseUri.Query.Length > 1)
                        baseUri.Query = baseUri.Query.Substring(1) + "&" + queryToAppend;
                    else
                        baseUri.Query = queryToAppend;
                }

                request.RequestUri = baseUri.Uri;
            }

            var response = await Client.SendAsync(request);
            return response;
        }

        #endregion

        #region Serialization helpers
        public static T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Convenience method to read the content of the response and deserialize to
        /// provided type.
        /// <see cref="Deserialize{T}(string)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        public static async Task<T> DeserializeContent<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Deserialize<T>(content);
        }

        /// <summary>
        /// Wrapper around <see cref="JsonConvert.SerializeObject(object)"/> with custom settings.
        /// </summary>
        /// <param name="o"></param>
        public static string Serialize(object o)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(o, Formatting.None, settings);
        }
        #endregion

        #region Generic Request helpers
        // Helper methods that are generic to all requests (not just API requests.)

        /// <summary>
        /// Call the function on a delay until the function returns true. Useful for waiting on
        /// APIs that don't have callbacks.
        /// </summary>
        /// <param name="timeout">The maximum amount of time to wait. Technically it could be max+pollInterval.</param>
        /// <param name="timeoutMessage">A message for when the timeout is exceeded</param>
        /// <param name="until">Async function to call every pollInterval. Return true to end waiting.</param>
        /// <param name="pollIntervalInSeconds">Call the function every...</param>
        public static async Task PollUntil(TimeSpan timeout, string timeoutMessage, Func<StringBuilder, Task<bool>> until, int pollIntervalInSeconds = 4)
        {
            // Set a cap on how long this can run
            var startTime = DateTime.UtcNow;

            // Assign the pollInterval
            TimeSpan pollEvery = TimeSpan.FromSeconds(pollIntervalInSeconds);

            // Initialize the log
            StringBuilder log = new StringBuilder();

            while (true)
            {
                // This would normally be in the while statement above but
                // that seems to exit the function early.
                var keepWaiting = await until(log);
                if (keepWaiting)
                {
                    break;
                }

                // Wait whatever the desired wait is before looping
                await Task.Delay(pollEvery);
            }
        }
        #endregion
    }
}
