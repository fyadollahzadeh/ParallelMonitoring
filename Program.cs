using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace MonitoringAssignment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
              IConfiguration  configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"appsettings.json", true, true)
              .Build();
              
            List<string> urls = new();
            configuration.GetSection("urls").Bind(urls);
            Monitor monitor = new();
            await monitor.MonitorUrls(urls);
        }

    }

}
