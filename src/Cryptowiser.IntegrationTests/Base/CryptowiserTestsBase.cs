using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cryptowiser.IntegrationTests
{
    public class CryptowiserTestsBase
    {
        protected const string CryptoApiUrlBase = "api";

        public TestServer CreateServer()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            var path = Assembly.GetAssembly(typeof(CryptowiserTestsBase))
               .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<CryptowiserTestsStartup>();

            return new TestServer(hostBuilder);
        }
    }
}