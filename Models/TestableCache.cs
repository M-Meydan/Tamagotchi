using Microsoft.Extensions.Caching.Memory;

namespace Tamagotchi.Models
{
    public interface ITestableCache
    {
        Pet GetPet();
        void RemovePet();
        void SetPet(Pet pet);
    }

    public class TestableCache : ITestableCache
    {
        private IMemoryCache _memoryCache;

        public TestableCache() { }
        public TestableCache(IMemoryCache memoryCache) { _memoryCache = memoryCache; }
        public void SetPet(Pet pet) { _memoryCache.Set("pet", pet); }

        public Pet GetPet() { return (_memoryCache.TryGetValue("pet", out Pet pet)) ? pet : null; }

        public void RemovePet() { _memoryCache.Remove("pet"); }
    }
}
