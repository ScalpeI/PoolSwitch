using PoolSwitch.Model.Log;
using PoolSwitch.Model.Request;
using PoolSwitch.Model.Work;
using System;
using System.Windows;

namespace PoolSwitch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool OnWorking = false;
        LogManager logManager;
        public MainWindow()
        {
            //TextBlock.Text = "";
            InitializeComponent();


            logManager = new LogManager(TextBlock);
            Console.SetOut(logManager);
            if (!OnWorking)
            {
                Start.IsEnabled = true;
                Stop.IsEnabled = false;
            }
            else
            {
                Start.IsEnabled = false;
                Stop.IsEnabled = true;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            OnWorking = Starter.OnStarting();
            if (!OnWorking)
            {
                Start.IsEnabled = true;
                Stop.IsEnabled = false;
            }
            else
            {
                Console.WriteLine("{0} : Process starting.", DateTime.Now);
                Start.IsEnabled = false;
                Stop.IsEnabled = true;
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            OnWorking = Starter.OnStoping();
            if (!OnWorking)
            {
                Console.WriteLine("{0} : Process stoping.", DateTime.Now);
                Start.IsEnabled = true;
                Stop.IsEnabled = false;
            }
            else
            {
                Start.IsEnabled = false;
                Stop.IsEnabled = true;
            }
        }
    }
}
