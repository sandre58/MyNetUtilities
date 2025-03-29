// -----------------------------------------------------------------------
// <copyright file="EmailFactory.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Mail;

public class EmailFactory(string from, string displayName = "") : IEmailFactory
{
    public IEmail Create() => Email.From(from, displayName);
}
