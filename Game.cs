using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Commands;
using Tamagotchi.Models;


namespace Tamagotchi
{
    public interface IGame{ Task Start(); }

    public class Game : IGame
    {
        IFoodContainer _foodContainer; ICommandFactory _commandFactory; IJobScheduler _jobScheduler; IMemoryCache _memoryCache; IMediator _mediator; IConsoleWriter _consoleWriter;

        public Game(IMediator mediatr, IMemoryCache memoryCache, ICommandFactory commandFactory, IFoodContainer foodContainer,
            IConsoleWriter consoleWriter, IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
            _memoryCache = memoryCache;
            _mediator = mediatr;
            _foodContainer = foodContainer;
            _commandFactory = commandFactory;
            _consoleWriter = consoleWriter;
        }

        public async Task Start()
        {
            _consoleWriter.WriteLine($"Tamagotchi world started! {Environment.NewLine}",true);
           
            await _mediator.Send(new CreateCommand());
            await _mediator.Send(new StatusCommand());
            await _mediator.Send(new HelpCommand());

            await _jobScheduler.CreateScheduler();
            await _jobScheduler.StartJob();

            ICommand cmd;
            while (GameStatus.Running)
            {
                var command = Console.ReadLine();

                if (GameStatus.Running && !string.IsNullOrWhiteSpace(command)
                    && (cmd = _commandFactory.GetCommand(command)) != null)
                {
                    await _mediator.Send(cmd);
                }
            }
        }
    }
}
