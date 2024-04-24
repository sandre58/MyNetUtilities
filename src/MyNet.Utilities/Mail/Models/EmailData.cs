// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace MyNet.Utilities.Mail.Models
{
    public class EmailData(EmailAddress from)
    {
        public List<EmailAddress> To { get; } = [];
        public List<EmailAddress> Cc { get; } = [];
        public List<EmailAddress> Bcc { get; } = [];
        public List<EmailAddress> ReplyTo { get; } = [];
        public List<Attachment> Attachments { get; } = [];
        public EmailAddress From { get; set; } = from;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string PlaintextAlternativeBody { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public List<string> Tags { get; } = [];

        public bool IsHtml { get; set; }
        public Dictionary<string, string> Headers { get; } = [];
    }
}
