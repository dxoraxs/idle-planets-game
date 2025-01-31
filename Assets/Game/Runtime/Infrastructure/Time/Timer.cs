using System;

namespace Game.Runtime.Infrastructure.Time
{
    public class Timer
    {
        public readonly string PlanetId;
        public readonly float Seconds;
        public readonly DateTime EndTime;

        public Timer(string planetId, float seconds, DateTime endTime)
        {
            PlanetId = planetId;
            Seconds = seconds;
            EndTime = endTime;
        }

        public event Action<float> Ticked;
        public event Action Ended;

        public void Update(float deltaTime)
        {
            Ticked?.Invoke(deltaTime);
        }

        public void Stop()
        {
            Ended?.Invoke();
        }
    }
}