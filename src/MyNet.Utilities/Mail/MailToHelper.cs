// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using MyNet.Utilities.Helpers;

namespace MyNet.Utilities.Mail
{
    public static class MailToHelper
    {
        public static bool SendMail(string address, string title, string body) => SendMail(new List<string> { address }, title, body);

        public static bool SendMail(IEnumerable<string> adresses, string title, string body)
        {
            var values = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(title))
            {
                values.Add("subject", title);
            }

            if (!string.IsNullOrEmpty(body))
            {
                values.Add("body", body.Replace("\r\n", "%0d%0a").Replace("\n", "%0d%0a"));
            }

            var command = $"mailto:{string.Join(";", adresses)}";

            if (values.Count != 0)
            {
                command += $"?{string.Join("&", values.Select(x => $"{x.Key}={x.Value}"))}";
            }

            ProcessHelper.Start(Uri.EscapeUriString(command));
            return true;
        }
    }
}
