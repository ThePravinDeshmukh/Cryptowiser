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
using Cryptowiser.Models.Repository;
using Microsoft.IdentityModel.Tokens;

namespace Cryptowiser.Controllers.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IOptions<AppSettings>> _mockAppSettings;
        private readonly UserController _target;
        private readonly Mock<SecurityTokenHandler> _mockSecurityTokenHandler;
        public UserControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockAppSettings = new Mock<IOptions<AppSettings>>();
            _mockSecurityTokenHandler = new Mock<SecurityTokenHandler>();

            _mockAppSettings.SetupGet(x => x.Value).Returns(
                new AppSettings
                {
                    ApiKey = It.IsAny<string>(),
                    CryptoBaseUrl = It.IsAny<string>(),
                    Secret = "Secret",
                });

            _target = new UserController(_mockUserRepository.Object, _mockMapper.Object, _mockAppSettings.Object, _mockSecurityTokenHandler.Object);
        }

        [Fact]
        public void Authenticate_ReturnsActionResult_WithAUserModel()
        {
            User user = new User { Username = "x", Id = 1 };
            // Arrange
            _mockUserRepository
                .Setup(repo => repo.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            _mockSecurityTokenHandler
                .Setup(x => x.CreateToken(It.IsAny<SecurityTokenDescriptor>()))
                .Returns(It.IsAny<SecurityToken>());

            _mockSecurityTokenHandler
                .Setup(x => x.WriteToken(It.IsAny<SecurityToken>()))
                .Returns(It.IsAny<string>());

            var authenticateModel = new AuthenticateModel
            {
                Username = "x",
                Password = "y"
            };

            // Act
            var result = _target.Authenticate(authenticateModel);

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Authenticate_ReturnsBadRequest_WhenBadResponseExceptionThrown()
        {
            // Arrange
            _mockUserRepository
                .Setup(repo => repo.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(It.IsAny<User>());

            var authenticateModel = new AuthenticateModel
            {
                Username = "x",
                Password = "y"
            };

            // Act
            var result = _target.Authenticate(authenticateModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



    }
}