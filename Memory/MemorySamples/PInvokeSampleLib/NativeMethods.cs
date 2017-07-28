using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace PInvokeSampleLib
{
    [SecurityCritical]
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true,
          EntryPoint = "CreateHardLinkW", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CreateHardLink(
          [In, MarshalAs(UnmanagedType.LPWStr)] string newFileName,
          [In, MarshalAs(UnmanagedType.LPWStr)] string existingFileName,
          IntPtr securityAttributes);

        internal static void CreateHardLink(string oldFileName,
                                            string newFileName)
        {
            if (!CreateHardLink(newFileName, oldFileName, IntPtr.Zero))
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new IOException($"Error {errorCode}");
            }
        }
    }
}
