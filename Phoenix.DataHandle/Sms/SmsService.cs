using Nexmo.Api;
using Nexmo.Api.Request;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Sms
{
    public class SmsService : ISmsService
    {
        private string apiKey;
        private string apiSecret;
        private string from = "Phoenix";

        public string ApiKey { set => this.apiKey = value; }
        public string ApiSecret { set => this.apiSecret = value; }
        public string From { set => this.from = value; }

        public SmsService(string apiKey, string apiSecret, string from = "AskPhoenix")
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.from = from;
        }

        public Task SendAsync(string destination, string body)
        {
            if (string.IsNullOrEmpty(this.apiKey) || string.IsNullOrEmpty(this.apiSecret))
                throw new Exception("SMS API credentials not defined.");

            string phone;
            if (destination.StartsWith("69"))
                phone = "+30" + destination;
            else if (destination.StartsWith("30"))
                phone = "+" + destination;
            else if (destination.StartsWith("+30"))
                phone = destination;
            else
                throw new Exception("Invalid phone number. Either it is not a Greek phone number, or not a mobile one.");

            var client = new Client(creds: new Credentials
            {
                ApiKey = this.apiKey,
                ApiSecret = this.apiSecret
            });

            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = this.from,
                to = phone,
                text = body,
                type = "unicode"
            });

            if(results.messages.Any(m => m.status != "0"))
                throw new Exception(results.messages.FirstOrDefault(m => !string.IsNullOrWhiteSpace(m.error_text))?.error_text);


            return Task.CompletedTask;
        }
    }
}
