// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Mail.Models
{
    public class EmailAddress
    {
        public string Name { get; } = string.Empty;

        public string Address { get; } = string.Empty;

        public EmailAddress() { }

        public EmailAddress(string emailAddress, string name = "") => (Name, Address) = (name, emailAddress);
    }
}
