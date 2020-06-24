using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Helper
{
    public static class HttpHelper
    {
        public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        private static HttpClient client = new HttpClient()
        {
            Timeout = new TimeSpan(0, 0, 0, 15),
        };

        public static async Task<HttpResponseMessage> GetRequestAsync(string url, string authToken = null, Dictionary<string, string> additionalParams = null)
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Confer Desktop Client/" + Version);
            
            if (additionalParams != null)
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                foreach (var param in additionalParams)
                {
                    query[param.Key] = param.Value;
                }
                url = url + "?" + query.ToString();
            }
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (authToken != null)
                requestMessage.Headers.Add("X-AuthKey", authToken);

            return await client.SendAsync(requestMessage);
        }

        public static async Task<HttpResponseMessage> PostRequestAsync(string url, object data, string authToken = null)
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Confer Desktop Client/" + Version);

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            if (authToken != null)
                requestMessage.Headers.Add("X-AuthKey", authToken);

            requestMessage.Headers.Add("version", Version);

            if (data != null)
            {
                if (data is List<KeyValuePair<string, string>>)
                    requestMessage.Content = new FormUrlEncodedContent(data as List<KeyValuePair<string, string>>);
                else if (data is string)
                    requestMessage.Content = new StringContent((string)data);
                else
                    throw new Exception("HttpHelper.PostRequestAsync: unsupported data type");
            }

            return await client.SendAsync(requestMessage);
        }

        public static async Task<HttpResponseMessage> PutRequestAsync(string url, object data, string authToken = null)
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Confer Desktop Client/" + Version);

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            if (authToken != null)
                requestMessage.Headers.Add("X-AuthKey", authToken);

            requestMessage.Headers.Add("version", Version);

            if (data != null)
            {
                if (data is List<KeyValuePair<string, string>>)
                    requestMessage.Content = new FormUrlEncodedContent(data as List<KeyValuePair<string, string>>);
                else if (data is string)
                    requestMessage.Content = new StringContent((string)data);
                else
                    throw new Exception("HttpHelper.PutRequestAsync: unsupported data type");
            }

            return await client.SendAsync(requestMessage);
        }

        public static async Task<HttpResponseMessage> DeleteRequestAsync(string url, object data, string authToken = null)
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Confer Desktop Client/" + Version);

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            if (authToken != null)
                requestMessage.Headers.Add("X-AuthKey", authToken);

            requestMessage.Headers.Add("version", Version);

            if (data != null)
            {
                if (data is List<KeyValuePair<string, string>>)
                    requestMessage.Content = new FormUrlEncodedContent(data as List<KeyValuePair<string, string>>);
                else if (data is string)
                    requestMessage.Content = new StringContent((string)data);
                else
                    throw new Exception("HttpHelper.DeleteRequestAsync: unsupported data type");
            }

            return await client.SendAsync(requestMessage);
        }

    }
}
