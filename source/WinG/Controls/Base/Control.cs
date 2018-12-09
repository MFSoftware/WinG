﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinG.Drawing;

namespace WinG
{
    public class Control
    {
        public IntPtr Handle = IntPtr.Zero;

        public Font Font
        {
            set
            {
                Core.Core.SendMessage(Handle, Core.Core.WM.SETFONT, value.Handle.ToInt32(), 0);
            }
        }

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
            get
            {
                return Core.Core.IsWindowVisible(Handle);
            }
        }

        public string Name
        {
            get
            {
                StringBuilder Buff = new StringBuilder(256);
                if (Core.Core.GetClassName(Handle, Buff, 256) > 0)
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

        public IntPtr Parent
        {
            set
            {
                Core.Core.SetParent(Handle, value);
            }
            get
            {
                return Core.Core.GetParent(Handle);
            }
        }

        public string Text
        {
            set
            {
                // Core.Core.SendMessage(Handle, 0x000C, 0, "");
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
                Core.Core.SetWindowPos(Handle, 0, value, r.Y, 0, 0, Core.Core.SWP.NOSIZE);
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
                Core.Core.SetWindowPos(Handle, 0, r.X, value, 0, 0, Core.Core.SWP.NOSIZE);
            }
        }

        public void Free()
        {
            Core.Core.DestroyWindow(Handle);
        }
    }
}
