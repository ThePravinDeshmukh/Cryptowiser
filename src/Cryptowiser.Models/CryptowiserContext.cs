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

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to sql server database
        //    options.UseSqlServer(Configuration.GetConnectionString("CryptowiserContext"));
        //}
        public DbSet<User> Users { get; set; }
        public DbSet<Symbol> Symbols { get; set; }

        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite(@"Data Source=..\Cryptowiser.db");
    }
}
