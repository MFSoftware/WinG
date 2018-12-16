using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG.Diagnostics
{
    public class Process
    {
        public IntPtr Handle;
        public string FileName;
        public bool CreateNoWindow;
        public int Id;

        public void Start()
        {
            Handle = Core.Core.ShellExecute(IntPtr.Zero, "open", FileName, null, null, CreateNoWindow ? 1 : 0);
            Id = Int32.Parse(Core.Core.GetProcessIdOfThread(Handle).ToString());
        }

        public static void Start(string Filename)
        {
            Core.Core.ShellExecute(IntPtr.Zero, "open", Filename, null, null, 0);
        }

        public static void Start(string Filename, bool CreateNoWindow)
        {
            Core.Core.ShellExecute(IntPtr.Zero, "open", Filename, null, null, CreateNoWindow ? 1 : 0);
        }
    }
}
