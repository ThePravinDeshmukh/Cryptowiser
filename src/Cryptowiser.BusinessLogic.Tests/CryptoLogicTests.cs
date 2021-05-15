using Xunit;
using Cryptowiser.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Cryptowiser.ExternalServices;

namespace Cryptowiser.BusinessLogic.Tests
{
    public class CryptoLogicTests
    {
        CryptoLogic _target;
        Mock<ICryptoExternalRates> _mockCryptoExternalRates;

        public CryptoLogicTests()
        {
            _mockCryptoExternalRates = new Mock<ICryptoExternalRates>();

            _target = new CryptoLogic(_mockCryptoExternalRates.Object);
        }

        [Fact()]
        public void GetSymbolsTest()
        {
            string externalJson =
@"{
      ""data"": [
        {
          "symbol": "BTC",
          "quote": {
            "USD": {
              "price": 9558.55163723,
              "volume_24h": 13728947008.2722,
              "percent_change_1h": -0.127291,
              "percent_change_24h": 0.328918,
              "percent_change_7d": -8.00576,
              "market_cap": 171155540318.86005,
              "last_updated": "2019-08-30T18:51:28.000Z"
            }
          }
        },
        {
          "id": 1027,
          "name": "Ethereum",
          "symbol": "ETH",
          "slug": "ethereum",
          "num_market_pairs": 5629,
          "date_added": "2015-08-07T00:00:00.000Z",
          "tags": [
            "mineable"
          ],
          "max_supply": null,
          "circulating_supply": 107537936.374,
          "total_supply": 107537936.374,
          "platform": null,
          "cmc_rank": 2,
          "last_updated": "2019-08-30T18:51:21.000Z",
          "quote": {
            "USD": {
              "price": 168.688633539,
              "volume_24h": 5774323846.44399,
              "percent_change_1h": -0.0330049,
              "percent_change_24h": -0.510765,
              "percent_change_7d": -13.1883,
              "market_cap": 18140427540.533985,
              "last_updated": "2019-08-30T18:51:21.000Z"
            }
          }
        },
        {
        "id": 52,
          "name": "XRP",
          "symbol": "XRP",
          "slug": "ripple",
          "num_market_pairs": 449,
          "date_added": "2013-08-04T00:00:00.000Z",
          "tags": [],
          "max_supply": 100000000000,
          "circulating_supply": 42932866967,
          "total_supply": 99991366793,
          "platform": null,
          "cmc_rank": 3,
          "last_updated": "2019-08-30T18:51:03.000Z",
          "quote": {
            "USD": {
                "price": 0.254448519152,
              "volume_24h": 926785215.623047,
              "percent_change_1h": -0.187121,
              "percent_change_24h": -1.85857,
              "percent_change_7d": -7.81634,
              "market_cap": 10924204422.702969,
              "last_updated": "2019-08-30T18:51:03.000Z"
            }
        }
    }]
}";

            _mockCryptoExternalRates.Setup(x => x.GetSymbols(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(externalJson);

            _target.GetSymbols(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
            Assert.True(false, "This test needs an implementation");
        }
    }
}