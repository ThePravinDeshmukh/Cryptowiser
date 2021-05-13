using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Cryptowiser.ExternalServices
{

    public interface ICryptoExternalRates
    {
        public T GetSymbols<T>(string cryptobase, string key);
        public T GetQuote<T>(string cryptobase, string key, string symbol, string convert);
    }
    public class CryptoExternalRates : ICryptoExternalRates
    {
        public CryptoExternalRates()
        {

        }

        public T GetSymbols<T>(string cryptobase, string key)
        {
            var URL = new UriBuilder(cryptobase + "cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["sort"] = "symbol";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", key);
            client.Headers.Add("Accepts", "application/json");
            return JsonConvert.DeserializeObject<T>(client.DownloadString(URL.ToString()));
        }

        public T GetQuote<T>(string cryptobase, string key, string symbol, string convert)
        {
            var URL = new UriBuilder(cryptobase + "cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = symbol;
            queryString["convert"] = convert;

            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", key);
            client.Headers.Add("Accepts", "application/json");
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
