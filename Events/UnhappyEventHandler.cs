using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Exceptions;
using Tamagotchi.Models;

namespace Tamagotchi.Events
{
    public class UnhappyEventHandler :BaseEventHandler, INotificationHandler<TimePassedEvent>
    {
        public UnhappyEventHandler(ITestableCache cache) :base(cache) { }
        public Task Handle(TimePassedEvent timePassedEvent, CancellationToken cancellationToken)
        {
            var pet = GetPet();

            pet.HappinessLevel -= 5;

            if (!pet.IsAlive)
            {
                pet.HappinessLevel = 0;
                throw new GameEndException($" --- RIP {pet.ToString()} --- ");
            }

            return Task.CompletedTask;
        }
    }
}
