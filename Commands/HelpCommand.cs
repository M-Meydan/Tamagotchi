using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tamagotchi.Commands
{
    public class HelpCommand : IRequest, ICommand { }

    public class HelpCommandHandler : IRequestHandler<HelpCommand>
    {
        IConsoleWriter _writer;

        public HelpCommandHandler(IConsoleWriter writer) { _writer = writer; }
        public Task<Unit> Handle(HelpCommand request, CancellationToken cancellationToken)
        {
            _writer.WriteLine($@"{Environment.NewLine}Use following commands to manage your pet:(Updates in every 24 sec.)

              feed : to feed your dragon (fish, chicken, lamb and cow) e.g. feed fish
              pet  : to make it happy
              play : to play with your pet
              help : to display commands
              status : to display pet's details {Environment.NewLine}");

            return Unit.Task;
        }
    }
}
