using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Exceptions;
using Tamagotchi.Models;

namespace Tamagotchi.Commands
{
    public class PlayCommand : IRequest, ICommand { public PlayCommand() { } }

    public class PlayCommandHandler : IRequestHandler<PlayCommand>
    {
        ITestableCache _cache; IConsoleWriter _writer;

        public PlayCommandHandler(ITestableCache cache, IConsoleWriter writer) { _cache = cache; _writer = writer; }

        public async Task<Unit> Handle(PlayCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var pet = _cache.GetPet();

            pet.HappinessLevel += 10;
            pet.Weight -= 1;

            _writer.WriteLine($"{Environment.NewLine} Feeling great! :) {pet.ToString()} {Environment.NewLine}");

            if (!pet.IsAlive)
            {
                pet.HappinessLevel = 0;
                throw new GameEndException($"{Environment.NewLine} --- RIP {pet.ToString()} --- {Environment.NewLine}");
            }

            return await Unit.Task;
        }
    }
}
