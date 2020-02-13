using PoolSwitch.Model.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PoolSwitch.Model.Work.Timer
{
    static class Timer
    {
        public static bool Initialized { get; set; }
        static DispatcherTimer dTimer = new DispatcherTimer();
        //static BlockHeight blockHeight = new BlockHeight();
        static GetBlocks getBlocks = new GetBlocks();
        static Probability probability = new Probability();
        static Timer()
        {
            Initialized = false;
            dTimer.Tick += new EventHandler(timer_Tick);
            dTimer.Interval = new TimeSpan(0, 10, 0);
        }
        private static void timer_Tick(object sender, EventArgs e)
        {
            Thread();
        }
        private static void Thread()
        {
            //Thread GetBlocks = new Thread(new ThreadStart(blockHeight.GetBlocks));
            Thread GetBlocks = new Thread(new ThreadStart(getBlocks.Get));
            if (GetBlocks.ThreadState == ThreadState.Unstarted && getBlocks.Initialized == false)
            {
                //blockHeight.GetBlocks();
                GetBlocks.Start();
            }
            //Console.WriteLine("Текущее время : {0}", DateTime.Now);
        }
        public static void Initialize()
        {
            try
            {
                Initialized = true;
                dTimer.Start();
                Thread();
            }
            catch (Exception ex)
            {
                Initialized = false;
                Console.WriteLine(ex);
            }
        }
        public static void Dispose()
        {
            try
            {
                Initialized = false;
                dTimer.Stop();
            }
            catch (Exception ex)
            {
                Initialized = true;
                Console.WriteLine(ex);
            }
        }
    }
}


