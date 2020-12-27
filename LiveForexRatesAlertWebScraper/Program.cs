using System;
using LiveForexRatesAlertWebScraper.Infrastructure;

namespace LiveForexRatesAlertWebScraper
{
    class Program
    {
        static string webSite = "https://www.investing.com/currencies/streaming-forex-rates-majors";

        static void Main(string[] args)
        {
            var results = WebScraper.GetCurrencyPairs(webSite);

            foreach (var item in results.Result)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
