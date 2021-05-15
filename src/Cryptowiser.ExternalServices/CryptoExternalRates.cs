using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Cryptowiser.ExternalServices
{
    public interface ICryptoExternalRates
    {
        public T GetSymbols<T>(string cryptobase, string key, string sort);
        public T GetQuote<T>(string cryptobase, string key, string symbol, string convert);
    }
    public class CryptoExternalRates : ICryptoExternalRates
    {
        public const string API_KEY = "X-CMC_PRO_API_KEY";
        public const string SYMBOL = "symbol";
        public const string CONVERT = "convert";
        public const string SORT = "sort";
        public const string ACCEPTS = "Accepts";
        public const string APPLICATION_JSON = "application/json";
        public const string ENDPOINT_QUOTES = "cryptocurrency/quotes/latest";
        public const string ENDPOINT_LISTINGS = "cryptocurrency/listings/latest";

        public T GetSymbols<T>(string cryptobase, string key, string sort = SYMBOL)
        {
            var URL = new UriBuilder(cryptobase + ENDPOINT_LISTINGS);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString[SORT] = sort;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add(API_KEY, key);
            client.Headers.Add(ACCEPTS, APPLICATION_JSON);
            return JsonConvert.DeserializeObject<T>(client.DownloadString(URL.ToString()));
        }

        public T GetQuote<T>(string cryptobase, string key, string symbol, string convert)
        {
            var URL = new UriBuilder(cryptobase + ENDPOINT_QUOTES);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString[SYMBOL] = symbol;
            queryString[CONVERT] = convert;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add(API_KEY, key);
            client.Headers.Add(ACCEPTS, APPLICATION_JSON);
            return JsonConvert.DeserializeObject<T>(client.DownloadString(URL.ToString()));
        }
    }
    public class JSONResponse<T> where T: class
    {
        public string status { get; set; }
        public List<T> data { get; set; }
    }

    public class Quote
    {
        public decimal price { get; set; }
    }

}
