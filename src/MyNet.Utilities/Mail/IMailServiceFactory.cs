// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Mail.Smtp;

namespace MyNet.Utilities.Mail
{
    public interface IMailServiceFactory
    {
        IMailService Create(SmtpClientOptions options);
    }
}
