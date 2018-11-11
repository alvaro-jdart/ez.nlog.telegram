using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ez.NLog.Telegram
{
    [Target("Telegram")]
    public class TelegramTarget : AsyncTaskTarget
    {
        [RequiredParameter]
        public string HostUrl { get; set; } = "https://api.telegram.org/bot";

        [RequiredParameter]
        public string BotToken { get; set; }

        [RequiredParameter]
        public string ChatId { get; set; }

        private readonly Lazy<TelegramClient> _telegramClient;

        public TelegramTarget()
        {
            _telegramClient = new Lazy<TelegramClient>(() => new TelegramClient(HostUrl, BotToken));
        }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var message = Layout.Render(logEvent);

            var (isOk, response) = await _telegramClient.Value.SendMessage(ChatId, message);

            if (!isOk)
            {
                throw new Exception($"Telegram api error. Response: {response}");
            }
        }
    }
}
