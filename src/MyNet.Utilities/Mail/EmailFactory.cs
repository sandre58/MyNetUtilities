// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Mail
{
    public class EmailFactory(string from, string displayName = "") : IEmailFactory
    {
        private readonly string _from = from;
        private readonly string _displayName = displayName;

        public IEmail Create() => Email.From(_from, _displayName);
    }
}
