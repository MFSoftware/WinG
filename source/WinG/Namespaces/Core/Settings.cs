using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG.Settings
{
    public class Settings
    {
        public static IntPtr MainWin;
        public static int LastId = 1;

        public static int RegControl()
        {
            LastId++;
            return LastId;
        }
    }
}
