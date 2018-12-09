using System;
using System.Collections.Generic;
using System.Linq;
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

        public void LoadFromSystemIcons(SystemIcons id)
        {
            Handle = Core.Core.LoadIcon(IntPtr.Zero, (int) id);
        }
    }
}
