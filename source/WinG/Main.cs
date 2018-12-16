using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WinG
{
    public enum SystemIcons
    {
        IDI_APPLICATION = 32512,
        IDI_HAND = 32513,
        IDI_QUESTION = 32514,
        IDI_EXCLAMATION = 32515,
        IDI_ASTERISK = 32516,
        IDI_WINLOGO = 32517,
        IDI_SHIELD = 32518,
        IDI_WARNING = IDI_EXCLAMATION,
        IDI_ERROR = IDI_HAND,
        IDI_INFORMATION = IDI_ASTERISK
    }

    public enum Color
    {
        LiteGray = 1,
        Gray = 2,
        PoloBlue = 3,
        Black_Brush = 4,
        NULL_BRUSH = 5,
        HOLLOW_BRUSH = NULL_BRUSH,
        White = 6,
        Black = 7,
        NULL_PEN = 8,
        OEM_FIXED_FONT = 10,
        ANSI_FIXED_FONT = 11,
        ANSI_VAR_FONT = 12,
        SYSTEM_FONT = 13,
        DEVICE_DEFAULT_FONT = 14,
        DEFAULT_PALETTE = 15,
        SYSTEM_FIXED_FONT = 16,
        DEFAULT_GUI_FONT = 17,
        DarkGray = 18,
        DC_PEN = 19,
    }

    public static class ColorLoader
    {
        public static Color FromName(string name)
        {
            switch (name.ToLower())
            {
                case "litegray":
                    return Color.LiteGray;
                case "black":
                    return Color.Black;
                case "poloblue":
                    return Color.PoloBlue;
                case "darkgray":
                    return Color.DarkGray;
                default:
                    return Color.White;
            }
        }
    }

    public enum ButtonStyle
    {
        BS_PUSHBUTTON = 0x00000000,
        BS_DEFPUSHBUTTON = 0x00000001,
        BS_CHECKBOX = 0x00000002,
        BS_AUTOCHECKBOX = 0x00000003,
        BS_RADIOBUTTON = 0x00000004,
        BS_3STATE = 0x00000005,
        BS_AUTO3STATE = 0x00000006,
        BS_GROUPBOX = 0x00000007,
        BS_USERBUTTON = 0x00000008,
        BS_AUTORADIOBUTTON = 0x00000009,
        BS_PUSHBOX = 0x0000000A,
        BS_OWNERDRAW = 0x0000000B,
        BS_TYPEMASK = 0x0000000F,
        BS_LEFTTEXT = 0x00000020,
        BS_TEXT = 0x00000000,
        BS_ICON = 0x00000040,
        BS_BITMAP = 0x00000080,
        BS_LEFT = 0x00000100,
        BS_RIGHT = 0x00000200,
        BS_CENTER = 0x00000300,
        BS_TOP = 0x00000400,
        BS_BOTTOM = 0x00000800,
        BS_VCENTER = 0x00000C00,
        BS_PUSHLIKE = 0x00001000,
        BS_MULTILINE = 0x00002000,
        BS_NOTIFY = 0x00004000,
        BS_FLAT = 0x00008000,
        BS_RIGHTBUTTON = BS_LEFTTEXT
    }

    public enum WindowState : int
    {
        Hide = 0,
        Normal = 1,
        ShowMinimized = 2,
        Maximize = 3,    
        ShowMaximized = 3,
        ShowNoActivate = 4,
        Show = 5,  
        Minimize = 6, 
        ShowMinNoActive = 7,
        ShowNA = 8,
        Restore = 9,
        ShowDefault = 10, 
        ForceMinimize = 11
    }

    public enum MessageBoxResult : uint
    {
        Ok = 1,
        Cancel,
        Abort,
        Retry,
        Ignore,
        Yes,
        No,
        Close,
        Help,
        TryAgain,
        Continue,
        Timeout = 32000
    }

    public enum MessageBoxType : uint
    {
        Ok = 0,
        OkCancel = 1,
        AbortRetryIgnore = 2,
        OkNoCancel = 3,
        YesNo = 4,
        RetryCancel = 5,
        CancelRetryContinue = 6,
        Error = 16,
        Warning = 48
    }

    public enum MessageType : uint
    {
        Warning = 48,
        Error = 16
    }

    public enum CursorStyle : int
    {
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
        IDC_HELP = 32651
    }

    public enum WindowStyle : int
    {
        WS_CHILD = 0x40000000,
        WS_BORDER = 0x00800000,
        WS_DLGFRAME = 0x00400000,
        WS_CAPTION = WS_BORDER | WS_DLGFRAME, 
        WS_SYSMENU = 0x00080000,
        BS_DEFPUSHBUTTON = 1
    }
}
