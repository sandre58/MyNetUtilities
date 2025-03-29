// -----------------------------------------------------------------------
// <copyright file="IMailService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using MyNet.Utilities.Mail.Models;

namespace MyNet.Utilities.Mail;

public interface IMailService
{
    SendResponse Send(IEmail email, CancellationToken? token = null);

    Task<SendResponse> SendAsync(IEmail email, CancellationToken? token = null);

    bool CanConnect();

    Task<bool> CanConnectAsync();
}
