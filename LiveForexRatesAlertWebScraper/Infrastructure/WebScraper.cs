using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using LiveForexRatesAlertWebScraper.Models;

namespace LiveForexRatesAlertWebScraper.Infrastructure
{
    public static class WebScraper
    {
        public static async Task<List<CurrencyPair>> GetCurrencyPairs(string webSite)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(webSite);

            var instrumentTilesRows = document.GetElementById("cr1");

            var tbody = instrumentTilesRows.QuerySelectorAll("tr");

            var currencyPairsList = new List<CurrencyPair>();

            foreach (var row in tbody)
            {
                var currencyPair = new CurrencyPair();

                var temp = row.QuerySelectorAll("td");

                if (temp.Length != 0)
                {
                    currencyPair.Name = temp[1].TextContent;
                    currencyPair.BidPrice = float.Parse(temp[2].TextContent);
                    currencyPair.AskPrice = float.Parse(temp[3].TextContent);

                    currencyPairsList.Add(currencyPair);
                }
            }

            return currencyPairsList;
        }
        
    }
}
