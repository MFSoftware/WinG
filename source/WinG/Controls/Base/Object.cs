using System;
using System.Text;

namespace WinG
{
    public class Object
    {
        public IntPtr Handle { get; set; }
        public int Id;

        public string Class
        {
            get
            {
                var buff = new StringBuilder(256);
                return Core.Core.GetClassName(Handle, buff, 256) > 0 ? buff.ToString() : null;
            }
        }

        public string Name => string.Empty;
    }
}
