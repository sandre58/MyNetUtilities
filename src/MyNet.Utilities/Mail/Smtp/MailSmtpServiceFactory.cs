// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Mail.Smtp
{
    public class MailSmtpServiceFactory : IMailServiceFactory
    {
        public IMailService Create(SmtpClientOptions options) => new MailSmtpService(options);
    }
}
