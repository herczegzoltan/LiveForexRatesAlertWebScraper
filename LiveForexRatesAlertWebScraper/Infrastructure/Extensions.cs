using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveForexRatesAlertWebScraper.Infrastructure
{
    public static class HasAnyElement
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
