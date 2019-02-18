using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace WinG
{
    public static class Environment
    {
        public static string GetCurrentDirectory()
        {
            uint nBufferLength = Core.Core.GetCurrentDirectory(0, null);
            var lpBuffer = new StringBuilder((int)nBufferLength);

            if (Core.Core.GetCurrentDirectory(nBufferLength, lpBuffer) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            return lpBuffer.ToString();
        }
    }
}
