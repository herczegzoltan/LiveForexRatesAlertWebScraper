using System;
using System.Linq;
using System.Timers;
using LiveForexRatesAlertWebScraper.Infrastructure;

namespace LiveForexRatesAlertWebScraper
{
    class Program
    {
        static readonly string webSite = "https://www.investing.com/currencies/streaming-forex-rates-majors";
        private static Timer _timer;

        static void Main(string[] args)
        {
            bool isValid = ValidationForCheckCurrencyAlerts();

            if (isValid)
            {
                SetTimer();
            }

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            _timer.Stop();
            _timer.Dispose();
        }

        static void CheckAlertCriterion(object sender, ElapsedEventArgs e)
        {
            var alerts = Config.Get().alerts;
            
            var webScrapeCurrencyPairs = WebScraper.GetCurrencyPairs(webSite).Result.AsEnumerable();

            var alertCurrencyPairs  = 
                from currency in webScrapeCurrencyPairs
                from alert in alerts
                where (currency.Name == alert.name && 
                       ((currency.BidPrice < alert.price && alert.direction == "-") ||
                      (currency.BidPrice > alert.price && alert.direction == "+")))
                select currency;

            if (!alertCurrencyPairs.IsNullOrEmpty())
            {
                string message = "";

                foreach (var item in alertCurrencyPairs)
                {
                    message = item.Name + " hit alert value at " + item.BidPrice  + "\n" + message;
                }
                EmailSender emailSender = new EmailSender(message);
                emailSender.Send();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Email was sent at {0:HH: mm: ss.fff}", DateTime.Now + "\n" + message + "\n");

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
        static bool ValidationForCheckCurrencyAlerts()
        {
            var results = WebScraper.GetCurrencyPairs(webSite);

            var alerts = Config.Get().alerts;

            bool isValid = false;
            
            foreach (var item in alerts)
            {
                isValid = results.Result.Exists(m => m.Name == item.name);

                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Invalid price alert in appsettings.json!");
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Valid price alerts were found in appsettings.json!");

            Console.ForegroundColor = ConsoleColor.White;

            return isValid;
        }

        static void SetTimer()
        {
            _timer = new System.Timers.Timer(10000);
            _timer.Elapsed += CheckAlertCriterion;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
    }
}
