using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public static class MessageBox
    {
        public static MessageBoxResult Show(string text, string caption = "Message", MessageBoxType type = 0)
        {
            return Core.Core.MessageBox(IntPtr.Zero, text, caption, type);
        }
    }
}
