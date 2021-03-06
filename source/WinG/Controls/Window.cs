﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WinG.Drawing;

namespace WinG
{
    public class Window
    {
        #region Properties

        public IntPtr Handle { get; set; }

        public Css Css
        {
            set
            {
                foreach (var line in value.Text.Split('\n'))
                {
                    Core.Core.ParseCssLine(line, Handle);
                }
            }
        }

        public int Transparency
        {
            set
            {
                Core.Core.SetWindowLong(Handle, -20, WinG.Core.Core.GetWindowLong(this.Handle, -20) | 524288);
                Core.Core.SetLayeredWindowAttributes(Handle, 0U, Convert.ToByte(byte.MaxValue * value / 100), 2U);
            }
            get => 0;
        }

        public Control[] Controls
        {
            get
            {
                var ctrls = new List<Control>();

                foreach (var handle in Core.Core.GetChildWindows(Handle))
                {
                    var c = new Control();
                    c.Handle = handle;
                    ctrls.Add(c);
                }
                return ctrls.ToArray();
            }
        }

        public WindowState WindowState
        {
            get => (WindowState) Core.Core.GetClassLong(Handle, -26);
            set => Core.Core.SetClassLongA(Handle.ToInt32(), -26, (int) value);
        }

        public CursorStyle Cursor
        {
            get => (CursorStyle) Core.Core.GetClassLong(Handle, -12);
            set
            {
                //Core.Core.SetClassLongA(Handle.ToInt32(), -12, Core.Core.LoadCursor(Handle, value).ToInt32());
                //Core.Core.SetWindowLong(Handle, -12, Core.Core.LoadCursor(Handle, value).ToInt32());
            }
        }

        public Color BackColor
        {
            get => (Color) Core.Core.GetClassLong(Handle, -10);
            set => Core.Core.SetClassLongA(Handle.ToInt32(), -10, (int) value);
        }

        public Icon Icon
        {
            set => Core.Core.SetClassLongA(Handle.ToInt32(), -14, value.Handle.ToInt32());
        }

        public string Text
        {
            set => Core.Core.SetWindowText(Handle, value);
            get
            {
                var buff = new StringBuilder(256);
                return Core.Core.GetWindowText(Handle, buff, 256) > 0 ? buff.ToString() : null;
            }
        }

        public int BorderRadius
        {
            set => Core.Core.SetWindowRgn(Handle, Core.Core.CreateRoundRectRgn(0, 0, Width, Height, value, value), true);
        }

        public int Width
        {
            set
            {
                var r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.SetWindowPos(Handle, 0, 0, 0, value, r.Height, 1);
            }
            get
            {
                var r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                return r.Width;
            }
        }

        public int Height
        {
            set
            {
                var r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.SetWindowPos(Handle, 0, 0, 0, r.Width, value, 1);
            }
            get
            {
                var r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                return r.Height;
            }
        }

        #endregion

        public Window(UInt16 atom)
        {
            Handle = Core.Core.CreateWindowEx2(
                Core.Core.WindowStylesEx.WS_EX_APPWINDOW,
                atom,
                string.Empty,
                Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW,
                0,
                0,
                400,
                300,
                IntPtr.Zero,
                IntPtr.Zero,
                Process.GetCurrentProcess().Handle,
                IntPtr.Zero);
        }

        public Window()
        {
            Handle = Core.Core.CreateWindowEx2(
                Core.Core.WindowStylesEx.WS_EX_APPWINDOW,
                Application.RegisterClass("Window", "Window"),
                string.Empty,
                Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW,
                0,
                0,
                400,
                300,
                IntPtr.Zero,
                IntPtr.Zero,
                Process.GetCurrentProcess().Handle,
                IntPtr.Zero);
        }

        public void Repaint()
        {
            var r = new Rect();
            Core.Core.GetWindowRect(Handle, ref r);
            Core.Core.MoveWindow(Handle, r.X, r.Y, r.Width, r.Height, true);
        }

        public void Show()
        {
            Core.Core.ShowWindow(Handle, WindowState);
            Core.Core.UpdateWindow(Handle);
        }

        public void Hide()
        {
            Core.Core.ShowWindow(Handle, WindowState.Hide);
        }

        public void Close()
        {
            Core.Core.DestroyWindow(Handle);
        }

        public void Free()
        {
            Core.Core.DestroyWindow(Handle);
        }

        public void Add(Control ctrl)
        {
            Core.Core.SetParent(ctrl.Handle, Handle);
        }
    }
}
