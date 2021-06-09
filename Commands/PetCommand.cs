using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Models;

namespace Tamagotchi.Commands
{
    public class PetCommand: IRequest, ICommand { public PetCommand(){} }

    public class PetCommandHandler : IRequestHandler<PetCommand>
    {
        ITestableCache _cache; IConsoleWriter _writer;

        public PetCommandHandler(ITestableCache cache, IConsoleWriter writer) { _cache = cache; _writer = writer;}

        public Task<Unit> Handle(PetCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var pet = _cache.GetPet();

            pet.HappinessLevel += 10;

            _writer.WriteLine($"{Environment.NewLine} {pet.Name}: is happy :) {pet.ToString()} {Environment.NewLine}");

            return Unit.Task;
        }
    }
}
