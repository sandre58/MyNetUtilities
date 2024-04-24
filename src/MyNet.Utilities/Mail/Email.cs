// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyNet.Utilities.Mail.Models;

namespace MyNet.Utilities.Mail
{
    public class Email(string fromAddress, string displayNameFrom = "") : IEmail
    {
        public EmailData Data { get; } = new EmailData(new EmailAddress(fromAddress, displayNameFrom));

        /// <summary>
        /// Creates a new Email instance and sets the from property
        /// </summary>
        /// <param name="emailAddress">Email address to send from</param>
        /// <param name="name">Name to send from</param>
        /// <returns>Instance of the Email class</returns>
        public static IEmail From(string emailAddress, string name = "") => new Email(emailAddress, name);


        /// <summary>
        /// Set the send from email address
        /// </summary>
        /// <param name="emailAddress">Email address of sender</param>
        /// <param name="name">Name of sender</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail SetFrom(string emailAddress, string name = "")
        {
            Data.From = new EmailAddress(emailAddress, name);
            return this;
        }

        /// <summary>
        /// Adds a reciepient to the email, Splits name and address on ';'
        /// </summary>
        /// <returns>Instance of the Email class</returns>
        private Email AddAdresses(List<EmailAddress> destination, string emailAddress, string name)
        {
            if (emailAddress.Contains(';'))
            {
                //email address has semi-colon, try split
                var nameSplit = name?.Split(';') ?? [];
                var addressSplit = emailAddress.Split(';');
                for (var i = 0; i < addressSplit.Length; i++)
                {
                    var currentName = string.Empty;
                    if (nameSplit.Length - 1 >= i)
                        currentName = nameSplit[i];

                    destination.Add(new EmailAddress(addressSplit[i].Trim(), currentName.Trim()));
                }
            }
            else
                destination.Add(new EmailAddress(emailAddress.Trim(), name.Trim()));
            return this;
        }

        /// <summary>
        /// Adds all reciepients in list to email
        /// </summary>
        /// <returns>Instance of the Email class</returns>
        private Email AddAdresses(List<EmailAddress> adresses, IList<EmailAddress> mailAddresses)
        {
            foreach (var address in mailAddresses)
                adresses.Add(address);

            return this;
        }

        /// <summary>
        /// Adds a reciepient to the email, Splits name and address on ';'
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient</param>
        /// <param name="name">Name of recipient</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail To(string emailAddress, string name = "") => AddAdresses(Data.To, emailAddress, name);

        /// <summary>
        /// Adds all reciepients in list to email
        /// </summary>
        /// <param name="mailAddresses">List of recipients</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail To(IList<EmailAddress> mailAddresses) => AddAdresses(Data.To, mailAddresses);

        /// <summary>
        /// Adds a Carbon Copy to the email
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient</param>
        /// <param name="name">Name of recipient</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Cc(string emailAddress, string name = "") => AddAdresses(Data.Cc, emailAddress, name);

        /// <summary>
        /// Adds a Carbon Copy to the email
        /// </summary>
        /// <param name="mailAddresses">List of recipients</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Cc(IList<EmailAddress> mailAddresses) => AddAdresses(Data.Cc, mailAddresses);

        /// <summary>
        /// Adds a blind carbon copy to the email
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient</param>
        /// <param name="name">Name of recipient</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Bcc(string emailAddress, string name = "") => AddAdresses(Data.Bcc, emailAddress, name);

        /// <summary>
        /// Adds a blind carbon copy to the email
        /// </summary>
        /// <param name="mailAddresses">List of recipients</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Bcc(IList<EmailAddress> mailAddresses) => AddAdresses(Data.Bcc, mailAddresses);

        /// <summary>
        /// Sets the ReplyTo address on the email
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient</param>
        /// <param name="name">Name of recipient</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail ReplyTo(string emailAddress, string name = "") => AddAdresses(Data.ReplyTo, emailAddress, name);

        /// <summary>
        /// Sets the ReplyTo address on the email
        /// </summary>
        /// <param name="mailAddresses">List of recipients</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail ReplyTo(IList<EmailAddress> mailAddresses) => AddAdresses(Data.ReplyTo, mailAddresses);

        /// <summary>
        /// Sets the subject of the email
        /// </summary>
        /// <param name="subject">email subject</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Subject(string subject)
        {
            Data.Subject = subject;
            return this;
        }

        /// <summary>
        /// Adds a Body to the Email
        /// </summary>
        /// <param name="body">The content of the body</param>
        /// <param name="isHtml">True if Body is HTML, false for plain text (default)</param>
        public IEmail Body(string body, bool isHtml = false)
        {
            Data.IsHtml = isHtml;
            Data.Body = body;
            return this;
        }

        /// <summary>
        /// Adds a Plaintext alternative Body to the Email. Used in conjunction with an HTML email,
        /// this allows for email readers without html capability, and also helps avoid spam filters.
        /// </summary>
        /// <param name="body">The content of the body</param>
        public IEmail PlaintextAlternativeBody(string body)
        {
            Data.PlaintextAlternativeBody = body;
            return this;
        }

        /// <summary>
        /// Marks the email as High Priority
        /// </summary>
        public IEmail HighPriority()
        {
            Data.Priority = Priority.High;
            return this;
        }

        /// <summary>
        /// Marks the email as Low Priority
        /// </summary>
        public IEmail LowPriority()
        {
            Data.Priority = Priority.Low;
            return this;
        }

        /// <summary>
        /// Adds an Attachment to the Email
        /// </summary>
        /// <param name="attachment">The Attachment to add</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Attach(Attachment attachment)
        {
            if (!Data.Attachments.Contains(attachment))
                Data.Attachments.Add(attachment);

            return this;
        }

        /// <summary>
        /// Adds Multiple Attachments to the Email
        /// </summary>
        /// <param name="attachments">The List of Attachments to add</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Attach(IList<Attachment> attachments)
        {
            foreach (var attachment in attachments.Where(attachment => !Data.Attachments.Contains(attachment)))
                Data.Attachments.Add(attachment);

            return this;
        }

        public IEmail AttachFromFilename(string filename, string contentType = "", string attachmentName = "")
        {
            var stream = File.OpenRead(filename);
            _ = Attach(new Attachment
            {
                Data = stream,
                Filename = attachmentName ?? filename,
                ContentType = contentType
            });

            return this;
        }

        /// <summary>
        /// Adds tag to the Email. This is currently only supported by the Mailgun provider. <see href="https://documentation.mailgun.com/en/latest/user_manual.html#tagging"/>
        /// </summary>
        /// <param name="tag">Tag name, max 128 characters, ASCII only</param>
        /// <returns>Instance of the Email class</returns>
        public IEmail Tag(string tag)
        {
            Data.Tags.Add(tag);

            return this;
        }

        public IEmail Header(string header, string body)
        {
            Data.Headers.Add(header, body);

            return this;
        }

        public override string? ToString() => $"From {Data.From.Address} To {string.Join(", ", Data.To.Select(x => x.Address))} - Subject : {Data.Subject}";

    }
}
