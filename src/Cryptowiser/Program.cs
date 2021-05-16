using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptowiser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>()
            .UseUrls("http://localhost:5000");
    }).Build().Run();

