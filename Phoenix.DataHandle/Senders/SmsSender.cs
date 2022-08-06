using Vonage;
using Vonage.Request;

namespace Phoenix.DataHandle.Senders
{
    public class SmsSender
    {
        public string From { get; set; } = "AskPhoenix";

        private readonly VonageClient _senderClient;

        public SmsSender(string apiKey, string apiSecret)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrEmpty(apiSecret))
                throw new ArgumentNullException(nameof(apiSecret));
            
            _senderClient = new(Credentials.FromApiKeyAndSecret(apiKey, apiSecret));
        }

        public SmsSender(string apiKey, string apiSecret, string from)
            : this(apiKey, apiSecret)
        {
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException(nameof(from));

            this.From = from;
        }

        public async Task SendAsync(string to, string content)
        {
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            // TODO: Verify phone number according to E.164 standard: ^\+[1-9]\d{1,14}$
            if (!to.All(d => char.IsDigit(d) || d == '+'))
                throw new ArgumentException($"{nameof(to)} is not a valid phone number.");

            var response = await _senderClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
            {
                To = to,
                From = this.From,
                Text = content,
                Type = Vonage.Messaging.SmsType.Unicode
            });

            if (response.Messages.Any(m => m.Status != "0"))
                throw new Exception("SMS send failed:\n"
                    + string.Join('\n', response.Messages.Select(m => m.ErrorText ?? "")));
        }
    }
}
