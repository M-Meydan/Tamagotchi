namespace Tamagotchi.Models
{
    public class Food
    {
        public string Name { get; set; }
        public int Energy { get; set; }

        public Food(string name, int energy)
        {
            Name = name;
            Energy = energy;
        }
    }
}

