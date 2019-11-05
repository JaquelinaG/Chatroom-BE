using System.Net.Http;
using System.Threading.Tasks;

namespace Chatoroom.Bot
{
    public class StockService : IStockService
    {
        private const string StockUrl = "https://stooq.com/q/l/?s=[stockCode]&f=sd2t2ohlcv&h&e=csv";
        private IStockReader stockReader;

        public StockService(IStockReader stockReader)
        {
            this.stockReader = stockReader;
        }

        public async Task<Stock> GetStock(string stockCode)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(this.GetStockUrl(stockCode));

                var content = await response.Content.ReadAsStringAsync();

                return this.stockReader.ReadStockFromCSV(content);
            }
        }

        private string GetStockUrl(string stockCode)
        { 
            return StockUrl.Replace("[stockCode]", stockCode);
        }
    }
}
