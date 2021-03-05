using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        IWebHostBuilder builder = new WebHostBuilder();
    //        builder.ConfigureServices(s =>
    //        {
    //            s.AddSingleton(builder);
    //        });
    //        builder.UseKestrel()
    //               .UseContentRoot(Directory.GetCurrentDirectory())
    //               .UseStartup<Startup>()
    //               .UseUrls("http://localhost:9000");

    //        var host = builder.Build();
    //        host.Run();
    //    }
    //}
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("ocelot.json");
                    config.AddJsonFile("appsettings.json");
                });
    }
}
