using Nexmo.Api;
using Nexmo.Api.Request;
using System;
using System.Linq;

namespace Phoenix.DataHandle.Sms
{
    public class SmsService : ISmsService
    {
        private string ApiKey { get; }
        private string ApiSecret { get; }
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

            this.ApiKey = apiKey;
            this.ApiSecret = apiSecret;
            this.From = from;

            this.NexmoClient = new Client(new Credentials
            {
                ApiKey = this.ApiKey,
                ApiSecret = this.ApiSecret
            });
        }

        public void Send(string destination, string body)
        {
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));
            if (string.IsNullOrEmpty(body))
                throw new ArgumentNullException(nameof(body));

            string phone;
            destination = destination.Trim();
            destination.All(d => char.IsDigit(d) || d == '+');

            if (destination.StartsWith("69") && destination.Length == 10)
                phone = "+30" + destination;
            else if (destination.StartsWith("3069"))
                phone = "+" + destination;
            else if (destination.StartsWith("003069"))
                phone = "+" + destination[2..];
            else if (destination.StartsWith("+3069"))
                phone = destination;
            else
                throw new Exception("Invalid phone number. Either it is not a Greek phone number, or not a mobile one.");

            var results = this.NexmoClient.SMS.Send(request: new SMS.SMSRequest
            {
                from = this.From,
                to = phone,
                text = body,
                type = "unicode"
            });

            if(results.messages.Any(m => m.status != "0"))
                throw new Exception(results.messages.FirstOrDefault(m => !string.IsNullOrWhiteSpace(m.error_text))?.error_text);
        }
    }
}
