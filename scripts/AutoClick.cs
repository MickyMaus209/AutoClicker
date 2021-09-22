using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoClicker.scripts
{
    public class AutoClick
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private double delay;
        public string key { get; }
        public CancellationTokenSource cts { get; set; }
        public static bool isRunning;

        public AutoClick(double delay, string key)
        {
            this.cts = new CancellationTokenSource();
            this.delay = delay;
            this.key = key;
        }

        public void Start()
        {
            isRunning = true;
            Task.Run(async () =>
            {
                while (isRunning)
                {
                    mouse_event(dwFlags: 0x0003, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(1);
                    mouse_event(dwFlags: 0x0001, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                }
                await Task.Delay(TimeSpan.FromSeconds(delay), this.cts.Token);
            }, this.cts.Token);
        }

        public void Stop()
        {
            isRunning = false;
            this.cts.Cancel();
            this.cts = null;
            this.cts = new CancellationTokenSource();
        }
    }
}
