using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public static class Environment
    {
        public static string GetCurrentDirectory()
        {
            uint nBufferLength = Core.Core.GetCurrentDirectory(0, null);
            var lpBuffer = new StringBuilder((int)nBufferLength);

            uint result = Core.Core.GetCurrentDirectory(nBufferLength, lpBuffer);
            if (result == 0)
            {
                //WinError.ThrowLastWin32Error();
            }

            return lpBuffer.ToString();
        }
    }
}
