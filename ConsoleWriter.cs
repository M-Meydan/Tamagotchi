using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi
{
    public interface IConsoleWriter
    {
        void WriteLine(string message, bool clearFirst = false);
    }

    /// <summary>
    /// Used to enable unit testing
    /// </summary>
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string message, bool clearFirst = false)
        {
            if (clearFirst) Console.Clear();
            Console.WriteLine(message);
        }
    }
}
