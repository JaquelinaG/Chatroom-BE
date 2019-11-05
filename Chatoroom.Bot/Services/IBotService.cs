namespace Chatoroom.Bot
{
    public interface IBotService
    {
        bool IsValidCommand(string text);

        string GetCommandPattern();

        string ProcessCommand(string text);
    }
}
