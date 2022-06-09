using Autofac.Extensions.DependencyInjection;
using dotenv.net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace GoSmartValue.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// host is responsible for application startup and lifetime management
        /// Ensures application’s services and the server are available and properly configured
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(new DotEnvOptions(true, new List<string>() { dotenv }, Encoding.UTF8, true));

            var port = Environment.GetEnvironmentVariable("PORT");
            //debugging statement in case the port didn't get passed correctly
            Console.WriteLine($"env PORT is {port ?? ("not found")}");

            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(config);
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        options.Listen(IPAddress.IPv6Any, Convert.ToInt32(port));
                    });
                });
        }
    }
}
