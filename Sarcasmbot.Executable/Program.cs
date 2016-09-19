namespace Sarcasmbot.Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new BotManager();
            manager.Execute().Wait();
        }
    }
}
