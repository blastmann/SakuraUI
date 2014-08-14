using System;
using Windows.System;
using Windows.UI.Xaml;

namespace SakuraUI.WindowsPhone.Controls
{
    public sealed partial class PhoneMemory
    {
        public PhoneMemory()
        {
            InitializeComponent();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += TimerOnTick;
            timer.Start();
        }

        private void TimerOnTick(object sender, object o)
        {
            try
            {
                var curUsage = MemoryManager.AppMemoryUsage / 1024.0 / 1024.0;
                var lmtUsage = MemoryManager.AppMemoryUsageLimit / 1024.0 / 1024.0;
                InfoBlock.Text = string.Format("{0:00.00}/{1}", curUsage, lmtUsage);
            }
            catch (Exception ex)
            {
                InfoBlock.Text = ex.Message;
            }
        }
    }
}
