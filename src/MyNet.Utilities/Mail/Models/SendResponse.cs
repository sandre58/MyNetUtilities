// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.Mail.Models
{
    public class SendResponse
    {
        public string MessageId { get; set; } = "";

        public List<string> ErrorMessages { get; } = [];
        public bool Successful => ErrorMessages.Count == 0;

        public SendResponse() { }
    }
}
