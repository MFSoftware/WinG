using System;
using WinG.Drawing;

namespace WinG
{
    public class Control : Object
    {
        public int Width
        {
            set
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.SetWindowPos(Handle, 0, 0, 0, value, r.Height, Core.Core.SWP.NOMOVE);
            }
            get
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                return r.Width;
            }
        }

        public bool Enabled
        {
            set => Core.Core.EnableWindow(Handle, value);
            get => Core.Core.IsWindowEnabled(Handle);
        }

        public int Height
        {
            set
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.SetWindowPos(Handle, 0, 0, 0, r.Width, value, Core.Core.SWP.NOMOVE);
            }
            get
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                return r.Height;
            }
        }

        public bool Visible
        {
            set
            {
                switch (value)
                {
                    case true:
                        Core.Core.ShowWindow(Handle, WindowState.Show);
                        break;
                    case false:
                        Core.Core.ShowWindow(Handle, WindowState.Hide);
                        break;
                }
            }
            get => Core.Core.IsWindowVisible(Handle);
        }

        public IntPtr Parent
        {
            set => Core.Core.SetParent(Handle, value);
            get => Core.Core.GetParent(Handle);
        }

        public int Left
        {
            get
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                return r.X;
            }
            set
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.MoveWindow(Handle, value, r.Y, r.Width, r.Height, false);
            }
        }

        public int Top
        {
            get
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                return r.Y;
            }
            set
            {
                Rect r = new Rect();
                Core.Core.GetWindowRect(Handle, ref r);
                Core.Core.MoveWindow(Handle, r.X, value, r.Width, r.Height, false);
            }
        }

        public void Free()
        {
            Core.Core.DestroyWindow(Handle);
            Handle = IntPtr.Zero;
        }

        public void LoadFromName(string name)
        {
            Handle = Core.Core.FindWindowByCaption(IntPtr.Zero, name);
        }
    }
}
