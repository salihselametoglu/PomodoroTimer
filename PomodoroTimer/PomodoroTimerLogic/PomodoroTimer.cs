using System;
using System.Diagnostics;

namespace PomodoroTimerLogic
{
    public class PomodoroTimer
    {
        private int _pomodoroRange = 25;
        private DateTime _startPomodoro;
        private DateTime _stopPomodoro;
        private DateTime _breakStartPomodoro;
        private DateTime _breakStopPomodoro;
        private SchedulerTimer _schedulerTimer;

        public Action<int> CounterEvent;
        public Action FinishEvent;

        public PomodoroTimer()
        {
            _schedulerTimer = new SchedulerTimer();
            _schedulerTimer.Event =
                () =>
                {
                    Debug.WriteLine("PomodoroTimer Event");

                    if (CounterEvent == null)
                        return;

                    CounterEvent.Invoke(_pomodoroRange--);

                    if (_pomodoroRange < 0)
                    {
                        StopPomodoroTimer();

                        if (FinishEvent == null)
                            return;

                        FinishEvent.Invoke();
                    }

                };
        }

        public void StartPomodoroTimer()
        {
            _startPomodoro = DateTime.Now;
            _pomodoroRange = 25;

            _schedulerTimer.Start();
        }

        public void BreakStartPomodoroTimer()
        {
            _breakStartPomodoro = DateTime.Now;
            _schedulerTimer.Pause();
        }

        public void BreakStopPomodoroTimer()
        {
            _breakStopPomodoro = DateTime.Now;
            _schedulerTimer.Resume();
        }

        public void StopPomodoroTimer()
        {
            _stopPomodoro = DateTime.Now;
            _schedulerTimer.Stop();
        }

        public void ResetPomodoroTimer()
        {
            _startPomodoro = new DateTime();
            _stopPomodoro = new DateTime();
            _breakStartPomodoro = new DateTime();
            _breakStopPomodoro = new DateTime();
        }
    }
}
