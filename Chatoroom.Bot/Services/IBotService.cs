using System.Threading.Tasks;

namespace Chatroom.Bot
{
    public interface IBotService
    {
        bool IsValidCommand(string text);

        string GetCommandPattern();

        Task<string> ProcessCommand(string text);
    }
}
