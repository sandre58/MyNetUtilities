// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Mail.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyNet.Utilities.Mail
{
    public interface IMailService
    {
        SendResponse Send(IEmail email, CancellationToken? token = null);

        Task<SendResponse> SendAsync(IEmail email, CancellationToken? token = null);

        bool CanConnect();

        Task<bool> CanConnectAsync();
    }
}
