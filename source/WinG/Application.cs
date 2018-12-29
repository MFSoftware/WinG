﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinG.Drawing;

namespace WinG
{
    public static class Application
    {
        public static void ShowConsole(bool value)
        {
            switch (value)
            {
                case true:
                    Core.Core.ShowWindow(Core.Core.GetConsoleWindow(), WindowState.Show);
                    break;
                case false:
                    Core.Core.ShowWindow(Core.Core.GetConsoleWindow(), WindowState.Hide);
                    break;
            }
        }

        public static void Run()
        {
            Core.Core.InitCommonControls();
            Core.Core.MSG msg = new Core.Core.MSG();
            sbyte hasMessage;

            /*msg.message = ~ Convert.ToUInt32(0x0012);

            while (msg.message != 0x0012 && (hasMessage = Core.Core.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0 && hasMessage != -1)
            {
                if (Core.Core.PeekMessage(ref msg, 0, 0, 0, 0x0001) && !Core.Core.TranslateAccelerator(msg.hwnd, Core.Core.LoadAcceleratorsW(System.Diagnostics.Process.GetCurrentProcess().Handle, 109), ref msg))
                {
                    Core.Core.TranslateMessage(ref msg);
                    Core.Core.DispatchMessage(ref msg);
                }
            }*/

            while ((hasMessage = Core.Core.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0 && hasMessage != -1)
            {
                if (!Core.Core.TranslateAccelerator(msg.hwnd, Core.Core.LoadAcceleratorsW(System.Diagnostics.Process.GetCurrentProcess().Handle, 109), ref msg))
                {
                    Core.Core.TranslateMessage(ref msg);
                    Core.Core.DispatchMessage(ref msg);
                }
            }


        }

        private static IntPtr MainWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            Core.Core.PAINTSTRUCT ps;
            IntPtr hdc;
            Rect rect;
            switch ((Core.Core.WM)msg)
            {
                case Core.Core.WM.PAINT:
                    hdc = Core.Core.BeginPaint(hWnd, out ps);
                    Core.Core.GetClientRect(hWnd, out rect);
                    Core.Core.EndPaint(hWnd, ref ps);
                    return IntPtr.Zero;
                case Core.Core.WM.DESTROY:
                    Core.Core.PostQuitMessage(0);
                    return IntPtr.Zero;
            }
            return Core.Core.DefWindowProc(hWnd, (Core.Core.WM)msg, wParam, lParam);
        }

        public static UInt16 RegisterClass(string menu, string className)
        {
            Core.Core.WNDCLASSEX wcx = new Core.Core.WNDCLASSEX();
            wcx.cbSize = Marshal.SizeOf(wcx);
            wcx.style = (int)(Core.Core.ClassStyles.VerticalRedraw | Core.Core.ClassStyles.HorizontalRedraw);
            wcx.lpfnWndProc = Marshal.GetFunctionPointerForDelegate((Delegate)(Core.Core.WndProc)MainWndProc);
            wcx.cbClsExtra = 0;
            wcx.cbWndExtra = 0;
            wcx.hbrBackground = (IntPtr) Color.White;
            wcx.hInstance = System.Diagnostics.Process.GetCurrentProcess().Handle;
            wcx.hCursor = Core.Core.LoadCursor(IntPtr.Zero, CursorStyle.IDC_ARROW);
            wcx.lpszMenuName = menu;
            wcx.lpszClassName = className;
            return Core.Core.RegisterClassEx2(ref wcx);
        }

        public static void Exit(int code = 0)
        {
            Core.Core.PostQuitMessage(code);
        }

        public static void Terminate(int code = 0)
        {
            Core.Core.FatalExit(code);
        }
    }
}
