namespace Chatoroom.Bot
{
    public interface IStockReader
    {
        Stock ReadStockFromCSV(string csvContent);
    }
}
