using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Exceptions
{
    public class GameEndException : Exception
    {
        public GameEndException(string message) : base(message){}
    }
}
