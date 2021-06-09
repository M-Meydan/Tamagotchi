using System;

namespace Tamagotchi.Models
{
    public enum LifeStage { Baby, Child,Teen, Adult }

    public class Pet
    {
        const int MaxAge = 50;
        const int MaxWeight = 50;

        LifeStage _currentLifeStage;

        public string Name { get; private set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public int HealthLevel { get; set; }
        public int HappinessLevel { get; set; }
        public DateTime LastFeedTime { get; set; }
        public LifeStage LifeStage
        {
            get
            {
                if (Age >= 0 && Age <= 2) _currentLifeStage = LifeStage.Baby;
                else if (Age >= 3 && Age <= 10) _currentLifeStage = LifeStage.Child;
                else _currentLifeStage = LifeStage.Adult;

                return _currentLifeStage;
            }
            set { _currentLifeStage = value; }
        }
        public int GetLastFeedSeconds => (DateTime.Now - LastFeedTime).Seconds;
        public bool IsAlive => !(Age >= MaxAge || Weight >= MaxWeight || Weight <= 0 || HappinessLevel <= 0 || HealthLevel <= 0);

        public Pet() { }

        public Pet(string name)
        {
            Name = name;
            Weight = 10;
            Age = 0;
            HealthLevel = 50;
            HappinessLevel = 50;
            LastFeedTime = DateTime.Now.AddSeconds(-5);
        }

        public override string ToString()
        {
            return $"{Name}: age:{Age} {LifeStage}  happiness:{HappinessLevel}  health:{HealthLevel}  weight:{Weight}";
        }
    }
}

