using AngleSharp.Dom;
using Services.Exceptions;
using Services.Helpers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToyStoreParserLibrary.Helpers
{
    internal class ToyWebsiteHttpHelper
    {
        public static async Task<IDocument> LoadDocument(RegionEnum region, string url)
        {
            try
            {
                var httpHelper = new ToyWebsiteHttpHelper();
                switch (region)
                {
                    case RegionEnum.Undefined:
                        return await GetDefaultDom(url);
                    case RegionEnum.Moscow:
                        return await GetDomForMoscow(url);
                    case RegionEnum.RostovOnDon:
                        return await GetDomForRostovOnDon(url);
                    case RegionEnum.SaintPetersburg:
                        return await GetDomForSaintPetersburg(url);
                    default:
                        throw new UnsupportedRegionException();
                }
            }
            catch (UnsupportedRegionException)
            {
                throw new UnsupportedRegionException();
            }
            catch (Exception)
            {
                throw new DownloadPageException();
            }
        }

        private static async Task<IDocument> GetDefaultDom(string url)
        {
            var httpHelper = new HttpHelper();
            return await httpHelper.LoadDomOrDefaultAsync(url);
        }

        private static async Task<IDocument> GetDomForSaintPetersburg(string url)
        {
            var cookie = CreateCookie("BITRIX_SM_city", "78000000000");

            var cookies = new CookieContainer();
            cookies.Add(cookie);

            var httpHelper = new HttpHelperWithCookies(cookies);

            return await httpHelper.LoadDomOrDefaultAsync(url);
        }

        private static async Task<IDocument> GetDomForMoscow(string url)
        {
            var cookie = CreateCookie("BITRIX_SM_city", "77000000000");

            var cookies = new CookieContainer();
            cookies.Add(cookie);

            var httpHelper = new HttpHelperWithCookies(cookies);

            return await httpHelper.LoadDomOrDefaultAsync(url);
        }

        private static async Task<IDocument> GetDomForRostovOnDon(string url)
        {
            var cookie = CreateCookie("BITRIX_SM_city", "61000001000");

            var cookies = new CookieContainer();
            cookies.Add(cookie);

            var httpHelper = new HttpHelperWithCookies(cookies);

            return await httpHelper.LoadDomOrDefaultAsync(url);
        }

        private static Cookie CreateCookie(string name, string value)
        {
            Cookie cookie = new Cookie(name, value);
            cookie.Discard = true;
            cookie.Domain = ".www.toy.ru";
            cookie.Path = "/";

            return cookie;
        }
    }
}
