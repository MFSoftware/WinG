using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public static class Clipboard
    {
        public static void Empty()
        {
            Core.Core.EmptyClipboard();
        }

        public static void SetText(string txt)
        {
            
            Core.Core.SetDataObject(txt, true);
        }
    }
}
