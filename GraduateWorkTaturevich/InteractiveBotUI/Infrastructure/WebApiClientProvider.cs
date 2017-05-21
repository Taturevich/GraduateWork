using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AimlBotUI.Infrastructure
{
    public class WebApiClientProvider
    {
        private readonly HttpClient _client;

        // private readonly string BaseUrl = "http://localhost:53754/";

        private readonly string _baseUrl = "http://localhost:59626/";

        public WebApiClientProvider()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string BaseUrl => _baseUrl;

        public HttpClient Client => _client;  
    }
}
