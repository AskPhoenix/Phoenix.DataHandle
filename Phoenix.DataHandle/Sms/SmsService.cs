using Nexmo.Api;
using Nexmo.Api.Request;

namespace Phoenix.DataHandle.Sms
{
    public class SmsService : ISmsService
    {
        public string From { get; set; }

        private Client NexmoClient { get; set; }

        public SmsService(string apiKey, string apiSecret, string from = "AskPhoenix")
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrEmpty(apiSecret))
                throw new ArgumentNullException(nameof(apiSecret));
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException(nameof(from));

            this.From = from;

            this.NexmoClient = new Client(new Credentials
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret
            });
        }

        public void Send(string destination, string body)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));
            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException(nameof(body));

            destination = destination.Trim();
            if (!destination.All(d => char.IsDigit(d) || d == '+'))
                throw new ArgumentException($"{nameof(destination)} is not a valid phone number.");

            var results = this.NexmoClient.SMS.Send(request: new SMS.SMSRequest
            {
                from = this.From,
                to = destination,
                text = body,
                type = "unicode"
            });

            if(results.messages.Any(m => m.status != "0"))
                throw new Exception(results.messages.FirstOrDefault(m => !string.IsNullOrWhiteSpace(m.error_text))?.error_text);
        }
    }
}
