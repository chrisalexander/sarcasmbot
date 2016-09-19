using System;
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
            Console.WriteLine($"Received DM from {context.Message.User.FormattedUserID}: {context.Message.Text}");

            if (context.Message.Text.StartsWith("add"))
            {
                var newSarcasm = this.addRegex.Replace(context.Message.Text, string.Empty);

                if (string.IsNullOrWhiteSpace(newSarcasm))
                {
                    return new BotMessage { Text = "Please type some sarcasm." };
                }

                this.sarcasm.Add(newSarcasm);

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
