using System.Diagnostics;

namespace EventsInMultithreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventRunner lRunner = new();
            while (true)
            {
                if (lRunner.MyEvent != null)
                {
                    lRunner.MyEvent -= Write;
                }
                else
                {
                    lRunner.MyEvent += Write;
                }
            }
        }

        private static void Write(object pRunner, EventArgs pArgs)
        {
            EventRunner lRunner = pRunner as EventRunner;
            if (lRunner.MyEvent == null)
            {
                Console.WriteLine("Looks like no Subscriber is there.");
                Debugger.Break();
            }
            else
            {
                Console.WriteLine("All is consistent.");
            }
        }
    }
    internal class EventRunner
    {
        internal EventHandler MyEvent;
        public EventRunner()
        {
            Task.Factory.StartNew(RunnerAction);
        }
        void RunnerAction()
        {
            while (true)
            {
                MyEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
