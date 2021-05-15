using Cryptowiser.BusinessLogic;
using Cryptowiser.ExternalServices;
using Cryptowiser.Models.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptowiser.IoC
{
    public static class Dependencies
    {
        public static void Map(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICryptoExternalRates, CryptoExternalRates>();
            services.AddScoped<ICryptoLogic, CryptoLogic>();
            services.AddScoped<ICryptoExternalRates, CryptoExternalRates>();
        }
    }
}
