// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.IO;

namespace MyNet.Utilities.Mail.Models
{
    public class Attachment
    {
        /// <summary>
        /// Gets or sets whether the attachment is intended to be used for inline images (changes the paramater name for providers such as MailGun)
        /// </summary>
        public bool IsInline { get; set; }
        public string Filename { get; set; } = string.Empty;
        public Stream? Data { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string ContentId { get; set; } = string.Empty;
    }
}
