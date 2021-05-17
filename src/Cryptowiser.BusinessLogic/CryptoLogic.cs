using Cryptowiser.BusinessLogic.Helpers;
using Cryptowiser.ExternalServices;
using Cryptowiser.Models;
using Cryptowiser.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptowiser.BusinessLogic
{
    public class CryptoLogic : ICryptoLogic
    {

        private readonly string[] DefaultConvertTo =
            {
                CurrencyEnum.USD.ToString(),
                CurrencyEnum.EUR.ToString(),
                CurrencyEnum.BRL.ToString(),
                CurrencyEnum.GBP.ToString(),
                CurrencyEnum.AUD.ToString()
            };

        private readonly ICryptoExternalRates _cryptoExternalRates;
        public CryptoLogic(ICryptoExternalRates cryptoExternalRates)
        {
            _cryptoExternalRates = cryptoExternalRates;

        }
        public IEnumerable<string> GetSymbols(string cryptobaseurl, string key, string sort)
        {
            try
            {
                JObject externalRatesJObject = JsonConvert.DeserializeObject<JObject>(_cryptoExternalRates.GetExternalSymbols(cryptobaseurl, key, sort));

                IEnumerable<string> symbols =
                    externalRatesJObject[Constants.DATA]
                    .ToObject<List<Symbol>>()
                    .Select(x => x.Name)
                    .ToList();

                return symbols;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw new BadResponseException("BR001", ex.Message);
            }
        }
        public IEnumerable<SymbolRate> GetQuotes(string cryptobaseurl, string key, string symbol, string[] convertTo)
        {
            return GetRatesForCrypto(cryptobaseurl, key, symbol, convertTo);
        }
        public IEnumerable<SymbolRate> GetQuotes(string cryptobaseurl, string key, string symbol)
        {
            return GetRatesForCrypto(cryptobaseurl, key, symbol, DefaultConvertTo);
        }

        private IEnumerable<SymbolRate> GetRatesForCrypto(string cryptobaseurl, string key, string symbol, string[] convertTo)
        {
            List<SymbolRate> symbolRates = new();

            try
            {
                foreach (string currency in convertTo)
                {
                    JObject externalRatesJObject = JsonConvert.DeserializeObject<JObject>(_cryptoExternalRates.GetExternalQuote(cryptobaseurl, key, symbol, currency));


                    symbolRates.AddRange(
                        externalRatesJObject[Constants.DATA][symbol][Constants.QUOTE]
                        .Select(
                            symbol =>
                            new SymbolRate
                            {
                                Name = ((JProperty)symbol).Name,
                                PriceDetail = ((JProperty)symbol).Value.ToObject<PriceDetail>()
                            })
                        .ToList());
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw new BadResponseException("BR002", ex.Message);
            }

            return symbolRates;
        }
    }
}

