using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace Game.Runtime.Infrastructure.Time
{
    public class LocalTimeService : ITimeService
    {
        public DateTime CurrentTime => DateTime.UtcNow;
        private readonly Dictionary<string, Timer> _timers = new();
        private readonly CancellationTokenSource _cts = new ();

        public void Initialize()
        {
            Timer().Forget();
        }

        public bool AddAndGetTimer(string id, float seconds, out Timer timer)
        {
            if (_timers.ContainsKey(id))
            {
                timer = null;
                Debug.LogWarning("Timer already exists!");
                return false;
            }
            
            var endTime = CurrentTime.AddSeconds(seconds);
            timer = new Timer(id, seconds, endTime);
            _timers.Add(id, timer);
            return true;
        }

        public void RemoveTimer(string id)
        {
            _timers.Remove(id);
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _timers.Clear();
        }

        private async UniTaskVoid  Timer()
        {
            var stoppedTimers = new List<(string key,Timer timer)>();
            while (true)
            {
                await UniTask.DelayFrame(1, cancellationToken: _cts.Token);

                if (!_timers.Any())
                {
                    continue;
                }
                
                var currentTime = CurrentTime;
                foreach (var timer in _timers)
                {
                    if (currentTime >= timer.Value.EndTime)
                    {
                        Debug.Log("Таймер завершился!");
                        stoppedTimers.Add((timer.Key, timer.Value));
                    }
                    else
                    {
                        var second = (float)(timer.Value.EndTime - currentTime).TotalSeconds;
                        var deltaTime = Mathf.Clamp01(second / timer.Value.Seconds);
                        timer.Value.Update(deltaTime);
                    }
                }

                foreach (var stoppedTimer in stoppedTimers)
                {
                    stoppedTimer.timer.Stop();
                    _timers.Remove(stoppedTimer.key);
                }

                stoppedTimers.Clear();
            }
        }
    }
}