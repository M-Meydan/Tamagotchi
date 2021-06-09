using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Exceptions;
using Tamagotchi.Models;

namespace Tamagotchi.Events
{
    public class AgedEventHandler : BaseEventHandler, INotificationHandler<TimePassedEvent>
    {
        public AgedEventHandler(ITestableCache cache) : base(cache) { }

        public Task Handle(TimePassedEvent timePassedEvent, CancellationToken cancellationToken)
        {
            var pet = GetPet();
            
            pet.Age++;

            if (!pet.IsAlive)
            {
                pet.Age = 0;
                throw new GameEndException($" --- RIP {pet.ToString()} --- ");
            }

            return Task.CompletedTask;
        }
    }
}
