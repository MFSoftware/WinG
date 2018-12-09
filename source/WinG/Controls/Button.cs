using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinG.Settings;

namespace WinG
{
    public class Button : Control
    {
        #region Properties

        

        #endregion

        public Button(Window win)
        {
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "Button", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.BS_DEFPUSHBUTTON, 1, 1, 36, 29, win.Handle, IntPtr.Zero, win.Handle, IntPtr.Zero);
        }

        public Button()
        {
            if (Settings.Settings.MainWin == IntPtr.Zero)
                Settings.Settings.MainWin = Core.Core.CreateWindowEx2(Core.Core.WindowStylesEx.WS_EX_APPWINDOW, Application.RegisterClass(Core.Core.GenRandomString(5), Core.Core.GenRandomString(5)), string.Empty,Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW,0,0,0,0,IntPtr.Zero,IntPtr.Zero,Process.GetCurrentProcess().Handle,IntPtr.Zero);
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "Button", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.BS_DEFPUSHBUTTON, 1, 1, 36, 29, Settings.Settings.MainWin, IntPtr.Zero, Settings.Settings.MainWin, IntPtr.Zero);
        }
    }
}
