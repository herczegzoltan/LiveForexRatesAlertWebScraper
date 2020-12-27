using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.X509;

namespace LiveForexRatesAlertWebScraper.Infrastructure
{
    public static class Config
    {
        private static IConfigurationRoot configuration;
        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false);
            configuration = builder.Build();
        }

        public static IConfigurationRoot Get()
        {
            var appSettings = configuration;
            
            return appSettings;
        }
    }
}
