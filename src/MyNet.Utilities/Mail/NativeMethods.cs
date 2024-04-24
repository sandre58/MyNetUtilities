// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace MyNet.Utilities.Mail
{
#pragma warning disable IDE0079 // Unread "private" fields should be removed
    public static partial class NativeMethods
    {
        [DllImport("MAPI32.DLL")]
        private static extern int MAPISendMail(IntPtr sess, IntPtr hwnd, MapiMessage message, int flg, int rsv);

        public static bool SendMail(string addr, string title) => SendMail(addr, title, "", "");

        public static bool SendMail(string addr, string title, string body) => SendMail(addr, title, body, "");

        public static bool SendMail(string addr, string title, string body, string fileName)
        {
            try
            {
                var msg = new MapiMessage
                {
                    recips = GetRecipient(addr),
                    recipCount = 1,
                    subject = title,
                    noteText = body,
                    fileCount = 1
                };

                if (!string.IsNullOrEmpty(fileName))
                {
                    msg.files = GetAttachment(fileName);
                }

                var result = MAPISendMail(IntPtr.Zero, IntPtr.Zero, msg, 0x1 | 0x8, 0);
                return result <= 1 && result >= 0;
            }
            catch
            {
                return false;
            }
        }

        private static IntPtr GetRecipient(string address)
        {
            var recipient = new MapiRecipDesc
            {
                recipClass = 1,
                name = address
            };

            var size = Marshal.SizeOf(typeof(MapiRecipDesc));
            var intPtr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(recipient, intPtr, false);

            return intPtr;
        }

        private static IntPtr GetAttachment(string fileName)
        {
            var size = Marshal.SizeOf(typeof(MapiFileDesc));
            var intPtr = Marshal.AllocHGlobal(size);

            var mapiFileDesc = new MapiFileDesc
            {
                //An integer used to indicate where in the message text to render the attachment.
                position = -1,

                name = System.IO.Path.GetFileName(fileName),
                path = fileName
            };
            Marshal.StructureToPtr(mapiFileDesc, intPtr, false);

            return intPtr;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private sealed class MapiMessage
        {
            public int reserved;

            public string subject = string.Empty;

            public string noteText = string.Empty;
            public string messageType = string.Empty;
            public string dateReceived = string.Empty;
            public string conversationID = string.Empty;
            public int flags;
            public IntPtr originator;

            public int recipCount;

            public IntPtr recips;

            public int fileCount;
            public IntPtr files;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private sealed class MapiRecipDesc
        {
            public int reserved;

            public int recipClass;

            public string name = string.Empty;
            public string address = string.Empty;
            public int eIDSize;
            public IntPtr entryID;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private sealed class MapiFileDesc
        {
            public int reserved;
            public int flags;

            public int position;

            public string path = string.Empty;

            public string name = string.Empty;
            public IntPtr type;
        }
    }
#pragma warning restore IDE0079 // Unread "private" fields should be removed
}
