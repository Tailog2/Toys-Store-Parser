using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class HttpHelper
    {
        public virtual async Task<IDocument?> LoadDomOrDefaultAsync(string url)
        {
            IDocument? document = null;
            try
            {
                var httpClient = CreateClient(20);
                var response = await SendRequst(url, httpClient);
                CheckStatusCode(response);
                document = await ConvertToDom(response);
            }
            catch (Exception)
            {
                return document;
            }

            return document;
        }

        protected virtual HttpClient CreateClient(int timeout)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            return httpClient;
        }

        protected virtual async Task<HttpResponseMessage> SendRequst(string url, HttpClient httpClient)
        {
            return await httpClient.GetAsync(url);
        }

        protected virtual void CheckStatusCode(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new DownloadPageException("Status Code: " + response.IsSuccessStatusCode);
        }

        protected virtual async Task<IDocument> ConvertToDom(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStreamAsync();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using StreamReader reader = new StreamReader(content, Encoding.GetEncoding(response.Content.Headers.ContentType.CharSet));          
            var html = reader.ReadToEnd();

            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsNotConsumingCharacterReferences = false,        
            });

            return await parser.ParseDocumentAsync(html);
        }
    }
}
