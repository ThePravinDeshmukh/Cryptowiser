using Cryptowiser.BusinessLogic.Helpers;
using Cryptowiser.ExternalServices;
using Cryptowiser.Models;
using Cryptowiser.Models.Enums;
using Cryptowiser.Models.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptowiser.BusinessLogic
{
    public class CryptoLogic : ICryptoLogic
    {
        
        private string[] DefaultConvertTo =
            {
                CurrencyEnum.USD.ToString(),
                CurrencyEnum.EUR.ToString(),
                CurrencyEnum.BRL.ToString(),
                CurrencyEnum.GBP.ToString(),
                CurrencyEnum.AUD.ToString()
            };
        private const char separator = ',';

        private readonly ICryptoExternalRates _cryptoExternalRates;
        public CryptoLogic(ICryptoExternalRates cryptoExternalRates)
        {
            _cryptoExternalRates = cryptoExternalRates;
            
        }
        public IEnumerable<string> GetSymbols(string cryptobaseurl, string key, string sort)
        {
            var result = _cryptoExternalRates.GetSymbols<JObject>(cryptobaseurl, key, sort);

            IEnumerable<string> symbols =
                result[Constants.DATA]
                .ToObject<List<Symbol>>()
                .Select(x => x.Name)
                .ToList();

            return symbols;
        }
        public IEnumerable<SymbolRate> GetRates(string cryptobaseurl, string key, string symbol, string[] convertTo)
        {
            return GetRatesForCrypto(cryptobaseurl, key, symbol, convertTo);
        }
        public IEnumerable<SymbolRate> GetRates(string cryptobaseurl, string key, string symbol)
        {
            return GetRatesForCrypto(cryptobaseurl, key, symbol, DefaultConvertTo);
        }

        private IEnumerable<SymbolRate> GetRatesForCrypto(string cryptobaseurl, string key, string symbol, string[] convertTo)
        {
            var result = _cryptoExternalRates.GetQuote<JObject>(cryptobaseurl, key, symbol, string.Join(separator, convertTo));

            var symbolRates = 
                result[Constants.DATA][symbol][Constants.QUOTE]
                .Select(
                    symbol => 
                    new SymbolRate
                    { 
                        Name =((JProperty)symbol).Name,
                        PriceDetail = ((JProperty)symbol).Value.ToObject<PriceDetail>() 
                    })
                .ToList();



            return symbolRates;
        }
    }


    public class SymbolRate
    {
        public string Name { get; set; }
        public PriceDetail PriceDetail { get; set; }
    }
    public class PriceDetail
    {
        [JsonProperty("price")]
        public float Price { get; set; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }

}

