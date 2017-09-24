using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
//using System.Timers;

namespace PomodoroTimerLogic
{
    public class SchedulerTimer
    {
        //private Timer _timer;
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
            }
        }

        public void Pause()
        {
            //_timer.Enabled = false;

            _timer.Change(0, Timeout.Infinite);
        }

        public void Resume()
        {
            //_timer.Enabled = true;

            _timer.Change(0, _period);
        }

        //private void Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    if (Event == null)
        //        return;

        //    try
        //    {
        //        Event.Invoke();
        //    }
        //    catch (Exception ex)
        //    {
        //        var builder = new StringBuilder();
        //        builder
        //            .Append(String.Concat("Error Message : ", ex.Message))
        //            .Append(String.Concat("Error InnerMessage : ", ex.InnerException == null ? "null" : ex.InnerException.Message))
        //            .Append(String.Concat("Error Source : ", ex.Source))
        //            .Append(String.Concat("Error StackTrace : ", ex.StackTrace));

        //        Debug.WriteLine(builder.ToString());
        //    }
        //}

        public void Stop()
        {
            //_timer.Stop();

            //_timer.Enabled = false;
            //_timer.Elapsed -= Elapsed;

            //_timer.Dispose();
            //_timer.Close();

            _timer.Change(0, Timeout.Infinite);
            _timer.Dispose();
        }
    }
}
