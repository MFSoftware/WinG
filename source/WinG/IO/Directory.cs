using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace WinG.IO
{
    public static class Directory
    {
        public static void Create(string name)
        {
            Core.Core.CreateDirectory(name);
        }

        public static void Remove(string name)
        {
            Core.Core.RemoveDirectory(name);
        }
    }
}
