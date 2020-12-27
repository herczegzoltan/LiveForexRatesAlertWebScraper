using System;
using System.IO;
using System.Timers;
using AngleSharp;
using AngleSharp.Css;
using LiveForexRatesAlertWebScraper.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LiveForexRatesAlertWebScraper
{
    class Program
    {
        static readonly string webSite = "https://www.investing.com/currencies/streaming-forex-rates-majors";
        private static Timer _timer;

        static void Main(string[] args)
        {
            SetTimer();

            EmailSender emailSender = new EmailSender();
            emailSender.test();
            
            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            _timer.Stop();
            _timer.Dispose();
        }

        static void PrintCurrenciesToConsole(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            var results = WebScraper.GetCurrencyPairs(webSite);

            foreach (var item in results.Result)
            {
                Console.WriteLine(item.Name + " " + item.BidPrice + " " + item.AskPrice);
            }
        }
        
        private static void SetTimer()
        {
            _timer = new System.Timers.Timer(5000);
            _timer.Elapsed += PrintCurrenciesToConsole;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
    }
}
