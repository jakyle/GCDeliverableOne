using System;

namespace GrandCircusEntry.Utility
{
    // pretty straight forward, we change the color of the console for most of these methods then change them back
    // to the original white color after the message is printed.
    class ConsoleHelper : IConsoleHelper
    {
        public void BlueBorderMessage(char symbol, int count, string title)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{new string(symbol, count)}  {title}  {new string(symbol, count)}");
            Console.ForegroundColor = ConsoleColor.White;

        }

        public void CyanLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Yellow(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void BlueBorder(char symbol, int count)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{new string(symbol, count)}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void RedLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void GreenLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
