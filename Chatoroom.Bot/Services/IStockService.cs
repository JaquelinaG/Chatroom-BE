using System.Threading.Tasks;

namespace Chatoroom.Bot
{
    public interface IStockService
    {
        Task<Stock> GetStock(string stockCode);
    }
}
