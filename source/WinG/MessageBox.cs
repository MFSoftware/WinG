using System;

namespace WinG
{
    public static class MessageBox
    {
        public static MessageBoxResult Show(string text, string caption = "Message", MessageBoxType type = 0)
        {
            return Core.Core.MessageBox(IntPtr.Zero, text, caption, type);
        }

        public static MessageBoxResult Show(int text, string caption = "Message", MessageBoxType type = 0)
        {
            return Core.Core.MessageBox(IntPtr.Zero, text.ToString(), caption, type);
        }
    }
}
