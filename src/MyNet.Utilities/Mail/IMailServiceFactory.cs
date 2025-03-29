// -----------------------------------------------------------------------
// <copyright file="IMailServiceFactory.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Mail.Smtp;

namespace MyNet.Utilities.Mail;

public interface IMailServiceFactory
{
    IMailService Create(SmtpClientOptions options);
}
