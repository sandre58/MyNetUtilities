// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Mail.Smtp
{
    public class SmtpClientOptions
    {
        public string? Server { get; set; }
        public int Port { get; set; } = 25;
        public string? User { get; set; }
        public string? Password { get; set; }
        public bool UseSsl { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string? PreferredEncoding { get; set; }
        public bool UsePickupDirectory { get; set; }
        public string? MailPickupDirectory { get; set; }
    }
}
