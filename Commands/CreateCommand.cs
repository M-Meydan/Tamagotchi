using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Models;

namespace Tamagotchi.Commands
{
    public class CreateCommand : IRequest, ICommand { }

    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        ITestableCache _cache; IConsoleWriter _writer;

        public CreateCommandHandler(ITestableCache cache , IConsoleWriter writer)
        { _cache = cache; _writer = writer; }

        public Task<Unit> Handle(CreateCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _writer.WriteLine("Name your pet dragon: ");

            var petName = Console.ReadLine();

            _cache.SetPet(new Pet(petName));

            return Unit.Task;
        }
    }
}
