using Phoenix.DataHandle.Senders;

namespace Phoenix.Verification.DataHandle.Tests
{
    public class SenderTests : ConfigurationTestsBase
    {
        [Fact]
        public async void EmailSendTest()
        {
            var emailSender = new EmailSender(_configuration["SendGrid:Key"]);

            var emailResponse = await emailSender.SendAsync(
                to: "tspyrou@askphoenix.gr",
                subject: "SendGrid Test",
                plainTextContent: "This is plain text",
                htmlContent: $"This is <b>html</b>");

            Assert.True(emailResponse.IsSuccessStatusCode);
        }
    }
}
