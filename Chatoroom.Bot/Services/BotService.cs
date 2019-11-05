using System.Linq;
using System.Threading.Tasks;

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
            var command = text.Split(new char[] { '=' }, 2 );

            var entryCommand = command.First();

            return entryCommand == validEntryCommand;
        }

        private string GetValidStockcode(string text)
        {
            var command = text.Split(new char[] { '=' }, 2);

            var stock_code = command.Last();

            if (stock_code == command.First())
            {
                return null;
            }

            return stock_code;
        }

        public async Task<string> ProcessCommand(string text)
        {
            var stock = await this.stockService.GetStock(this.GetValidStockcode(text));

            if (stock == null)
                return "Invalid stock!";

            return $"{stock.Ticker} quote is ${stock.Price} per share";
        }

        public string GetCommandPattern()
        {
            return $"{validEntryCommand}=stock_code";
        }
    }
}
