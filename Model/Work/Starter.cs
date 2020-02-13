using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timers = PoolSwitch.Model.Work.Timer.Timer;

namespace PoolSwitch.Model.Work
{
    static class Starter
    {
        public static bool OnStarting()
        {
            try
            {
                if (!Timers.Initialized)
                    Timers.Initialize();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public static bool OnStoping()
        {
            try
            {
                if (Timers.Initialized)
                    Timers.Dispose();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return true;
            }
        }
    }
}
