using Xunit;
using Cryptowiser.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Cryptowiser.Models;

namespace Cryptowiser.ExternalServices.Tests
{
    public class CryptoExternalRatesTests
    {
        readonly CryptoExternalRates _target;
        const string sanboxApiAddress = "https://sandbox-api.coinmarketcap.com/v1/";
        const string sanboxApiKey = "b54bcf4d-1bca-4e8e-9a24-22ff2c3d462c";

        public CryptoExternalRatesTests()
        {
            _target = new CryptoExternalRates();
        }


        [Fact()]
        public void GetExternalSymbols_ShouldReturnValidJObject()
        {
            var result = _target.GetExternalSymbols(sanboxApiAddress, sanboxApiKey);

            result.Should().NotBeEmpty();

            var data = Assert.IsAssignableFrom<JObject>(
                JsonConvert.DeserializeObject<JObject>(result));

            var symbols = Assert.IsAssignableFrom<List<Symbol>>(
                data["data"].ToObject<List<Symbol>>());
        }

        [Fact()]
        public void GetExternalRates_ShouldReturnValidJObject()
        {
            var result = _target.GetExternalQuote(sanboxApiAddress, sanboxApiKey, "BTC", "USD");

            result.Should().NotBeEmpty();

            var data = Assert.IsAssignableFrom<JObject>(
                JsonConvert.DeserializeObject<JObject>(result));

            var symbols = Assert.IsAssignableFrom<SymbolRate>(
                data["data"]["BTC"].ToObject<SymbolRate>());
        }
    }
}