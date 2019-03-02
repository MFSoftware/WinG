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
        
        public void LoadFromSystemIcon(SystemIcons id)
        {
            Handle = Core.Core.LoadIcon(IntPtr.Zero, (int) id);
        }

        public static Icon FromFile(string filename)
        {
            var ico = new Icon {Handle = Core.Core.ExtractIcon(IntPtr.Zero, filename, 0)};
            return ico;
        }

        public static Icon FromSystemIcon(SystemIcons id)
        {
            var ico = new Icon {Handle = Core.Core.LoadIcon(IntPtr.Zero, (int) id)};
            return ico;
        }
    }
}
