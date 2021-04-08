using System.Collections.Generic;

namespace WebApi.Model
{
    public class SendEmailRequest
    {
        public EmailAddress FromAddress { get; set; }
        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress>? CcAddresses { get; set; }
        public List<EmailAddress>? BccAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtmlBody { get; set; }
    }
}
