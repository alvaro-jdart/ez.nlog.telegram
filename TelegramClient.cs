using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ez.NLog.Telegram
{
    internal class TelegramClient
    {
        private const string okTemplate = "\"ok\":true";

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;

        public TelegramClient(string host, string botToken)
        {
            _baseUrl = host + botToken;
        }

        public async Task<(bool isOk, string response)> SendMessage(string chatId, string message)
        {
            var url = $"{_baseUrl}/sendMessage?chat_id={chatId}&text={WebUtility.UrlEncode(message)}";
            var response = await Get(url);
            return (response.Contains(okTemplate), response);
        }

        private async Task<string> Get(string url)
        {
            using (var response = await _httpClient.GetAsync(url).ConfigureAwait(false))
            {
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}
