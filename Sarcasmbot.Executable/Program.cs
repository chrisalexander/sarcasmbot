using System;
using System.Threading;

namespace Sarcasmbot.Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new BotManager();

            var cts = new CancellationTokenSource();

            manager.Execute(cts.Token);

            Console.WriteLine("Enter to exit");
            Console.ReadLine();
            cts.Cancel();
        }
    }
}
