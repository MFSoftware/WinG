using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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

        public Control[] Controls
        {
            get
            {
                IntPtr[] child = new IntPtr[15];
                List<Control> forRet = new List<Control>();
                child[0] = Core.Core.GetWindow(Handle, 5);

                for (int i = 1; i <= 5; i++)
                {
                    Control c = new Control();
                    c.Handle = Core.Core.GetWindow(child[i - 1], 2);
                    forRet.Add(c);
                }
                return forRet.ToArray();
            }
        }

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

        public Color BackColor
        {
            get
            {
                return (Color) Core.Core.GetClassLong(Handle, -10);
            }
            set
            {
                Core.Core.SetClassLongA(Handle.ToInt32(), -10, (int) value);
            }
        }

        public Icon Icon
        {
            set
            {
                Core.Core.SetClassLongA(Handle.ToInt32(), -14, value.Handle.ToInt32());
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
                System.Diagnostics.Process.GetCurrentProcess().Handle,
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
                System.Diagnostics.Process.GetCurrentProcess().Handle,
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

        public void ToBack()
        {
            Core.Core.SetForegroundWindow(Handle);
        }
    }
}
