using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MargieBot;

namespace Sarcasmbot.Executable
{
    class BotManager
    {
        public async void Execute(CancellationToken token)
        {
            var bot = new Bot();

            var apiKey = File.ReadLines("SlackApiKey.txt").First();

            await bot.Connect(apiKey);

            var service = new SarcasmService();
            
            bot.Responders.Add(new SarcasmResponder(service));

            while (true)
            {
                await Task.Delay(1000);

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}
