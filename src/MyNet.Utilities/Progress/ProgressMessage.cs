// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Progress
{
    public class ProgressMessage
    {
        public string Message { get; }

        public object[] Parameters { get; }

        public ProgressMessage(string message, params object[] parameters) => (Message, Parameters) = (message, parameters);
    }
}
