using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tamagotchi.Models;

namespace Tamagotchi.Commands
{
    public class FeedCommand : IRequest, ICommand
    {
        public string FoodName { get; private set; }
        public FeedCommand(string foodName) { FoodName = foodName; }
    }

    public class FeedCommandHandler : IRequestHandler<FeedCommand>
    {
        IFoodContainer  _foodContainer; ITestableCache _cache; IConsoleWriter _writer;

        public FeedCommandHandler(ITestableCache cache,IFoodContainer  foodContainer, IConsoleWriter writer)
        {
            _cache = cache;
            _foodContainer = foodContainer;
            _writer = writer;
        }

        public Task<Unit> Handle(FeedCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var food = _foodContainer.Get(request.FoodName);
            var pet = _cache.GetPet();
         
            if (pet.GetLastFeedSeconds <= 4)
            {
                _writer.WriteLine($"{Environment.NewLine} Not hungry. Just had food! {Environment.NewLine}");
                return Unit.Task; // cant anymore. not hungry yet!
            }
            pet.LastFeedTime = DateTime.Now;
            pet.HealthLevel += food.Energy;
            pet.Weight += (food.Energy / 5) + 2;

            _writer.WriteLine($"{Environment.NewLine} {food.Name} was yummy :) {pet.ToString()}");

            return Unit.Task;
        }
    }
}
