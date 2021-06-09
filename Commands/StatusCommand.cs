using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Models;

namespace Tamagotchi.Commands
{
    public class StatusCommand : IRequest, ICommand { public StatusCommand() { } }

    public class StatusCommandHandler : IRequestHandler<StatusCommand>
    {
        ITestableCache _cache; IConsoleWriter _writer;

        public StatusCommandHandler(ITestableCache cache, IConsoleWriter writer) { _cache = cache; _writer = writer; }
        public Task<Unit> Handle(StatusCommand notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var pet = _cache.GetPet();

            _writer.WriteLine($"{Environment.NewLine} {pet.ToString()} {Environment.NewLine}");

            return Unit.Task;
        }
    }
}
