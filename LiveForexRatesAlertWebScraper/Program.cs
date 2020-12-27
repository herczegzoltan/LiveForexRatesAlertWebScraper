using System;
using System.Timers;
using AngleSharp.Css;
using LiveForexRatesAlertWebScraper.Infrastructure;

namespace LiveForexRatesAlertWebScraper
{
    class Program
    {
        static string webSite = "https://www.investing.com/currencies/streaming-forex-rates-majors";
        private static Timer aTimer;

        static void Main(string[] args)
        {
            SetTimer();

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();
        }

        static void PrintCurrenciesToConsole(object sender, ElapsedEventArgs e)
        {
            var results = WebScraper.GetCurrencyPairs(webSite);

            foreach (var item in results.Result)
            {
                Console.WriteLine(item.Name);
            }
        }
        
        private static void SetTimer()
        {
            aTimer = new System.Timers.Timer(5000);
            aTimer.Elapsed += PrintCurrenciesToConsole;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
    }
}
