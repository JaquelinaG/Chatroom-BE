namespace Chatroom.Bot
{
    public interface IStockReader
    {
        Stock ReadStockFromCSV(string csvContent);
    }
}
