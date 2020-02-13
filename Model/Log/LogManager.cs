using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PoolSwitch.Model.Log
{
    public class LogManager:TextWriter
    {
        TextBlock textBlock = null;

        public  LogManager(TextBlock log)
        {
            textBlock = log;
        }

        public override void Write(char value)
        {
            base.Write(value);
            textBlock.Dispatcher.BeginInvoke(new Action(() =>
            {
                textBlock.Text += value;
            }));
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
