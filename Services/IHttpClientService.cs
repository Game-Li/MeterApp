using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterApp.Services
{
    interface IHttpClientService
    {
        Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string> headers, Dictionary<string, string> queryParams);

        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, Dictionary<string, string> headers);
    }
}
