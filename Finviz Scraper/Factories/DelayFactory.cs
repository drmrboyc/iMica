using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finviz_Scraper.Factories
{
    public static class DelayFactory
    {
        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new Timer();
            timer.Tick += delegate
            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = millisecond;
            timer.Start();
        }
    }
}
