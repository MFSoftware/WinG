﻿using System;
using System.Diagnostics;

namespace WinG
{
    public class ProgressBar : Control
    {
        public int Progress
        {
            set => Core.Core.SendMessage(this.Handle, WinG.Core.Core.WM.SETPOS, value, 0);
            get => Core.Core.SendMessage(this.Handle, WinG.Core.Core.WM.GETPOS, 0, 0);
        }

        public int Step
        {
            set => Core.Core.SendMessage(this.Handle, WinG.Core.Core.WM.SETSTEP, value, 0);
            get => Core.Core.SendMessage(this.Handle, WinG.Core.Core.WM.GETSTEP, 0, 0);
        }

        public int Range
        {
            set => Core.Core.SendMessage(this.Handle, WinG.Core.Core.WM.SETRANGE, 0, value);
        }

        public ProgressBar(Window win)
        {
            this.Handle = Core.Core.CreateWindowEx(WinG.Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "msctls_progress32", "", WinG.Core.Core.WindowStyles.WS_CHILD | WinG.Core.Core.WindowStyles.WS_VISIBLE, 1, 1, 100, 29, win.Handle, IntPtr.Zero, win.Handle, IntPtr.Zero);
        }

        public ProgressBar()
        {
            if (Settings.Settings.MainWin == IntPtr.Zero)
                Settings.Settings.MainWin = WinG.Core.Core.CreateWindowEx2(WinG.Core.Core.WindowStylesEx.WS_EX_APPWINDOW, Application.RegisterClass(WinG.Core.Core.GenRandomString(5), WinG.Core.Core.GenRandomString(5)), string.Empty, WinG.Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, Process.GetCurrentProcess().Handle, IntPtr.Zero);
            this.Handle = WinG.Core.Core.CreateWindowEx(WinG.Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "msctls_progress32", "", WinG.Core.Core.WindowStyles.WS_CHILD | WinG.Core.Core.WindowStyles.WS_MAXIMIZEBOX | WinG.Core.Core.WindowStyles.WS_VISIBLE, 1, 1, 100, 29, WinG.Settings.Settings.MainWin, IntPtr.Zero, WinG.Settings.Settings.MainWin, IntPtr.Zero);
        }
    }
}
