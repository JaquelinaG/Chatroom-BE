using System.Linq;
using Chatoroom.Bot;

namespace Chatroom.Bot
{
    public class BotService : IBotService
    {
        public const string validEntryCommand = "/stock";
        private readonly IStockService stockService;

        public BotService(IStockService stockService)
        {
            this.stockService = stockService;
        }

        public bool IsValidCommand(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            return this.IsValidEntryCommand(text) 
                && !string.IsNullOrEmpty(this.GetValidStockcode(text));
        }

        private bool IsValidEntryCommand(string text)
        {
            var command = text.Split(new char[] { '=' }, 2);

            var entryCommand = command.First();

            return entryCommand == validEntryCommand;
        }

        private string GetValidStockcode(string text)
        {
            var command = text.Split(new char[] { '=' }, 2);

            var stock_code = command.Last();

            return stock_code;
        }

        public string ProcessCommand(string text)
        {
            var stock = this.stockService.GetStock(this.GetValidStockcode(text));

            if (stock == null)
                return "Invalid stock!";

            return $"{stock.Result.Ticker} quote is ${stock.Result.Price} per share";
        }

        public string GetCommandPattern()
        {
            return $"{validEntryCommand}+stock_code";
        }
    }
}
