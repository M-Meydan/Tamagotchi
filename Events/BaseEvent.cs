using MediatR;
using Tamagotchi.Exceptions;
using Tamagotchi.Models;

namespace Tamagotchi.Events
{
    public class BaseEventHandler
    {
        protected ITestableCache _cache;

        public BaseEventHandler(ITestableCache cache){ _cache = cache; }

        protected Pet GetPet()
        {
            Pet pet = _cache.GetPet();

            if (pet == null) throw new GameEndException("Pet is not found in cache!");

            return pet;
        }
    }
}
