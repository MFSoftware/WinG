using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinG.OsInfo
{
    public class OsInfo
    {
        public string ComputerName;
        public string Product;

        [DllImport("ntdll.dll")]
        public static extern void RtlGetVersion(ref Core.Core.OSVERSIONINFOEX version);

        [DllImport("kernel32.dll")]
        public static extern void GetVersionEx(ref Core.Core.OSVERSIONINFOEX v);

        public OsInfo()
        {
            var name = new StringBuilder(260);
            uint size = 260;
            Core.Core.GetComputerNameEx(Core.Core.COMPUTER_NAME_FORMAT.ComputerNameDnsHostname, name, ref size);
            ComputerName = name.ToString();

            var osvi = new Core.Core.OSVERSIONINFOEX();
            GetVersionEx(ref osvi);
            Product = osvi.wProductType.ToString();
        }
    }
}
