using System.Text.RegularExpressions;
using MargieBot.Models;
using MargieBot.Responders;

namespace Sarcasmbot.Executable
{
    class SarcasmSetup : IResponder
    {
        private readonly Regex addRegex = new Regex(@"^add\s+");

        private readonly Regex removeRegex = new Regex(@"^remove\s+");

        private readonly SarcasmService sarcasm;

        public SarcasmSetup(SarcasmService sarcasm)
        {
            this.sarcasm = sarcasm;
        }

        public bool CanRespond(ResponseContext context)
        {
            return
                    !context.BotHasResponded                                // Must be new response
                    && context.Message.ChatHub.Type == SlackChatHubType.DM; // Must be a DM
        }

        public BotMessage GetResponse(ResponseContext context)
        {
            if (context.Message.Text.StartsWith("add"))
            {
                this.sarcasm.Add(this.addRegex.Replace(context.Message.Text, string.Empty));

                return new BotMessage { Text = "Added - thanks for your sarcasm." };
            }
            else if (context.Message.Text.StartsWith("list"))
            {
                var text = "Choose from the below, then remove with 'remove n' where n = index.\r\n\r\n";

                var list = this.sarcasm.List();

                for (var i = 0; i < list.Count; i++)
                {
                    text += $"{i}: {list[i]}\r\n";
                }

                return new BotMessage { Text = text };
            }
            else if (context.Message.Text.StartsWith("remove"))
            {
                try
                {
                    this.sarcasm.Remove(int.Parse(this.removeRegex.Replace(context.Message.Text, string.Empty)));
                    return new BotMessage { Text = "Sarcasm removed." };
                }
                catch
                {
                    return new BotMessage { Text = "Invalid format. Please stop trying to crash me." };
                }
            }

            return new BotMessage { Text = "Please stop talking to me." };
        }
    }
}
