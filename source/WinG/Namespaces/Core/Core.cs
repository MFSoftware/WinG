using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using WinG.Drawing;
using Point = WinG.Drawing.Point;

namespace WinG.Core
{
    public class Core
    {
        #region Classes

        public enum GetWindowCmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        [Flags]
        public enum WINDOWS_STATION_ACCESS_MASK : uint
        {
            WINSTA_NONE = 0,

            WINSTA_ENUMDESKTOPS = 0x0001,
            WINSTA_READATTRIBUTES = 0x0002,
            WINSTA_ACCESSCLIPBOARD = 0x0004,
            WINSTA_CREATEDESKTOP = 0x0008,
            WINSTA_WRITEATTRIBUTES = 0x0010,
            WINSTA_ACCESSGLOBALATOMS = 0x0020,
            WINSTA_EXITWINDOWS = 0x0040,
            WINSTA_ENUMERATE = 0x0100,
            WINSTA_READSCREEN = 0x0200,

            WINSTA_ALL_ACCESS = (WINSTA_ENUMDESKTOPS | WINSTA_READATTRIBUTES | WINSTA_ACCESSCLIPBOARD |
                            WINSTA_CREATEDESKTOP | WINSTA_WRITEATTRIBUTES | WINSTA_ACCESSGLOBALATOMS |
                            WINSTA_EXITWINDOWS | WINSTA_ENUMERATE | WINSTA_READSCREEN),
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SecurityAttributes
        {
            #region Struct members
            [MarshalAs(UnmanagedType.U4)]
            private int mStuctLength;

            private IntPtr mSecurityDescriptor;

            [MarshalAs(UnmanagedType.U4)]
            private bool mInheritHandle;
            #endregion

            public SecurityAttributes()
            {
                mStuctLength = Marshal.SizeOf(typeof(SecurityAttributes));
                mSecurityDescriptor = IntPtr.Zero;
            }

            public IntPtr SecurityDescriptor
            {
                get { return mSecurityDescriptor; }
                set { mSecurityDescriptor = value; }
            }

            public bool Inherit
            {
                get { return mInheritHandle; }
                set { mInheritHandle = value; }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public UInt16 wServicePackMajor;
            public UInt16 wServicePackMinor;
            public UInt16 wSuiteMask;
            public byte wProductType;
            public byte wReserved;

        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool MoveFileWithProgress(string lpExistingFileName, string lpNewFileName,
        CopyProgressRoutine lpProgressRoutine, IntPtr lpData, MoveFileFlags dwCopyFlags);

        public delegate CopyProgressResult CopyProgressRoutine(
        long TotalFileSize,
        long TotalBytesTransferred,
        long StreamSize,
        long StreamBytesTransferred,
        uint dwStreamNumber,
        CopyProgressCallbackReason dwCallbackReason,
        IntPtr hSourceFile,
        IntPtr hDestinationFile,
        IntPtr lpData);

        public enum CopyProgressResult : uint
        {
            PROGRESS_CONTINUE = 0,
            PROGRESS_CANCEL = 1,
            PROGRESS_STOP = 2,
            PROGRESS_QUIET = 3
        }

        public enum CopyProgressCallbackReason : uint
        {
            CALLBACK_CHUNK_FINISHED = 0x00000000,
            CALLBACK_STREAM_SWITCH = 0x00000001
        }

        [Flags]
        public enum MoveFileFlags : uint
        {
            MOVE_FILE_REPLACE_EXISTSING = 0x00000001,
            MOVE_FILE_COPY_ALLOWED = 0x00000002,
            MOVE_FILE_DELAY_UNTIL_REBOOT = 0x00000004,
            MOVE_FILE_WRITE_THROUGH = 0x00000008,
            MOVE_FILE_CREATE_HARDLINK = 0x00000010,
            MOVE_FILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
        }

        [Flags]
        public enum CopyFileFlags : uint
        {
            COPY_FILE_FAIL_IF_EXISTS = 0x00000001,
            COPY_FILE_RESTARTABLE = 0x00000002,
            COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x00000004,
            COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x00000008
        }

        public enum COMPUTER_NAME_FORMAT
        {
            ComputerNameNetBIOS,
            ComputerNameDnsHostname,
            ComputerNameDnsDomain,
            ComputerNameDnsFullyQualified,
            ComputerNamePhysicalNetBIOS,
            ComputerNamePhysicalDnsHostname,
            ComputerNamePhysicalDnsDomain,
            ComputerNamePhysicalDnsFullyQualified
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            byte BlendOp;
            byte BlendFlags;
            byte SourceConstantAlpha;
            byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        const int AC_SRC_OVER = 0x00;
        const int AC_SRC_ALPHA = 0x01;

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            /// <summary>dest = (source AND pattern)</summary>
            MERGECOPY = 0x00C000CA,
            /// <summary>dest = (NOT source) OR dest</summary>
            MERGEPAINT = 0x00BB0226,
            /// <summary>dest = pattern</summary>
            PATCOPY = 0x00F00021,
            /// <summary>dest = DPSnoo</summary>
            PATPAINT = 0x00FB0A09,
            /// <summary>dest = pattern XOR dest</summary>
            PATINVERT = 0x005A0049,
            /// <summary>dest = (NOT dest)</summary>
            DSTINVERT = 0x00550009,
            /// <summary>dest = BLACK</summary>
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000
        }


        public static class Win32_IDC_Constants
        {
            public const int
             IDC_ARROW = 32512,
             IDC_IBEAM = 32513,
             IDC_WAIT = 32514,
             IDC_CROSS = 32515,
             IDC_UPARROW = 32516,
             IDC_SIZE = 32640,
             IDC_ICON = 32641,
             IDC_SIZENWSE = 32642,
             IDC_SIZENESW = 32643,
             IDC_SIZEWE = 32644,
             IDC_SIZENS = 32645,
             IDC_SIZEALL = 32646,
             IDC_NO = 32648,
             IDC_HAND = 32649,
             IDC_APPSTARTING = 32650,
             IDC_HELP = 32651;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WNDCLASSEX
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int style;
            public IntPtr lpfnWndProc; 
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [Flags()]
        public enum WindowStyles : uint
        { 
            WS_BORDER = 0x800000, 
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000, 
            WS_GROUP = 0x20000,
            WS_HSCROLL = 0x100000,  
            WS_MAXIMIZE = 0x1000000,
            WS_MAXIMIZEBOX = 0x10000,  
            WS_MINIMIZE = 0x20000000,
            /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>  
            WS_MINIMIZEBOX = 0x20000,
            /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>  
            WS_OVERLAPPED = 0x0,
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

            /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>  
            WS_POPUP = 0x80000000u,

            /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>  
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

            /// <summary>The window has a sizing border.</summary>  
            WS_SIZEFRAME = 0x40000,

            /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>  
            WS_SYSMENU = 0x80000,  
            WS_TABSTOP = 0x10000,

            /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>  
            WS_VISIBLE = 0x10000000, 
            WS_VSCROLL = 0x200000,
            BS_DEFPUSHBUTTON = 1,
            BS_CHECKBOX = 0x00000002,
            SS_LEFT = 0,
            ES_MULTILINE = 4
        }

        public enum WindowLongFlags : int
        {
            GWL_EXSTYLE = -20,
            GWLP_HINSTANCE = -6,
            GWLP_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
            DWLP_USER = 0x8,
            DWLP_MSGRESULT = 0x0,
            DWLP_DLGPROC = 0x4
        }

        [Flags]
        public enum WindowStylesEx : uint
        {
            WS_EX_ACCEPTFILES = 0x00000010, 
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_LAYERED = 0x00080000,
            WS_EX_LAYOUTRTL = 0x00400000,  
            WS_EX_LEFT = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_LTRREADING = 0x00000000, 
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_NOACTIVATE = 0x08000000,
            WS_EX_NOINHERITLAYOUT = 0x00100000,  
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE, 
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST, 
            WS_EX_RIGHT = 0x00001000, 
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            WS_EX_RTLREADING = 0x00002000, 
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_WINDOWEDGE = 0x00000100
        }

        [Flags]
        public enum ClassStyles : uint
        {
            ByteAlignClient = 0x1000,
            ByteAlignWindow = 0x2000,
            ClassDC = 0x40,
            DoubleClicks = 0x8, 
            DropShadow = 0x20000,
            GlobalClass = 0x4000,
            HorizontalRedraw = 0x2,  
            NoClose = 0x200,
            OwnDC = 0x20,
            ParentDC = 0x80,
            SaveBits = 0x800,
            VerticalRedraw = 0x1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public Rect rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr Wnd, bool Enable);

        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASS
        {
            public ClassStyles style;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WndProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszClassName;
        }

        public enum WM : uint
        {
            NULL = 0x0000,
            SETRANGE = USER + 1,
            SETPOS = USER + 2,
            SETSTEP = USER + 4,
            GETPOS = USER + 8,
            SETBARCOLOR = USER + 9,
            GETSTEP = USER + 13,
            SETBKCOLOR = 0x2000 + 1,
            CREATE = 0x0001,  
            DESTROY = 0x0002,  
            MOVE = 0x0003,  
            SIZE = 0x0005,
            ACTIVATE = 0x0006,
            SETFOCUS = 0x0007,
            KILLFOCUS = 0x0008,  
            ENABLE = 0x000A,
            SETREDRAW = 0x000B, 
            SETTEXT = 0x000C,
            GETTEXT = 0x000D,
            GETTEXTLENGTH = 0x000E,
            PAINT = 0x000F,
            CLOSE = 0x0010,
            /// <summary>  
            /// The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.  
            /// After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.  
            /// </summary>  
            QUERYENDSESSION = 0x0011,
            /// <summary>  
            /// The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.  
            /// </summary>  
            QUERYOPEN = 0x0013,
            /// <summary>  
            /// The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.  
            /// </summary>  
            ENDSESSION = 0x0016,
            /// <summary>  
            /// The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.  
            /// </summary>  
            QUIT = 0x0012,
            /// <summary>  
            /// The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting.   
            /// </summary>  
            ERASEBKGND = 0x0014,
            /// <summary>  
            /// This message is sent to all top-level windows when a change is made to a system color setting.   
            /// </summary>  
            SYSCOLORCHANGE = 0x0015,
            /// <summary>  
            /// The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.  
            /// </summary>  
            SHOWWINDOW = 0x0018,
            /// <summary>  
            /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.  
            /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.  
            /// </summary>  
            WININICHANGE = 0x001A,
            /// <summary>  
            /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.  
            /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.  
            /// </summary>  
            SETTINGCHANGE = WININICHANGE,
            /// <summary>  
            /// The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings.   
            /// </summary>  
            DEVMODECHANGE = 0x001B,
            /// <summary>  
            /// The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.  
            /// </summary>  
            ACTIVATEAPP = 0x001C,
            /// <summary>  
            /// An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources.   
            /// </summary>  
            FONTCHANGE = 0x001D,
            /// <summary>  
            /// A message that is sent whenever there is a change in the system time.  
            /// </summary>  
            TIMECHANGE = 0x001E,
            /// <summary>  
            /// The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.  
            /// </summary>  
            CANCELMODE = 0x001F,
            /// <summary>  
            /// The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.   
            /// </summary>  
            SETCURSOR = 0x0020,
            /// <summary>  
            /// The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.  
            /// </summary>  
            MOUSEACTIVATE = 0x0021,
            /// <summary>  
            /// The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.  
            /// </summary>  
            CHILDACTIVATE = 0x0022,
            /// <summary>  
            /// The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure.   
            /// </summary>  
            QUEUESYNC = 0x0023,
            /// <summary>  
            /// The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size.   
            /// </summary>  
            GETMINMAXINFO = 0x0024,
            /// <summary>  
            /// Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.  
            /// </summary>  
            PAINTICON = 0x0026,
            /// <summary>  
            /// Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.  
            /// </summary>  
            ICONERASEBKGND = 0x0027,
            /// <summary>  
            /// The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box.   
            /// </summary>  
            NEXTDLGCTL = 0x0028,
            /// <summary>  
            /// The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue.   
            /// </summary>  
            SPOOLERSTATUS = 0x002A,
            /// <summary>  
            /// The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.  
            /// </summary>  
            DRAWITEM = 0x002B,
            /// <summary>  
            /// The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.  
            /// </summary>  
            MEASUREITEM = 0x002C,
            /// <summary>  
            /// Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.  
            /// </summary>  
            DELETEITEM = 0x002D,
            /// <summary>  
            /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message.   
            /// </summary>  
            VKEYTOITEM = 0x002E,
            /// <summary>  
            /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message.   
            /// </summary>  
            CHARTOITEM = 0x002F,
            SETFONT = 0x0030,
            GETFONT = 0x0031,
            SETHOTKEY = 0x0032, 
            GETHOTKEY = 0x0033, 
            QUERYDRAGICON = 0x0037,
            COMPAREITEM = 0x0039, 
            GETOBJECT = 0x003D,
            COMPACTING = 0x0041,
            [Obsolete]
            COMMNOTIFY = 0x0044,
            WINDOWPOSCHANGING = 0x0046,
            WINDOWPOSCHANGED = 0x0047,
            [Obsolete]
            POWER = 0x0048,
            COPYDATA = 0x004A,  
            CANCELJOURNAL = 0x004B,
            NOTIFY = 0x004E,
            INPUTLANGCHANGEREQUEST = 0x0050,
            INPUTLANGCHANGE = 0x0051,
            TCARD = 0x0052,
            HELP = 0x0053,
            USERCHANGED = 0x0054, 
            NOTIFYFORMAT = 0x0055,
            CONTEXTMENU = 0x007B, 
            STYLECHANGING = 0x007C,  
            STYLECHANGED = 0x007D,
            DISPLAYCHANGE = 0x007E,
            GETICON = 0x007F,
            SETICON = 0x0080,
            NCCREATE = 0x0081,  
            NCDESTROY = 0x0082,
            NCCALCSIZE = 0x0083,
            /// <summary>  
            /// The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.  
            /// </summary>  
            NCHITTEST = 0x0084,
            /// <summary>  
            /// The WM_NCPAINT message is sent to a window when its frame must be painted.   
            /// </summary>  
            NCPAINT = 0x0085,
            /// <summary>  
            /// The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.  
            /// </summary>  
            NCACTIVATE = 0x0086,
            /// <summary>  
            /// The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.  
            /// </summary>  
            GETDLGCODE = 0x0087,
            /// <summary>  
            /// The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.  
            /// </summary>  
            SYNCPAINT = 0x0088,
            /// <summary>  
            /// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCMOUSEMOVE = 0x00A0,
            /// <summary>  
            /// The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCLBUTTONDOWN = 0x00A1,
            /// <summary>  
            /// The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCLBUTTONUP = 0x00A2,
            /// <summary>  
            /// The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCLBUTTONDBLCLK = 0x00A3,
            /// <summary>  
            /// The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCRBUTTONDOWN = 0x00A4,
            /// <summary>  
            /// The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCRBUTTONUP = 0x00A5,
            /// <summary>  
            /// The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCRBUTTONDBLCLK = 0x00A6,
            /// <summary>  
            /// The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCMBUTTONDOWN = 0x00A7,
            /// <summary>  
            /// The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCMBUTTONUP = 0x00A8,
            /// <summary>  
            /// The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCMBUTTONDBLCLK = 0x00A9,
            /// <summary>  
            /// The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCXBUTTONDOWN = 0x00AB,
            /// <summary>  
            /// The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCXBUTTONUP = 0x00AC,
            /// <summary>  
            /// The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.  
            /// </summary>  
            NCXBUTTONDBLCLK = 0x00AD,
            /// <summary>  
            /// The WM_INPUT_DEVICE_CHANGE message is sent to the window that registered to receive raw input. A window receives this message through its WindowProc function.  
            /// </summary>  
            INPUT_DEVICE_CHANGE = 0x00FE,
            /// <summary>  
            /// The WM_INPUT message is sent to the window that is getting raw input.   
            /// </summary>  
            INPUT = 0x00FF,
            /// <summary>  
            /// This message filters for keyboard messages.  
            /// </summary>  
            KEYFIRST = 0x0100,
            /// <summary>  
            /// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.   
            /// </summary>  
            KEYDOWN = 0x0100,
            /// <summary>  
            /// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.   
            /// </summary>  
            KEYUP = 0x0101,
            /// <summary>  
            /// The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed.   
            /// </summary>  
            CHAR = 0x0102,
            /// <summary>  
            /// The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key.   
            /// </summary>  
            DEADCHAR = 0x0103,
            /// <summary>  
            /// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.   
            /// </summary>  
            SYSKEYDOWN = 0x0104,
            /// <summary>  
            /// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.   
            /// </summary>  
            SYSKEYUP = 0x0105,
            /// <summary>  
            /// The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down.   
            /// </summary>  
            SYSCHAR = 0x0106,
            /// <summary>  
            /// The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key.   
            /// </summary>  
            SYSDEADCHAR = 0x0107,
            /// <summary>  
            /// The WM_UNICHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_UNICHAR message contains the character code of the key that was pressed.   
            /// The WM_UNICHAR message is equivalent to WM_CHAR, but it uses Unicode Transformation Format (UTF)-32, whereas WM_CHAR uses UTF-16. It is designed to send or post Unicode characters to ANSI windows and it can can handle Unicode Supplementary Plane characters.  
            /// </summary>  
            UNICHAR = 0x0109,
            /// <summary>  
            /// This message filters for keyboard messages.  
            /// </summary>  
            KEYLAST = 0x0109,
            /// <summary>  
            /// Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function.   
            /// </summary>  
            IME_STARTCOMPOSITION = 0x010D,
            /// <summary>  
            /// Sent to an application when the IME ends composition. A window receives this message through its WindowProc function.   
            /// </summary>  
            IME_ENDCOMPOSITION = 0x010E,
            /// <summary>  
            /// Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function.   
            /// </summary>  
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,
            /// <summary>  
            /// The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.   
            /// </summary>  
            INITDIALOG = 0x0110,
            /// <summary>  
            /// The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.   
            /// </summary>  
            COMMAND = 0x0111,
            /// <summary>  
            /// A window receives this message when the user chooses a command from the Window menu, clicks the maximize button, minimize button, restore button, close button, or moves the form. You can stop the form from moving by filtering this out.  
            /// </summary>  
            SYSCOMMAND = 0x0112,
            /// <summary>  
            /// The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.   
            /// </summary>  
            TIMER = 0x0113,
            /// <summary>  
            /// The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control.   
            /// </summary>  
            HSCROLL = 0x0114,
            /// <summary>  
            /// The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.   
            /// </summary>  
            VSCROLL = 0x0115,
            /// <summary>  
            /// The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed.   
            /// </summary>  
            INITMENU = 0x0116,
            /// <summary>  
            /// The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed, without changing the entire menu.   
            /// </summary>  
            INITMENUPOPUP = 0x0117,
            /// <summary>  
            /// The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item.   
            /// </summary>  
            MENUSELECT = 0x011F,
            /// <summary>  
            /// The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu.   
            /// </summary>  
            MENUCHAR = 0x0120,
            /// <summary>  
            /// The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages.   
            /// </summary>  
            ENTERIDLE = 0x0121,
            /// <summary>  
            /// The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item.   
            /// </summary>  
            MENURBUTTONUP = 0x0122,
            /// <summary>  
            /// The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item.   
            /// </summary>  
            MENUDRAG = 0x0123,
            /// <summary>  
            /// The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.   
            /// </summary>  
            MENUGETOBJECT = 0x0124,
            /// <summary>  
            /// The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed.   
            /// </summary>  
            UNINITMENUPOPUP = 0x0125,
            /// <summary>  
            /// The WM_MENUCOMMAND message is sent when the user makes a selection from a menu.   
            /// </summary>  
            MENUCOMMAND = 0x0126,
            /// <summary>  
            /// An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.  
            /// </summary>  
            CHANGEUISTATE = 0x0127,
            /// <summary>  
            /// An application sends the WM_UPDATEUISTATE message to change the user interface (UI) state for the specified window and all its child windows.  
            /// </summary>  
            UPDATEUISTATE = 0x0128,
            /// <summary>  
            /// An application sends the WM_QUERYUISTATE message to retrieve the user interface (UI) state for a window.  
            /// </summary>  
            QUERYUISTATE = 0x0129,
            /// <summary>  
            /// The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle.   
            /// </summary>  
            CTLCOLORMSGBOX = 0x0132,
            /// <summary>  
            /// An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control.   
            /// </summary>  
            CTLCOLOREDIT = 0x0133,
            /// <summary>  
            /// Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle.   
            /// </summary>  
            CTLCOLORLISTBOX = 0x0134,
            /// <summary>  
            /// The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message.   
            /// </summary>  
            CTLCOLORBTN = 0x0135,
            /// <summary>  
            /// The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle.   
            /// </summary>  
            CTLCOLORDLG = 0x0136,
            /// <summary>  
            /// The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the display context handle to set the background color of the scroll bar control.   
            /// </summary>  
            CTLCOLORSCROLLBAR = 0x0137,
            /// <summary>  
            /// A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control.   
            /// </summary>  
            CTLCOLORSTATIC = 0x0138,
            /// <summary>  
            /// Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.  
            /// </summary>  
            MOUSEFIRST = 0x0200,
            /// <summary>  
            /// The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            MOUSEMOVE = 0x0200,
            /// <summary>  
            /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            LBUTTONDOWN = 0x0201,
            /// <summary>  
            /// The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            LBUTTONUP = 0x0202,
            /// <summary>  
            /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            LBUTTONDBLCLK = 0x0203,
            /// <summary>  
            /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            RBUTTONDOWN = 0x0204,
            /// <summary>  
            /// The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            RBUTTONUP = 0x0205,
            /// <summary>  
            /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            RBUTTONDBLCLK = 0x0206,
            /// <summary>  
            /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            MBUTTONDOWN = 0x0207,
            /// <summary>  
            /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            MBUTTONUP = 0x0208,
            /// <summary>  
            /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            MBUTTONDBLCLK = 0x0209,
            /// <summary>  
            /// The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.  
            /// </summary>  
            MOUSEWHEEL = 0x020A,
            /// <summary>  
            /// The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.   
            /// </summary>  
            XBUTTONDOWN = 0x020B,
            /// <summary>  
            /// The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            XBUTTONUP = 0x020C,
            /// <summary>  
            /// The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.  
            /// </summary>  
            XBUTTONDBLCLK = 0x020D,
            /// <summary>  
            /// The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.  
            /// </summary>  
            MOUSEHWHEEL = 0x020E,
            /// <summary>  
            /// Use WM_MOUSELAST to specify the last mouse message. Used with PeekMessage() Function.  
            /// </summary>  
            MOUSELAST = 0x020E,
            /// <summary>  
            /// The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created, the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.  
            /// </summary>  
            PARENTNOTIFY = 0x0210,
            /// <summary>  
            /// The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered.   
            /// </summary>  
            ENTERMENULOOP = 0x0211,
            /// <summary>  
            /// The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited.   
            /// </summary>  
            EXITMENULOOP = 0x0212,
            /// <summary>  
            /// The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.   
            /// </summary>  
            NEXTMENU = 0x0213,
            /// <summary>  
            /// The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position.   
            /// </summary>  
            SIZING = 0x0214,
            /// <summary>  
            /// The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.  
            /// </summary>  
            CAPTURECHANGED = 0x0215,
            /// <summary>  
            /// The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.  
            /// </summary>  
            MOVING = 0x0216,
            /// <summary>  
            /// Notifies applications that a power-management event has occurred.  
            /// </summary>  
            POWERBROADCAST = 0x0218,
            /// <summary>  
            /// Notifies an application of a change to the hardware configuration of a device or the computer.  
            /// </summary>  
            DEVICECHANGE = 0x0219,
            /// <summary>  
            /// An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window.   
            /// </summary>  
            MDICREATE = 0x0220,
            /// <summary>  
            /// An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window.   
            /// </summary>  
            MDIDESTROY = 0x0221,
            /// <summary>  
            /// An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.   
            /// </summary>  
            MDIACTIVATE = 0x0222,
            /// <summary>  
            /// An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.   
            /// </summary>  
            MDIRESTORE = 0x0223,
            /// <summary>  
            /// An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window.   
            /// </summary>  
            MDINEXT = 0x0224,
            /// <summary>  
            /// An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window.   
            /// </summary>  
            MDIMAXIMIZE = 0x0225,
            /// <summary>  
            /// An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.   
            /// </summary>  
            MDITILE = 0x0226,
            /// <summary>  
            /// An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.   
            /// </summary>  
            MDICASCADE = 0x0227,
            /// <summary>  
            /// An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized.   
            /// </summary>  
            MDIICONARRANGE = 0x0228,
            /// <summary>  
            /// An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.   
            /// </summary>  
            MDIGETACTIVE = 0x0229,
            /// <summary>  
            /// An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both.   
            /// </summary>  
            MDISETMENU = 0x0230,
            /// <summary>  
            /// The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.   
            /// The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.  
            /// </summary>  
            ENTERSIZEMOVE = 0x0231,
            /// <summary>  
            /// The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.   
            /// </summary>  
            EXITSIZEMOVE = 0x0232,
            /// <summary>  
            /// Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.  
            /// </summary>  
            DROPFILES = 0x0233,
            /// <summary>  
            /// An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.   
            /// </summary>  
            MDIREFRESHMENU = 0x0234,
            /// <summary>  
            /// Sent to an application when a window is activated. A window receives this message through its WindowProc function.   
            /// </summary>  
            IME_SETCONTEXT = 0x0281,
            /// <summary>  
            /// Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function.   
            /// </summary>  
            IME_NOTIFY = 0x0282, 
            IME_CONTROL = 0x0283, 
            IME_COMPOSITIONFULL = 0x0284,
            IME_SELECT = 0x0285,
            IME_CHAR = 0x0286, 
            IME_REQUEST = 0x0288,
            IME_KEYDOWN = 0x0290,
            IME_KEYUP = 0x0291,  
            MOUSEHOVER = 0x02A1,  
            MOUSELEAVE = 0x02A3,
            NCMOUSEHOVER = 0x02A0,
            NCMOUSELEAVE = 0x02A2, 
            WTSSESSION_CHANGE = 0x02B1,
            TABLET_FIRST = 0x02c0,
            TABLET_LAST = 0x02df,
            CUT = 0x0300, 
            COPY = 0x0301,
            PASTE = 0x0302,
            CLEAR = 0x0303, 
            UNDO = 0x0304, 
            RENDERFORMAT = 0x0305, 
            RENDERALLFORMATS = 0x0306,  
            DESTROYCLIPBOARD = 0x0307, 
            DRAWCLIPBOARD = 0x0308,
            PAINTCLIPBOARD = 0x0309,
            VSCROLLCLIPBOARD = 0x030A,  
            SIZECLIPBOARD = 0x030B,
            ASKCBFORMATNAME = 0x030C, 
            CHANGECBCHAIN = 0x030D, 
            HSCROLLCLIPBOARD = 0x030E, 
            QUERYNEWPALETTE = 0x030F,
            PALETTEISCHANGING = 0x0310, 
            PALETTECHANGED = 0x0311,
            HOTKEY = 0x0312,
            PRINT = 0x0317,
            PRINTCLIENT = 0x0318, 
            APPCOMMAND = 0x0319, 
            THEMECHANGED = 0x031A,
            CLIPBOARDUPDATE = 0x031D,
            DWMCOMPOSITIONCHANGED = 0x031E,  
            DWMNCRENDERINGCHANGED = 0x031F, 
            DWMCOLORIZATIONCOLORCHANGED = 0x0320,
            DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
            GETTITLEBARINFOEX = 0x033F,
            HANDHELDFIRST = 0x0358,
            HANDHELDLAST = 0x035F,
            AFXFIRST = 0x0360,
            AFXLAST = 0x037F,
            PENWINFIRST = 0x0380,
            PENWINLAST = 0x038F,
            APP = 0x8000,
            USER = 0x0400,
            CPL_LAUNCH = USER + 0x1000,
            CPL_LAUNCHED = USER + 0x1001,
            SYSTIMER = 0x118
        }

        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct MSG
        {
            public IntPtr hwnd;
            public UInt32 message;
            public UIntPtr wParam;
            public UIntPtr lParam;
            public UInt32 time;
            public Point pt;
        }

        #endregion

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetComputerNameEx(COMPUTER_NAME_FORMAT NameType, StringBuilder lpBuffer, ref uint lpnSize);

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadAcceleratorsW(IntPtr hInstance, int lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool TranslateAccelerator(IntPtr hWnd, IntPtr hAcess, ref MSG msg);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth,
        int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        public static string GenRandomString(int Length)
        {
            Random rnd = new Random();
            string Alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;

            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }

            return sb.ToString();
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
        CopyProgressRoutine lpProgressRoutine, IntPtr lpData,
        MoveFileFlags dwCopyFlags);

        [DllImport("kernel32.dll")]
        public static extern void FatalExit(int ExitCode);

        [DllImport("user32.dll")]
        public static extern void CheckDlgButton(IntPtr HWnd, int idButton, int word);

        [DllImport("user32.dll")]
        public static extern int IsDlgButtonChecked(IntPtr hDlg, int nIdButton);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetProcessIdOfThread(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
        CopyProgressRoutine lpProgressRoutine, IntPtr lpData, ref Int32 pbCancel,
        CopyFileFlags dwCopyFlags);

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        public static extern int SetClassLongA(int hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, WM m, int wParam, int lParam);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement,
        int nOrientation, int fnWeight, uint fdwItalic, uint fdwUnderline, uint
        fdwStrikeOut, uint fdwCharSet, uint fdwOutputPrecision, uint
        fdwClipPrecision, uint fdwQuality, uint fdwPitchAndFamily, string lpszFace);

        [DllImport("user32.dll")]
        public static extern bool UnregisterClass(string ClassName, IntPtr Instance);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType,
            int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr Wnd, StringBuilder ClassName, int MaxCount);

        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hwnd, string file, int a);

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        public static extern uint SetClassLongPtr32(HandleRef hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        public static extern IntPtr SetClassLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextColor(IntPtr hdc, int crColor);

        [DllImport("gdi32.dll")]
        public static extern uint GetTextColor(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern uint SetBkColor(IntPtr hdc, int crColor);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("comctl32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool InitCommonControls();

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EndTask(IntPtr hWnd, bool fShutDown, bool fForce);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowText(IntPtr hWnd, String lpString);

        [DllImport("user32.dll")]
        public static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx")]
        public static extern IntPtr CreateWindowEx(
           WindowStylesEx dwExStyle,
           string lpClassName,
           string lpWindowName,
           WindowStyles dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx")]
        public static extern IntPtr CreateWindowEx2(
           WindowStylesEx dwExStyle,
           UInt16 lpClassName,
           string lpWindowName,
           WindowStyles dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);

        [DllImport("user32.dll")]
        public static extern ushort RegisterClass([In] ref WNDCLASS lpWndClass);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, WindowState nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll")]
        public static extern int GetClassLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int DrawText(IntPtr hDC, string lpString, int nCount, ref Rect lpRect, uint uFormat);

        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, int lpIconName);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIConName);

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetStockObject(Color fnObject);

        [DllImport("user32.dll")]
        public static extern MessageBoxResult MessageBox(IntPtr hWnd, string text, string caption, MessageBoxType options);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MessageBeep(MessageType uType);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(ref MSG lpMsg, int hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        //[DllImport("user32.dll")]
        //public static extern IntPtr LoadCursor(IntPtr hInstance, CursorStyle lpCursorName);

        [DllImport("user32")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr hInstance);

        [DllImport("user32.dll")]
        public static extern short RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassEx")]
        public static extern UInt16 RegisterClassEx2([In] ref WNDCLASSEX lpwcx);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveDirectory(string lpPathName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EmptyClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SetDataObject(string text, bool b);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreateDirectory(string lpPathName, SecurityAttribute lpSecurityAttributes = null);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetCurrentDirectory(uint nBufferLength, [Out] StringBuilder lpBuffer);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        public static int MakeCOLORREF(byte r, byte g, byte b)
        {
            return (int)(((uint)r) | (((uint)g) << 8) | (((uint)b) << 16));
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr HWnd, GetWindowCmd cmd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public static void ParseCssLine(string line, IntPtr hwnd)
        {
            StringBuilder Buff = new StringBuilder(256);
            if (Core.GetClassName(hwnd, Buff, 256) > 0) { string type = Buff.ToString(); }
            //int counter = 1;

            string[] arr = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            
        }
    }
}
