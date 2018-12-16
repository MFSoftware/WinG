using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinG.IO
{
    public static class File
    {
        public static void Copy(string oldFile, string newFile)
        {
            int pbCancel = 0;
            Core.Core.CopyFileEx(oldFile, newFile, new Core.Core.CopyProgressRoutine(CopyProgressHandler), IntPtr.Zero, ref pbCancel, Core.Core.CopyFileFlags.COPY_FILE_RESTARTABLE);
        }

        public static void Move(string oldFile, string newFile)
        {
            Core.Core.CopyFileEx(oldFile, newFile, new Core.Core.CopyProgressRoutine(CopyProgressHandler), IntPtr.Zero, Core.Core.MoveFileFlags.MOVE_FILE_REPLACE_EXISTSING | Core.Core.MoveFileFlags.MOVE_FILE_WRITE_THROUGH | Core.Core.MoveFileFlags.MOVE_FILE_COPY_ALLOWED);
        }

        public static void Delete(string path)
        {
            Core.Core.DeleteFile(path);
        }

        private static Core.Core.CopyProgressResult CopyProgressHandler(long total, long transferred, long streamSize, long StreamByteTrans, uint dwStreamNumber, Core.Core.CopyProgressCallbackReason reason, IntPtr hSourceFile, IntPtr hDestinationFile, IntPtr lpData)
        {
            return Core.Core.CopyProgressResult.PROGRESS_CONTINUE;
        }
    }
}
