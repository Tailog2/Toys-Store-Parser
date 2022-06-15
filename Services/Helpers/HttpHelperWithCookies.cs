using System;
using System.Collections.Generic;
using System.Net;

namespace Services.Helpers
{
    public class HttpHelperWithCookies : HttpHelper
    {
        private CookieContainer _cookies;

        public HttpHelperWithCookies(CookieContainer cookies)
        {
            _cookies = cookies;
        }

        protected override HttpClient CreateClient(int timeout)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = _cookies;
            handler.UseCookies = true;

            HttpClient httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            return httpClient;
        }
    }
}
