using Tamagotchi.Commands;
using System;
using System.Collections.Generic;
using Tamagotchi.Models;

namespace Tamagotchi
{
    public interface ICommandFactory{ ICommand GetCommand(string commandString); }

    /// <summary>
    /// Creates commands from instructions
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        IConsoleWriter _consoleWriter;
        readonly IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>
        {
            { "feed fish", new FeedCommand("fish") },
            { "feed chicken", new FeedCommand("chicken") },
            { "feed lamb", new FeedCommand("lamb") },
            { "feed cow", new FeedCommand("cow") },
            { "pet",  new PetCommand() },
            { "play",  new PlayCommand() },
            { "help", new HelpCommand() },
            { "status", new StatusCommand() }
        };

        public CommandFactory(IConsoleWriter consoleWriter) { _consoleWriter = consoleWriter; }

       

        public ICommand GetCommand(string commandString)
        {
            if (_commands.TryGetValue(commandString.ToLower(), out ICommand command)) return command;

            _consoleWriter.WriteLine($"Invalid command: '{commandString}' {Environment.NewLine}");
            return null;
        }
    }
}
