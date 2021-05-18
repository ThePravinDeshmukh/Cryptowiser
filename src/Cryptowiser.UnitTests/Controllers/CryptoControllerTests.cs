using Xunit;
using Cryptowiser.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Cryptowiser.BusinessLogic;
using AutoMapper;
using Cryptowiser.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Cryptowiser.Models;

namespace Cryptowiser.Controllers.Tests
{
    public class CryptoControllerTests
    {
        private readonly Mock<ICryptoLogic> _mockCryptoLogic;
        private readonly Mock<IOptions<AppSettings>> _mockAppSettings;
        private readonly CryptoController _target;
        public CryptoControllerTests()
        {
            _mockCryptoLogic = new Mock<ICryptoLogic>();
            _mockAppSettings = new Mock<IOptions<AppSettings>>();

            _mockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings
            {
                ApiKey = It.IsAny<string>(),
                CryptoBaseUrl = It.IsAny<string>(),
                Secret = It.IsAny<string>(),
            });

            _target = new CryptoController(_mockCryptoLogic.Object, _mockAppSettings.Object);
        }

        [Fact]
        public void GetAll_ShouldThrowException_WhenSortOrderIsEmpty()
        {
            // Arrange
            var sortOrder = string.Empty;
            var symbols = new string[] { "BTC", "ETH" };
            _mockCryptoLogic
                .Setup(repo => repo.GetSymbols(It.IsAny<string>(), It.IsAny<string>(), sortOrder))
                .Returns(symbols);

            // Act
            var result = _target.GetAll(sortOrder);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetAll_ReturnsActionResult_WithAListOfSymbols()
        {
            // Arrange
            var sortOrder = "symbol";
            var symbols = new string[] { "BTC", "ETH" };
            _mockCryptoLogic
                .Setup(repo => repo.GetSymbols(It.IsAny<string>(), It.IsAny<string>(), sortOrder))
                .Returns(symbols);

            // Act
            var result = _target.GetAll(sortOrder);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(
                viewResult.Value);
            Assert.Equal(symbols.Count(), model.Count());
        }

        [Fact]
        public void GetAll_ReturnsBadRequest_WhenBadResponseExceptionThrown()
        {
            // Arrange & Act
            var sortOrder = "symbol";
            _mockCryptoLogic
                .Setup(repo => repo.GetSymbols(It.IsAny<string>(), It.IsAny<string>(), sortOrder))
                .Throws(new BadResponseException("ErrorCode", "ErrorMessage"));

            // Act
            var result =  _target.GetAll(It.IsAny<string>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



        [Fact]
        public void GetRates_ReturnsActionResult_WithAListOfSymbols()
        {
            // Arrange
            var symbol = "BTC";
            var convertTo = new string[] { "USD", "GBP" };
            var symbolRates = new List<SymbolRate> {
                new SymbolRate {Name = "USD", PriceDetail = new PriceDetail{ Price = 1, LastUpdated = DateTime.Now} },
                new SymbolRate {Name = "GBP", PriceDetail = new PriceDetail{ Price = 2, LastUpdated = DateTime.Now} },
            };

            _mockCryptoLogic
                .Setup(repo => repo.GetQuotes(It.IsAny<string>(), It.IsAny<string>(), symbol, convertTo))
                .Returns(symbolRates);

            // Act
            var result = _target.GetRates(symbol, convertTo);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SymbolRate>>(
                viewResult.Value);
            Assert.Equal(symbolRates.Count(), model.Count());
        }

        [Fact]
        public void GetRates_ReturnsBadRequest_WhenBadResponseExceptionThrown()
        {
            // Arrange
            var symbol = "BTC";
            var convertTo = new string[] { "USD", "GBP" };
            var symbolRates = new List<SymbolRate> {
                new SymbolRate {Name = "USD", PriceDetail = new PriceDetail{ Price = 1, LastUpdated = DateTime.Now} },
                new SymbolRate {Name = "GBP", PriceDetail = new PriceDetail{ Price = 2, LastUpdated = DateTime.Now} },
            };

            _mockCryptoLogic
                .Setup(repo => repo.GetQuotes(It.IsAny<string>(), It.IsAny<string>(), symbol, convertTo))
                .Throws(new BadResponseException("ErrorCode", "ErrorMessage"));


            // Act
            var result = _target.GetRates(symbol, convertTo);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void GetRates_WithoutCurrencies_ReturnsActionResult_WithAListOfSymbols()
        {
            // Arrange
            var symbol = "BTC";
            var symbolRates = new List<SymbolRate> {
                new SymbolRate {Name = "USD", PriceDetail = new PriceDetail{ Price = 1, LastUpdated = DateTime.Now} },
                new SymbolRate {Name = "GBP", PriceDetail = new PriceDetail{ Price = 2, LastUpdated = DateTime.Now} },
            };

            _mockCryptoLogic
                .Setup(repo => repo.GetQuotes(It.IsAny<string>(), It.IsAny<string>(), symbol))
                .Returns(symbolRates);

            // Act
            var result = _target.GetRates(symbol);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SymbolRate>>(
                viewResult.Value);
            Assert.Equal(symbolRates.Count(), model.Count());
        }

        [Fact]
        public void GetRates_WithoutCurrencies_ReturnsBadRequest_WhenBadResponseExceptionThrown()
        {
            // Arrange
            var symbol = "BTC";
            var symbolRates = new List<SymbolRate> {
                new SymbolRate {Name = "USD", PriceDetail = new PriceDetail{ Price = 1, LastUpdated = DateTime.Now} },
                new SymbolRate {Name = "GBP", PriceDetail = new PriceDetail{ Price = 2, LastUpdated = DateTime.Now} },
            };

            _mockCryptoLogic
                .Setup(repo => repo.GetQuotes(It.IsAny<string>(), It.IsAny<string>(), symbol))
                .Throws(new BadResponseException("ErrorCode", "ErrorMessage"));


            // Act
            var result = _target.GetRates(symbol);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}