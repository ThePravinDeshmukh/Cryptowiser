using Cryptowiser.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;

namespace Cryptowiser.IntegrationTests
{
    class CryptowiserTestsStartup : Startup
    {
        public CryptowiserTestsStartup(IConfiguration env) : base(env)
        {
        }

        public static ILogger<ConsoleLoggerProvider> AppLogger = null;
        public static ILoggerFactory loggerFactory = null;
        //
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Trace)
            );
            loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            AppLogger = loggerFactory.CreateLogger<ConsoleLoggerProvider>();
            services.Configure<RouteOptions>(Configuration);
            base.ConfigureServices(services);
            services
                .AddDbContext<CryptowiserContext>(
                    options => options.UseInMemoryDatabase("Cryptowiser")
                );
        }
    }
}