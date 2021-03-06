﻿using System;
using System.Text;

namespace WinG
{
    public class Label : Control
    {
        #region Properties

        public string Text
        {
            set => Core.Core.SetWindowText(Handle, value);
            get
            {
                var buff = new StringBuilder(256);
                return Core.Core.GetWindowText(Handle, buff, 256) > 0 ? buff.ToString() : null;
            }
        }

        public Font Font
        {
            set => Core.Core.SendMessage(Handle, Core.Core.WM.SETFONT, value.Handle.ToInt32(), 0);
        }

        #endregion

        public Label(Window win)
        {
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_LEFT, "Static", "", Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.SS_LEFT, 1, 1, 36, 29, win.Handle, IntPtr.Zero, win.Handle, IntPtr.Zero);
        }

        public Label(string text)
        {
            if (Settings.Settings.MainWin == IntPtr.Zero)
                Settings.Settings.MainWin = Core.Core.CreateWindowEx2(Core.Core.WindowStylesEx.WS_EX_APPWINDOW, Application.RegisterClass(Core.Core.GenRandomString(5), Core.Core.GenRandomString(5)), string.Empty, Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, System.Diagnostics.Process.GetCurrentProcess().Handle, IntPtr.Zero);
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_LEFT, "Static", "", Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.SS_LEFT, 1, 1, 36, 29, Settings.Settings.MainWin, IntPtr.Zero, Settings.Settings.MainWin, IntPtr.Zero);

            Text = text;
        }

        public Label()
        {
            if (Settings.Settings.MainWin == IntPtr.Zero)
                Settings.Settings.MainWin = Core.Core.CreateWindowEx2(Core.Core.WindowStylesEx.WS_EX_APPWINDOW, Application.RegisterClass(Core.Core.GenRandomString(5), Core.Core.GenRandomString(5)), string.Empty, Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, System.Diagnostics.Process.GetCurrentProcess().Handle, IntPtr.Zero);
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_LEFT, "Static", "", Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.SS_LEFT, 1, 1, 36, 29, Settings.Settings.MainWin, IntPtr.Zero, Settings.Settings.MainWin, IntPtr.Zero);
        }
    }
}
