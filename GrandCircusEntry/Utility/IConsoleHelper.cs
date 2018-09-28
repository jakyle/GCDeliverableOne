namespace GrandCircusEntry.Utility
{
    // contract for the console helper
    internal interface IConsoleHelper
    {
        void CyanLine(string message);
        void BlueBorderMessage(char symbol, int count, string title);
        void Yellow(string message);
        void BlueBorder(char symbol, int count);
        void RedLine(string message);
        void GreenLine(string message);
    }
}