using Microsoft.AspNet.Identity;
using Nexmo.Api;
using Nexmo.Api.Request;
using System;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Sms
{
    public class SmsService : IIdentityMessageService
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

        public Task SendAsync(IdentityMessage message)
        {
            if (string.IsNullOrEmpty(this.apiKey) || string.IsNullOrEmpty(this.apiSecret))
                throw new Exception("SMS API credentials not defined.");

            string phone;
            if (message.Destination.StartsWith("69"))
                phone = "+30" + message.Destination;
            else if (message.Destination.StartsWith("30"))
                phone = "+" + message.Destination;
            else if (message.Destination.StartsWith("+30"))
                phone = message.Destination;
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
                text = message.Body
            });

            return Task.FromResult(0);
        }
    }
}
