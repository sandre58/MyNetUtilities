// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Linq;

namespace MyNet.Utilities.Helpers
{
    public static class ProcessHelper
    {
        public static void Start(string uri) => Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });

        public static void Open(string exe, params string[] args)
        {
            var ps = new ProcessStartInfo
            {
                FileName = exe,
                UseShellExecute = true
            };

            args?.ToList().ForEach(ps.ArgumentList.Add);

            _ = Process.Start(ps);
        }

        public static void OpenInExcel(string filename) => Open("EXCEL.EXE", filename);

        public static void OpenFolder(string folder) => Open("explorer.exe", folder);
    }
}
