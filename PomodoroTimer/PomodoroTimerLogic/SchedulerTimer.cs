using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace PomodoroTimerLogic
{
    public class SchedulerTimer
    {
        private Timer _timer;
        private int _period = 60000;

        public Action Event;

        public void Start()
        {
            if (_timer != null)
            {
                _timer = null;
            }

            _timer = new Timer(Test, null, 0, _period);
        }

        private void Test(object state)
        {
            if (Event == null)
                return;

            try
            {
                Event.Invoke();
            }
            catch (Exception ex)
            {
                var builder = new StringBuilder();
                builder
                    .Append(String.Concat("Error Message : ", ex.Message))
                    .Append(String.Concat("Error InnerMessage : ", ex.InnerException == null ? "null" : ex.InnerException.Message))
                    .Append(String.Concat("Error Source : ", ex.Source))
                    .Append(String.Concat("Error StackTrace : ", ex.StackTrace));

                Debug.WriteLine(builder.ToString());

                throw ex;
            }
        }

        public void Pause()
        {
            _timer.Change(0, Timeout.Infinite);
        }

        public void Resume()
        {
            _timer.Change(0, _period);
        }

        public void Stop()
        {
            _timer.Change(0, Timeout.Infinite);
            _timer.Dispose();
        }
    }
}
