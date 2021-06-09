using MediatR;

namespace Tamagotchi.Events
{
    /// <summary>
    /// Represents the time passing for periodic events
    /// </summary>
    public class TimePassedEvent : INotification { }
}
