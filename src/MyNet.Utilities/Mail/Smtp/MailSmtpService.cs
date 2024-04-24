// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using MyNet.Utilities.Mail.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyNet.Utilities.Mail.Smtp
{
    public sealed class MailSmtpService : IMailService, IDisposable
    {
        private readonly SmtpClient _smtpClient;

        public MailSmtpService(SmtpClient smtpClient) => _smtpClient = smtpClient;

        public MailSmtpService(SmtpClientOptions options) => _smtpClient = new SmtpClient(options.Server, options.Port)
        {
            Credentials = new NetworkCredential(options.User, options.Password),
            EnableSsl = options.UseSsl,
            PickupDirectoryLocation = options.MailPickupDirectory,
            UseDefaultCredentials = options.RequiresAuthentication,
            DeliveryMethod = options.UsePickupDirectory ? SmtpDeliveryMethod.SpecifiedPickupDirectory : SmtpDeliveryMethod.Network
        };

        public SendResponse Send(IEmail email, CancellationToken? token = null) => Task.Run(() => SendAsync(email, token)).Result;

        public async Task<SendResponse> SendAsync(IEmail email, CancellationToken? token = null)
        {
            var response = new SendResponse();

            using (var message = CreateMailMessage(email))
            {

                if (!(token?.IsCancellationRequested ?? false))
                {
                    await Task.Run(async () =>
                    {
                        token?.ThrowIfCancellationRequested();

                        var tcs = new TaskCompletionSource<bool>();
                        SendCompletedEventHandler? handler = null;
                        void unsubscribe() => _smtpClient.SendCompleted -= handler;

                        handler = async (s, e) =>
                        {
                            unsubscribe();

                            // a hack to complete the handler asynchronously
                            await Task.Yield();

                            _ = e.UserState != tcs
                                ? tcs.TrySetException(new InvalidOperationException("Unexpected UserState"))
                                : e.Cancelled ? tcs.TrySetCanceled() : e.Error != null ? tcs.TrySetException(e.Error) : tcs.TrySetResult(true);
                        };

                        _smtpClient.SendCompleted += handler;
                        try
                        {
                            _smtpClient.SendAsync(message, tcs);
                            using (token?.Register(() => _smtpClient.SendAsyncCancel(), useSynchronizationContext: false))
                            {
                                _ = await tcs.Task.ConfigureAwait(false);
                            }
                        }
                        finally
                        {
                            unsubscribe();
                        }
                    }).ConfigureAwait(false);

                    return response;
                }
            }

            return response;
        }

        public bool CanConnect() => SmtpHelper.TestSmtpConnection(_smtpClient.Host, _smtpClient.Port);

        public Task<bool> CanConnectAsync() => Task.FromResult(CanConnect());

        public static MailMessage CreateMailMessage(IEmail email)
        {
            var data = email.Data;
            MailMessage? message = null;

            // Smtp seems to require the HTML version as the alternative.
            if (!string.IsNullOrEmpty(data.PlaintextAlternativeBody))
            {
                message = new MailMessage
                {
                    Subject = data.Subject,
                    Body = data.PlaintextAlternativeBody,
                    IsBodyHtml = false,
                    From = new MailAddress(data.From.Address, data.From.Name)
                };

                var mimeType = new System.Net.Mime.ContentType("text/html; charset=UTF-8");
                var alternate = AlternateView.CreateAlternateViewFromString(data.Body, mimeType);
                message.AlternateViews.Add(alternate);
            }
            else
            {
                message = new MailMessage
                {
                    Subject = data.Subject,
                    Body = data.Body,
                    IsBodyHtml = data.IsHtml,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    From = new MailAddress(data.From.Address, data.From.Name)
                };
            }

            foreach (var header in data.Headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            data.To.ForEach(x => message.To.Add(new MailAddress(x.Address, x.Name)));
            data.Cc.ForEach(x => message.CC.Add(new MailAddress(x.Address, x.Name)));
            data.Bcc.ForEach(x => message.Bcc.Add(new MailAddress(x.Address, x.Name)));
            data.ReplyTo.ForEach(x => message.ReplyToList.Add(new MailAddress(x.Address, x.Name)));

            switch (data.Priority)
            {
                case Priority.Low:
                    message.Priority = MailPriority.Low;
                    break;
                case Priority.Normal:
                    message.Priority = MailPriority.Normal;
                    break;
                case Priority.High:
                    message.Priority = MailPriority.High;
                    break;
                default:
                    break;
            }

            data.Attachments.ForEach(x =>
            {
                if (x.Data != null)
                {
                    var a = new System.Net.Mail.Attachment(x.Data, x.Filename, x.ContentType)
                    {
                        ContentId = x.ContentId
                    };

                    message.Attachments.Add(a);
                }
            });

            return message;
        }
        public void Dispose() => _smtpClient.Dispose();
    }

}
