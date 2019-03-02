using System;

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
            Id = int.Parse(Core.Core.GetProcessIdOfThread(Handle).ToString());
        }

        public static void Start(string filename)
        {
            Core.Core.ShellExecute(IntPtr.Zero, "open", filename, null, null, 0);
        }

        public static void Start(string filename, bool createNoWindow)
        {
            Core.Core.ShellExecute(IntPtr.Zero, "open", filename, null, null, createNoWindow ? 1 : 0);
        }
    }
}
