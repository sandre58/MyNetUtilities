// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Mail.Smtp;

namespace MyNet.Utilities.Mail.MailKit
{
    public class MailKitServiceFactory : IMailServiceFactory
    {
        public IMailService Create(SmtpClientOptions options) => new MailKitService(options);
    }
}
