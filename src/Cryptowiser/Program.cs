using Cryptowiser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder
        .UseStartup<Startup>()
        .UseSerilog()
        .UseKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = null;
            options.AllowSynchronousIO = true;
        })
        .UseUrls("http://localhost:5000");
    })
    .Build()
    .Run();

