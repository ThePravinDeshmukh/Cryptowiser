using Xunit;
using Cryptowiser.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;

namespace Cryptowiser.Models.Repository.Tests
{
    public class UserRepositoryTests
    {
        readonly UserRepository _target;
        readonly CryptowiserContext _cryptowiserContext;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CryptowiserContext>()
            .UseInMemoryDatabase(databaseName: "CryptowiserContext")
            .Options;

            _cryptowiserContext = new CryptowiserContext(options);
            _target = new UserRepository(_cryptowiserContext);
        }

        [Fact()]
        public void Authenticate_Should_Throw_Exception_When_Username_Or_Password_Null()
        {
            string username = "x", password = "y";

            var resultWhenPasswordNull = _target.Authenticate(username, null);
            resultWhenPasswordNull.Should().BeNull();

            var resultWhenUsernameNull = _target.Authenticate(null, password);
            resultWhenUsernameNull.Should().BeNull();
        }

        [Fact()]
        public void Authenticate_Should_Return_NULL_When_User_Null()
        {
            string username = It.IsAny<string>(), password = It.IsAny<string>();
            var result = _target.Authenticate(username, password);
            result.Should().BeNull();
        }

        [Fact()]
        public void Create_Should_Return_User_Oc_Successful_Login()
        {
            string username = "x2", password = "y2";
            User user = new User
            {
                FirstName = "a2",
                LastName = "b2",
                Username = "x2",
            };
            _target.Create(user, password);

            var result = _target.Authenticate(username, password);

            result.FirstName.Should().BeEquivalentTo(user.FirstName);
            result.LastName.Should().BeEquivalentTo(user.LastName);
            result.Username.Should().BeEquivalentTo(user.Username);
        }
        [Fact()]
        public void Create_Should_ThrowException_If_Password_Is_Null()
        {
            string password = null;
            User user = new User
            {
                FirstName = "a",
                LastName = "b",
                Username = "x",
            };
            
            Assert.Throws<ValidationException>(() => _target.Create(It.IsAny<User>(), password));

        }
        [Fact()]
        public void Create_Should_ThrowException_If_User_Exists()
        {
            string password = "y";
            User user = new User
            {
                FirstName = "a",
                LastName = "b",
                Username = "x",
            };

            _target.Create(user, password);

            Assert.Throws<ValidationException>(() => _target.Create(user, password));

        }

    }
}