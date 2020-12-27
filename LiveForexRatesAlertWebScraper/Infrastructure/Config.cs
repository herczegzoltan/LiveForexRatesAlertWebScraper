using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LiveForexRatesAlertWebScraper.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace LiveForexRatesAlertWebScraper.Infrastructure
{
    public static class Config
    {
        private static IConfigurationRoot _configuration;
        
        public static Settings Get()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false);
            _configuration = builder.Build();


            var section = _configuration.GetSection(nameof(Settings));
            var settingsConfig = section.Get<Settings>();

            return settingsConfig;
        }
    }
}
