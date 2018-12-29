using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinG.Drawing;

namespace WinG
{
    public class Object
    {
        public IntPtr Handle = IntPtr.Zero;
        public int Id;

        public string Class
        {
            get
            {
                StringBuilder Buff = new StringBuilder(256);
                if (Core.Core.GetClassName(Handle, Buff, 256) > 0)
                    return Buff.ToString();
                return null;
            }
        }

        public string Name
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
