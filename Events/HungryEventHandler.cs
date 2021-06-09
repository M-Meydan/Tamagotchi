using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Exceptions;
using Tamagotchi.Models;

namespace Tamagotchi.Events
{
    public class HungryEventHandler : BaseEventHandler, INotificationHandler<TimePassedEvent>
    {
        public HungryEventHandler(ITestableCache cache) :base(cache) {}

        public Task Handle(TimePassedEvent timePassedEvent, CancellationToken cancellationToken)
        {
            var pet = GetPet();

            pet.HealthLevel -= GetHungerLevel(pet.LifeStage);
            pet.Weight -= 1;

            if (!pet.IsAlive)
            {
                pet.HealthLevel = 0;
                throw new GameEndException($" --- RIP {pet.ToString()} --- ");
            }

            return Task.CompletedTask;
        }

        int GetHungerLevel(LifeStage lifeStage)
        {
            if (lifeStage == LifeStage.Baby) return 5;
            else if (lifeStage == LifeStage.Child) return 7;
            else  return 10;
        }
    }
}
