using SendGrid;
using SendGrid.Helpers.Mail;

namespace Phoenix.DataHandle.Senders
{
    public class EmailSender
    {
        public EmailAddress FromAddress { get; set; } = new("it@askphoenix", "AskPhoenix");

        private readonly SendGridClient _senderClient;

        public EmailSender(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            
            _senderClient = new SendGridClient(apiKey);
        }

        public EmailSender(string apiKey, string fromEmail, string? fromName)
            : this(apiKey)
        {
            if (string.IsNullOrEmpty(fromEmail))
                throw new ArgumentNullException(nameof(fromEmail));

            this.FromAddress = new(fromEmail, fromName);
        }

        public async Task<Response> SendAsync(
            string to, string subject, string? plainTextContent, string? htmlContent)
        {
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));
            if (subject is null)
                throw new ArgumentNullException(nameof(to));

            var toAddress = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail
                (FromAddress, toAddress, subject, plainTextContent, htmlContent);
            
            return await _senderClient.SendEmailAsync(msg);
        }
    }
}
