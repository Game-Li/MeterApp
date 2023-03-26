using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace MeterApp.Services.Impl
{ 
    public class HttpClientServiceImpl : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientServiceImpl(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                uri = AddQueryParams(uri, queryParams);
            }

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = await _httpClient.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = content;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = await _httpClient.SendAsync(request);
            return response;
        }

        private string AddQueryParams(string uri, Dictionary<string, string> queryParams)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            foreach (var queryParam in queryParams)
            {
                query[queryParam.Key] = queryParam.Value;
            }

            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }
    }
}
