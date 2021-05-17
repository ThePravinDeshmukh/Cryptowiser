using Xunit;
using Cryptowiser.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Cryptowiser.ExternalServices;
using System.Linq;
using FluentAssertions;
using Cryptowiser.Models;

namespace Cryptowiser.BusinessLogic.Tests
{
    public class CryptoLogicTests
    {
        readonly CryptoLogic _target;
        readonly Mock<ICryptoExternalRates> _mockCryptoExternalRates;

        public CryptoLogicTests()
        {
            _mockCryptoExternalRates = new Mock<ICryptoExternalRates>();

            _target = new CryptoLogic(_mockCryptoExternalRates.Object);
        }

        [Fact()]
        public void GetSymbols_Should_Return_Valid_Symbols_String_Array()
        {
            string externalJson =
                @"{
                ""data"": [
                {
                  ""symbol"": ""BTC"",
                  ""quote"": {
                    ""USD"": {
                      ""price"": 9558.55163723,
                      ""last_updated"": ""2019-08-30T18:51:28.000Z""
                    }
                  }
                },
                {
                  ""symbol"": ""ETH"",
                  ""quote"": {
                    ""USD"": {
                    ""last_updated"": ""2019-08-30T18:51:21.000Z""
                    }
                  }
                },
                {
                  ""symbol"": ""XRP"",
                  ""quote"": {
                    ""USD"": {
                        ""price"": 0.254448519152,
                        ""last_updated"": ""2019-08-30T18:51:03.000Z""
                    }
                }
             }]
            }";

            _mockCryptoExternalRates.Setup(x => x.GetExternalSymbols(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(externalJson);

            var symbols = _target.GetSymbols(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            symbols.Should().NotBeNullOrEmpty();
            symbols.Count().Should().Be(3);
            symbols.Should().Contain(new string[] { "BTC", "ETH", "XRP" });
        }


        [Fact()]
        public void GetSymbols_Should_Throw_Exception_When_Bad_Response_From_API()
        {
            _mockCryptoExternalRates.Setup(x => x.GetExternalSymbols(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(string.Empty);

            Assert.Throws<BadResponseException>(() => _target.GetSymbols(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mockCryptoExternalRates.Verify(mock => mock.GetExternalSymbols(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Fact()]
        public void GetQuotes_Should_Throw_Exception_When_Bad_Response_From_API()
        {
            _mockCryptoExternalRates.Setup(x => x.GetExternalQuote(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(string.Empty);

            Assert.Throws<BadResponseException>(() => _target.GetQuotes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _mockCryptoExternalRates.Verify(mock => mock.GetExternalQuote(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
        [Fact()]
        public void GetQuotes_Should_Return_Valid_SymbolRates__Array()
        {
            string symboRate =
                @"
                    {
                      ""data"": {
                        ""{0}"": {
                          ""symbol"": ""{0}"",
                          ""quote"": {
                            ""{1}"": {
                              ""price"": {2}, 
                              ""last_updated"": ""{3}""
                            }
                          }
                        }
                    }
                }
                ";

            object[] symbolRateUSD = { "BTC", "USD", 3770.0096702907013, "2021-05-15T16:30:02.000Z" };
            object[] symbolRateAUD = { "BTC", "AUD", 1.2225559990000, "2021-05-15T16:30:02.000Z" };

            _mockCryptoExternalRates
                .Setup(x => x.GetExternalQuote(It.IsAny<string>(), It.IsAny<string>(), symbolRateUSD[0].ToString(), symbolRateUSD[1].ToString()))
                .Returns(symboRate
                    .Replace("{0}", symbolRateUSD[0].ToString())
                    .Replace("{1}", symbolRateUSD[1].ToString())
                    .Replace("{2}", symbolRateUSD[2].ToString())
                    .Replace("{3}", symbolRateUSD[3].ToString())
                );

            _mockCryptoExternalRates
                .Setup(x => x.GetExternalQuote(It.IsAny<string>(), It.IsAny<string>(), symbolRateAUD[0].ToString(), symbolRateAUD[1].ToString()))
                .Returns(symboRate
                    .Replace("{0}", symbolRateAUD[0].ToString())
                    .Replace("{1}", symbolRateAUD[1].ToString())
                    .Replace("{2}", symbolRateAUD[2].ToString())
                    .Replace("{3}", symbolRateAUD[3].ToString())
                );

            var symbolRates = _target.GetQuotes(It.IsAny<string>(), It.IsAny<string>(), "BTC", new string[]{ symbolRateUSD[1].ToString(), symbolRateAUD[1].ToString() });

            symbolRates.Should().NotBeNullOrEmpty();
            symbolRates.Count().Should().Be(2);
        }
    }
}