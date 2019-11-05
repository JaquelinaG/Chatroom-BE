using System.Threading.Tasks;

namespace Chatroom.Bot
{
    public interface IStockService
    {
        Task<Stock> GetStock(string stockCode);
    }
}
