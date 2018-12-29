using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public class Memo : Control
    {
        #region Properties

        public string Text
        {
            set
            {
                Core.Core.SetWindowText(Handle, value);
            }
            get
            {
                StringBuilder Buff = new StringBuilder(256);
                if (Core.Core.GetWindowText(Handle, Buff, 256) > 0)
                    return Buff.ToString();

                return null;
            }
        }

        public bool Enabled
        {
            set
            {
                Core.Core.EnableWindow(Handle, value);
            }
            get
            {
                return Core.Core.IsWindowEnabled(Handle);
            }
        }

        #endregion

        public Memo(Window win)
        {
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "Edit", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE, 1, 1, 36, 29, win.Handle, IntPtr.Zero, win.Handle, IntPtr.Zero);
        }

        public Memo()
        {
            if (Settings.Settings.MainWin == IntPtr.Zero)
                Settings.Settings.MainWin = Core.Core.CreateWindowEx2(Core.Core.WindowStylesEx.WS_EX_APPWINDOW, Application.RegisterClass(Core.Core.GenRandomString(5), Core.Core.GenRandomString(5)), string.Empty, Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, System.Diagnostics.Process.GetCurrentProcess().Handle, IntPtr.Zero);
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "Edit", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.ES_MULTILINE, 1, 1, 36, 29, Settings.Settings.MainWin, IntPtr.Zero, Settings.Settings.MainWin, IntPtr.Zero);
        }
    }
}
