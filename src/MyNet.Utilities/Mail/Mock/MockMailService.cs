// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Threading;
using System.Threading.Tasks;
using MyNet.Utilities.Logging;
using MyNet.Utilities.Mail.Models;

namespace MyNet.Utilities.Mail.Mock
{
    public class MockMailService : IMailService
    {
        public bool CanConnect() => true;

        public Task<bool> CanConnectAsync() => Task.FromResult(true);

        public SendResponse Send(IEmail email, CancellationToken? token = null)
        {
            Thread.Sleep(1000);
            LogManager.Debug($"Simulate Sending Mail : {email}");

            return new SendResponse();
        }
        public async Task<SendResponse> SendAsync(IEmail email, CancellationToken? token = null) => await Task.Run(() =>
                                                                                                              {
                                                                                                                  Thread.Sleep(1000);
                                                                                                                  LogManager.Debug($"Simulate Sending Mail : {email}");
                                                                                                                  return new SendResponse();
                                                                                                              });
    }
}
