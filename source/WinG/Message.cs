using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG
{
    public static class Message
    {
        public static void Beep(MessageType type)
        {
            Core.Core.MessageBeep(type);
        }
    }
}
