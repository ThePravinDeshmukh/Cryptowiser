using Cryptowiser.ExternalServices;
using Cryptowiser.Models;
using Cryptowiser.Models.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Albellicart.BusinessLogic
{
    public class CryptoLogic : ICryptoLogic
    {
        const char separator = ','
        private readonly ICryptoExternalRates _cryptoExternalRates;
        public CryptoLogic(ICryptoExternalRates cryptoExternalRates)
        {
            _cryptoExternalRates = cryptoExternalRates;
        }
        public IEnumerable<string> GetSymbols(string cryptobaseurl, string key)
        {
            var result = _cryptoExternalRates.GetSymbols<JObject>(cryptobaseurl, key);

            IEnumerable<string> symbols = 
                result["data"]
                .ToObject<List<Symbol>>()
                .Select(x=>x.Name)
                .ToList();

            return symbols;
        }
        public IEnumerable<string> GetRates(string cryptobaseurl, string key, string symbol, string[] convert)
        {
            var result = _cryptoExternalRates.GetQuote<JObject>(cryptobaseurl, key, symbol, string.Join(separator, convert));

            IEnumerable<string> symbols =
                result["data"]
                .ToObject<List<Symbol>>()
                .Select(x => x.Name)
                .ToList();

            return symbols;
        }
    }
}
