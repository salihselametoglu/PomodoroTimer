using PomodoroTimerLogic;
using System;
using System.Windows.Forms;

namespace PomodoroTimerWinUI
{
    public partial class PomodoroForm : Form
    {
        PomodoroTimer _p;

        public PomodoroForm()
        {
            InitializeComponent();

            _p = new PomodoroTimer();
        }

        private void BtnStartTimer(object sender, EventArgs e)
        {
            _p.StartPomodoroTimer();

            _p.CounterEvent = new Action<int>(CounterEvent);
            _p.FinishEvent = new Action(FinishEvent);
        }

        private void FinishEvent()
        {
            Console.Beep(10000, 2000);
        }

        public void CounterEvent(int p)
        {
            if (lblCounter.InvokeRequired)
                lblCounter.Invoke(new MethodInvoker(() =>
                {
                    CounterEvent(p);
                }));

            lblCounter.Text = p.ToString();
        }

        private void BtnStopTimer(object sender, EventArgs e)
        {
            _p.StopPomodoroTimer();
        }
    }
}
