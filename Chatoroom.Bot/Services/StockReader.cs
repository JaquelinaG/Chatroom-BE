using System.Collections.Generic;
using System.Linq;

namespace Chatoroom.Bot
{
    public class StockReader : IStockReader
    {
        public Stock ReadStockFromCSV(string csvContent)
        {
            var keys = new Dictionary<string, int>();

            var lines = csvContent.Split("\r\n");
            var cols = lines.First().Split(',');

            for (var i = 0; i < cols.Length; i++)
                keys.Add(cols[i], i);

            var values = lines[1].Split(',');

            var stock = new Stock
            {
                Ticker = values[keys["Symbol"]],
                Price = values[keys["Close"]]
            };

            return stock;
        }
    }
}
