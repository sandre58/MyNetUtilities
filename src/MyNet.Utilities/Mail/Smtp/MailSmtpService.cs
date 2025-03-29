// -----------------------------------------------------------------------
// <copyright file="MailSmtpService.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyNet.Utilities.Mail.Models;

namespace MyNet.Utilities.Mail.Smtp;

public sealed class MailSmtpService(SmtpClient smtpClient) : IMailService, IDisposable
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose in class Dispose()")]
    public MailSmtpService(SmtpClientOptions options)
        : this(new SmtpClient(options.Server, options.Port)
        {
            Credentials = new NetworkCredential(options.User, options.Password),
            EnableSsl = options.UseSsl,
            PickupDirectoryLocation = options.MailPickupDirectory,
            UseDefaultCredentials = options.RequiresAuthentication,
            DeliveryMethod = options.UsePickupDirectory
            ? SmtpDeliveryMethod.SpecifiedPickupDirectory
            : SmtpDeliveryMethod.Network
        })
    {
    }

    public static MailMessage CreateMailMessage(IEmail email)
    {
        var data = email.Data;
        MailMessage? message;

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

        message.Priority = data.Priority switch
        {
            Priority.Low => MailPriority.Low,
            Priority.Normal => MailPriority.Normal,
            Priority.High => MailPriority.High,
            _ => message.Priority
        };

        data.Attachments.ForEach(x =>
        {
            if (x.Data == null) return;
            var a = new System.Net.Mail.Attachment(x.Data, x.Filename, x.ContentType) { ContentId = x.ContentId };

            message.Attachments.Add(a);
        });

        return message;
    }

    public SendResponse Send(IEmail email, CancellationToken? token = null) =>
        Task.Run(() => SendAsync(email, token)).Result;

    public async Task<SendResponse> SendAsync(IEmail email, CancellationToken? token = null)
    {
        var response = new SendResponse();

        if (token?.IsCancellationRequested ?? false) return response;
        await Task.Run(async () =>
        {
            token?.ThrowIfCancellationRequested();

            using var message = CreateMailMessage(email);
            var tcs = new TaskCompletionSource<bool>();

            smtpClient.SendCompleted += handler;
            try
            {
                smtpClient.SendAsync(message, tcs);
                await using (token?.Register(smtpClient.SendAsyncCancel, useSynchronizationContext: false))
                {
                    _ = await tcs.Task.ConfigureAwait(false);
                }
            }
            finally
            {
                smtpClient.SendCompleted -= handler;
            }

            async void handler(object s, System.ComponentModel.AsyncCompletedEventArgs e)
            {
                smtpClient.SendCompleted -= handler;

                // a hack to complete the handler asynchronously
                await Task.Yield();

                _ = e.UserState != tcs
                    ? tcs.TrySetException(new InvalidOperationException("Unexpected UserState"))
                    : e.Cancelled
                        ? tcs.TrySetCanceled()
                        : e.Error != null
                            ? tcs.TrySetException(e.Error)
                            : tcs.TrySetResult(true);
            }
        }).ConfigureAwait(false);

        return response;
    }

    public bool CanConnect() => SmtpHelper.TestSmtpConnection(smtpClient.Host, smtpClient.Port);

    public Task<bool> CanConnectAsync() => Task.FromResult(CanConnect());

    public void Dispose() => smtpClient.Dispose();
}
