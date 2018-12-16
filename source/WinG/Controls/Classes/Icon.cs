using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public class Icon
    {
        public IntPtr Handle;

        public void LoadFromFile(string filename)
        {
            Handle = Core.Core.ExtractIcon(IntPtr.Zero, filename, 0);
        }

        public static Icon FromFile(string filename)
        {
            Icon ico = new Icon();
            ico.Handle = Core.Core.ExtractIcon(IntPtr.Zero, filename, 0);
            return ico;
        }

        public void LoadFromSystemIcons(SystemIcons id)
        {
            Handle = Core.Core.LoadIcon(IntPtr.Zero, (int) id);
        }
    }
}
