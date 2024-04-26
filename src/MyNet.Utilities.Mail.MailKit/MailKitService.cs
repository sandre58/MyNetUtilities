// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MyNet.Utilities.Exceptions;
using MyNet.Utilities.Logging;
using MyNet.Utilities.Mail.Models;
using MyNet.Utilities.Mail.Smtp;

namespace MyNet.Utilities.Mail.MailKit
{
    /// <summary>
    /// Creates a sender that uses the given SmtpClientOptions when sending with MailKit. Since the client is internal this will dispose of the client.
    /// </summary>
    /// <param name="smtpClientOptions">The SmtpClientOptions to use to create the MailKit client</param>
    public sealed class MailKitService(SmtpClientOptions smtpClientOptions) : IMailService
    {
        private readonly SmtpClientOptions _smtpClientOptions = smtpClientOptions;

        /// <summary>
        /// Send the specified email.
        /// </summary>
        /// <returns>A response with any errors and a success boolean.</returns>
        /// <param name="email">Email.</param>
        /// <param name="token">Cancellation Token.</param>
        public SendResponse Send(IEmail email, CancellationToken? token = null)
        {
            var response = new SendResponse();
            var message = CreateMailMessage(email);

            using (LogManager.MeasureTime())
            {
                if (!(token?.IsCancellationRequested ?? false))
                {
                    try
                    {
                        CheckOptions();
                        CheckMessage(message);

                        if (_smtpClientOptions.UsePickupDirectory)
                        {
                            SaveToPickupDirectoryAsync(message, _smtpClientOptions.MailPickupDirectory).Wait();
                            return response;
                        }

                        using var client = new SmtpClient
                        {
                            SslProtocols = SslProtocols.None | SslProtocols.Tls12
                        };
                        client.Connect(
                            _smtpClientOptions.Server,
                            _smtpClientOptions.Port,
                            SecureSocketOptions.Auto,
                            token.GetValueOrDefault());

                        // Note: only needed if the SMTP server requires authentication
                        if (_smtpClientOptions.RequiresAuthentication)
                        {
                            client.Authenticate(_smtpClientOptions.User, _smtpClientOptions.Password, token.GetValueOrDefault());
                        }

                        _ = client.Send(message, token.GetValueOrDefault());
                        client.Disconnect(true, token.GetValueOrDefault());
                    }
                    catch (Exception ex)
                    {
                        response.ErrorMessages.Add(ex.Message);
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Send the specified email.
        /// </summary>
        /// <returns>A response with any errors and a success boolean.</returns>
        /// <param name="email">Email.</param>
        /// <param name="token">Cancellation Token.</param>
        public async Task<SendResponse> SendAsync(IEmail email, CancellationToken? token = null)
        {
            var response = new SendResponse();
            var message = CreateMailMessage(email);

            using (LogManager.MeasureTime())
            {
                if (!(token?.IsCancellationRequested ?? false))
                {
                    try
                    {
                        CheckOptions();
                        CheckMessage(message);

                        if (_smtpClientOptions.UsePickupDirectory)
                        {
                            await SaveToPickupDirectoryAsync(message, _smtpClientOptions.MailPickupDirectory).ConfigureAwait(false);
                            return response;
                        }

                        using var client = new SmtpClient
                        {
                            SslProtocols = SslProtocols.None | SslProtocols.Tls12
                        };
                        await client.ConnectAsync(
                            _smtpClientOptions.Server,
                            _smtpClientOptions.Port,
                            SecureSocketOptions.Auto,
                            token.GetValueOrDefault()).ConfigureAwait(false);

                        // Note: only needed if the SMTP server requires authentication
                        if (_smtpClientOptions.RequiresAuthentication)
                        {
                            await client.AuthenticateAsync(_smtpClientOptions.User, _smtpClientOptions.Password, token.GetValueOrDefault()).ConfigureAwait(false);
                        }

                        _ = await client.SendAsync(message, token.GetValueOrDefault()).ConfigureAwait(false);
                        await client.DisconnectAsync(true, token.GetValueOrDefault()).ConfigureAwait(false);

                        LogManager.Info($"Mail has been sended with success : {email}");
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(ex.Message);
                        response.ErrorMessages.Add(ex.Message);
                    }

                    return response;
                }
            }

            return response;
        }

        /// <summary>
        /// Saves email to a pickup directory.
        /// </summary>
        /// <param name="message">Message to save for pickup.</param>
        /// <param name="pickupDirectory">Pickup directory.</param>
        private static async Task SaveToPickupDirectoryAsync(MimeMessage message, string? pickupDirectory)
        {
            // Note: this will require that you know where the specified pickup directory is.
            var path = Path.Combine(pickupDirectory ?? string.Empty, Guid.NewGuid().ToString() + ".eml");

            if (File.Exists(path))
            {
                return;
            }

            using var stream = new FileStream(path, FileMode.CreateNew);
            await message.WriteToAsync(stream).ConfigureAwait(false);
        }

        public bool CanConnect()
        {
            SmtpClient? client = null;

            try
            {
                CheckOptions();

                client = new SmtpClient
                {
                    SslProtocols = SslProtocols.None | SslProtocols.Tls12
                };
                client.Connect(
                    _smtpClientOptions.Server,
                    _smtpClientOptions.Port,
                     SecureSocketOptions.Auto);

                if (_smtpClientOptions.RequiresAuthentication)
                {
                    client.Authenticate(_smtpClientOptions.User, _smtpClientOptions.Password);
                }

                client.Disconnect(true);
            }
            catch (Exception e)
            {
                LogManager.Info(e.Message);
                return false;
            }
            finally
            {
                client?.Dispose();
            }

            return true;
        }

        public async Task<bool> CanConnectAsync()
        {
            SmtpClient? client = null;

            try
            {
                CheckOptions();
                var protocols = SslProtocols.None | SslProtocols.Tls12 | SslProtocols.Tls13;

                client = new SmtpClient
                {
                    SslProtocols = protocols
                };
                await client.ConnectAsync(
                    _smtpClientOptions.Server,
                    _smtpClientOptions.Port,
                   _smtpClientOptions.UseSsl ? SecureSocketOptions.StartTlsWhenAvailable : SecureSocketOptions.None).ConfigureAwait(false);

                if (_smtpClientOptions.RequiresAuthentication)
                {
                    await client.AuthenticateAsync(_smtpClientOptions.User, _smtpClientOptions.Password).ConfigureAwait(false);
                }

                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                client?.Dispose();
            }

            return true;
        }

        private static void CheckMessage(MimeMessage message)
        {
            if (!message.From.Mailboxes.Any(x => !string.IsNullOrEmpty(x.Address)))
                throw new EmptySenderAddressesException();
        }

        private void CheckOptions()
        {
            if (string.IsNullOrEmpty(_smtpClientOptions.Server)) throw new UndefinedServerException();
        }

        /// <summary>
        /// Create a MimMessage so MailKit can send it
        /// </summary>
        /// <returns>The mail message.</returns>
        /// <param name="email">Email data.</param>
        public static MimeMessage CreateMailMessage(IEmail email)
        {
            var data = email.Data;

            var message = new MimeMessage
            {
                Subject = data.Subject ?? string.Empty
            };

            message.From.Add(new MailboxAddress(data.From.Name, data.From.Address));
            data.To.ForEach(x => message.To.Add(new MailboxAddress(x.Name, x.Address)));
            data.Cc.ForEach(x => message.Cc.Add(new MailboxAddress(x.Name, x.Address)));
            data.Bcc.ForEach(x => message.Bcc.Add(new MailboxAddress(x.Name, x.Address)));
            data.ReplyTo.ForEach(x => message.ReplyTo.Add(new MailboxAddress(x.Name, x.Address)));

            var builder = new BodyBuilder();
            if (!string.IsNullOrEmpty(data.PlaintextAlternativeBody))
            {
                builder.TextBody = data.PlaintextAlternativeBody;
                builder.HtmlBody = data.Body;
            }
            else if (!data.IsHtml)
            {
                builder.TextBody = data.Body;
            }
            else
            {
                builder.HtmlBody = data.Body;
            }

            data.Attachments.ForEach(x => _ = x.ContentType != null
                    ? builder.Attachments.Add(x.Filename, x.Data, ContentType.Parse(x.ContentType))
                    : builder.Attachments.Add(x.Filename, x.Data));

            message.Body = builder.ToMessageBody();

            foreach (var header in data.Headers)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            switch (data.Priority)
            {
                case Priority.Low:
                    message.Priority = MessagePriority.NonUrgent;
                    break;

                case Priority.Normal:
                    message.Priority = MessagePriority.Normal;
                    break;

                case Priority.High:
                    message.Priority = MessagePriority.Urgent;
                    break;
            }

            return message;
        }
    }
}
