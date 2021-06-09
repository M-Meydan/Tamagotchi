using System.Collections.Generic;
using System.Linq;
using Tamagotchi.Models;

namespace Tamagotchi
{
    public interface IFoodContainer { Food Get(string name); }

    public class FoodContainer : IFoodContainer
    {
        List<Food> Foods = new List<Food>();

        public FoodContainer(params Food[] food)
        {
            Foods.AddRange(food.ToList());
        }

        public Food Get(string name)
        {
            return Foods.FirstOrDefault(x => x.Name.Equals(name,System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}

