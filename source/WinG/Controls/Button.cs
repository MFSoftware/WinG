using System;
using System.Text;

namespace WinG
{
    public class Button : Control
    {
        #region Properties

        public Font Font
        {
            set => Core.Core.SendMessage(Handle, Core.Core.WM.SETFONT, value.Handle.ToInt32(), 0);
        }

        public CursorStyle Cursor
        {
            set
            {
                //Core.Core.SetCursor(Core.Core.LoadCursor(Handle, value));
            }
        }

        public string Text
        {
            set => Core.Core.SetWindowText(Handle, value);
            get
            {
                StringBuilder buff = new StringBuilder(256);
                return Core.Core.GetWindowText(Handle, buff, 256) > 0 ? buff.ToString() : null;
            }
        }

        public Color FontColor
        {
            set => Core.Core.SetTextColor(Handle, (int) value);
            get => (Color) Core.Core.GetTextColor(Handle);
        }

        public Color BackColor
        {
            set => Core.Core.SetBkColor(Handle, (int)value);
            get => (Color)Core.Core.GetTextColor(Handle);
        }

        public Css Css
        {
            set
            {
                //Css.LoadStyleString("");
            }
        }

        #endregion

        public Button(Window win)
        {
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "Button", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.BS_DEFPUSHBUTTON, 1, 1, 36, 29, win.Handle, IntPtr.Zero, win.Handle, IntPtr.Zero);
        }

        public Button()
        {
            if (Settings.Settings.MainWin == IntPtr.Zero)
                Settings.Settings.MainWin = Core.Core.CreateWindowEx2(Core.Core.WindowStylesEx.WS_EX_APPWINDOW, Application.RegisterClass(Core.Core.GenRandomString(5), Core.Core.GenRandomString(5)), string.Empty,Core.Core.WindowStyles.WS_OVERLAPPEDWINDOW,0,0,0,0,IntPtr.Zero,IntPtr.Zero, System.Diagnostics.Process.GetCurrentProcess().Handle,IntPtr.Zero);
            Handle = Core.Core.CreateWindowEx(Core.Core.WindowStylesEx.WS_EX_STATICEDGE, "Button", "", Core.Core.WindowStyles.WS_CHILD | Core.Core.WindowStyles.WS_VISIBLE | Core.Core.WindowStyles.BS_DEFPUSHBUTTON, 1, 1, 36, 29, Settings.Settings.MainWin, IntPtr.Zero, Settings.Settings.MainWin, IntPtr.Zero);
        }
    }
}
