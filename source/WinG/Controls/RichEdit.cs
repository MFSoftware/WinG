using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public class RichEdit : Control
    {
        #region Properties

        #endregion

        public RichEdit(Window win)
        {
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_CLIENTEDGE, "RICHEDIT50W", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE, 1, 1, 200, 300, win.Handle, IntPtr.Zero, win.Handle, IntPtr.Zero);
        }

        public RichEdit()
        {
            if (Settings.Settings.MainWin == IntPtr.Zero)
                Settings.Settings.MainWin = Core.Core.CreateWindowEx2(Core.Core.WindowStylesEx.WS_EX_APPWINDOW, Application.RegisterClass(Core.Core.GenRandomString(5), Core.Core.GenRandomString(5)), string.Empty, Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, System.Diagnostics.Process.GetCurrentProcess().Handle, IntPtr.Zero);
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_CLIENTEDGE, "RICHEDIT50W", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.WS_BORDER | Core.Core.WindowStyles.ES_MULTILINE | Core.Core.WindowStyles.WS_TABSTOP, 1, 1, 200, 300, Settings.Settings.MainWin, IntPtr.Zero, Settings.Settings.MainWin, IntPtr.Zero);
        }
    }
}
