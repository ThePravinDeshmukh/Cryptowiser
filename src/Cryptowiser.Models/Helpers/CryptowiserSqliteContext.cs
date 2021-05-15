using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptowiser.Models.Helpers
{
    public class CryptowiserSqliteContext : CryptowiserContext
    {
        public CryptowiserSqliteContext(IConfiguration configuration) : base(configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString(Constants.CRYPTOWISER_CONTEXT));
        }
    }
}
