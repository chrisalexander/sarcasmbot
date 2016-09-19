using System.Linq;
using MargieBot.Models;
using MargieBot.Responders;

namespace Sarcasmbot.Executable
{
    class SarcasmResponder : IResponder
    {
        private readonly SarcasmService sarcasm;

        public SarcasmResponder(SarcasmService sarcasm)
        {
            this.sarcasm = sarcasm;
        }

        public bool CanRespond(ResponseContext context)
        {
            return
                    !context.BotHasResponded                                // Must be new response
                    && context.Message.ChatHub.Type != SlackChatHubType.DM  // Must not be a DM
                    && context.Message.MentionsBot                          // Must mention me
                    && context.Message.Text.Contains("?");                  // Must be a "question"
        }

        public BotMessage GetResponse(ResponseContext context)
        {
            return new BotMessage { Text = context.Message.User.FormattedUserID + " " + this.sarcasm.Sarcasm() } ;
        }
    }
}
