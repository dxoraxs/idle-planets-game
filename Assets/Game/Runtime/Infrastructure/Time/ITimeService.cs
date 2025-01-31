using System;

namespace Game.Runtime.Infrastructure.Time
{
    public interface ITimeService : IDisposable
    {
        DateTime CurrentTime { get; }
        void Initialize();
        bool AddAndGetTimer(string id, float seconds, out Timer timer);
        void RemoveTimer(string id);
    }
}