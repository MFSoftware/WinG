using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinG.Drawing;

namespace WinG
{
    public class Window
    {
        #region Properties

        public IntPtr Handle = IntPtr.Zero;

        public WindowState WindowState
        {
            get
            {
                return (WindowState) Core.Core.GetClassLong(Handle, -26);
            }
            set
            {
                Core.Core.SetClassLongA(Handle.ToInt32(), -26, (int) value);
            }
        }

        public CursorStyle Cursor
        {
            get
            {
                return (CursorStyle) Core.Core.GetClassLong(Handle, -12);
            }
            set
            {
                //Core.Core.SetClassLongA(Handle.ToInt32(), -12, Core.Core.LoadCursor(Handle, value).ToInt32());
                //Core.Core.SetWindowLong(Handle, -12, Core.Core.LoadCursor(Handle, value).ToInt32());
            }
        }

        public WindowColor BackColor
        {
            get
            {
                return (WindowColor) Core.Core.GetClassLong(Handle, -10);
            }
            set
            {
                Core.Core.SetClassLongA(Handle.ToInt32(), -10, (int) value);
            }
        }

        public string Icon
        {
            set
            {
                Core.Core.SetClassLongA(Handle.ToInt32(), -14, Core.Core.ExtractIcon(Handle, value, 0).ToInt32());
            }
        }

        public string Text
        {
            set
            {
                Core.Core.SetWindowText(Handle, value);
            }
            get
            {
                StringBuilder Buff = new StringBuilder(256);
                if (Core.Core.GetClassName(Handle, Buff, 256) > 0)
                    return Buff.ToString();

                return null;
            }
        }

        public int Width
        {
            set
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.SetWindowPos(Handle, 0, 0, 0, value, r.Height, 1);
            }
            get
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                return r.Width;
            }
        }

        public int Height
        {
            set
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.SetWindowPos(Handle, 0, 0, 0, r.Width, value, 1);
            }
            get
            {
                Rect r = new Rect();
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
                Application.RegisterClass(Core.Core.GenRandomString(5), Core.Core.GenRandomString(5)),
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
            Rect r = new Rect();
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
