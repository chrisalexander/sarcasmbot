using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MargieBot;

namespace Sarcasmbot.Executable
{
    class BotManager
    {
        public async Task Execute()
        {
            var bot = new Bot();

            var apiKey = File.ReadLines("SlackApiKey.txt").First();

            await bot.Connect(apiKey);

            bot.MessageReceived += (msg) => Console.WriteLine(msg);

            await Task.Delay(10000000);
        }
    }
}
