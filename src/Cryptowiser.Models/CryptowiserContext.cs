using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;


namespace Cryptowiser.Models
{
    public class CryptowiserContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public CryptowiserContext(DbContextOptions<CryptowiserContext> options) : base(options)
        {
        }
        public CryptowiserContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Symbol> Symbols { get; set; }

    }
}
