﻿// -----------------------------------------------------------------------
// <copyright file="MailKitServiceFactory.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MyNet.Utilities.Mail.Smtp;

namespace MyNet.Utilities.Mail.MailKit;

public class MailKitServiceFactory : IMailServiceFactory
{
    public IMailService Create(SmtpClientOptions options) => new MailKitService(options);
}
