using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public static class Desktop
    {
        public static void HideTaskBar()
        {
            var hWndDesktop = Core.Core.GetDesktopWindow();
            var hWndStartButton = Core.Core.FindWindowEx(hWndDesktop, IntPtr.Zero, "button", string.Empty);
            var hWndTaskBar = Core.Core.FindWindowEx(hWndDesktop, IntPtr.Zero, "Shell_TrayWnd", string.Empty);
            Core.Core.SetWindowPos(hWndStartButton, 0, 0, 0, 0, 0, 0x0080);
            Core.Core.SetWindowPos(hWndTaskBar, 0, 0, 0, 0, 0, 0x0080);
        }

        public static void ShowTaskBar()
        {
            var hWndDesktop = Core.Core.GetDesktopWindow();
            var hWndStartButton = Core.Core.FindWindowEx(hWndDesktop, IntPtr.Zero, "button", string.Empty);
            var hWndTaskBar = Core.Core.FindWindowEx(hWndDesktop, IntPtr.Zero, "Shell_TrayWnd", string.Empty);
            Core.Core.SetWindowPos(hWndStartButton, 0, 0, 0, 0, 0, 0x0040);
            Core.Core.SetWindowPos(hWndTaskBar, 0, 0, 0, 0, 0, 0x0040);
        }
    }
}
