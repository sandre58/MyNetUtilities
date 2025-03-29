﻿// -----------------------------------------------------------------------
// <copyright file="IEmailFactory.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Mail;

public interface IEmailFactory
{
    IEmail Create();
}
