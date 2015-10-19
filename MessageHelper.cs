using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace CWExpert
{

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public uint cbSize;
        public Rectangle rcWindow;
        public Rectangle rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }
    }

    public class MessageHelper
    {
        [DllImport("User32.dll")]
        private static extern int RegisterWindowMessage(string lpString);

        [DllImport("User32.dll", EntryPoint = "GetWindowInfo")]
        private static extern bool GetWindowInfo(int hwnd, ref WINDOWINFO winfo);

        [DllImport("User32.dll", EntryPoint = "GetWindowRect")]
        private static extern bool GetWindowRect(int hwnd, ref Rectangle rect);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(String lpClassName, String lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern int FindWindowEx(int hwndParent, int hwndChildAfter, String lpClassName, String lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(int hWnd);
/*
        private System.ComponentModel.IContainer components;

        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();
*/
        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);
/*
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label captionWindowLabel;
        private System.Windows.Forms.Label IDWindowLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
*/     

        public string GetWindowLabel(int handle)
        {

            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0) { return Buff.ToString(); }
            else
                return "?";
        }

        public const int WM_USER = 0x400;
        public const int WM_COPYDATA = 0x4A;
        public int WM_SETTEXT = 0x000c;

        public bool bringAppToFront(int hWnd)
        {
            return SetForegroundWindow(hWnd);
        }

        public int sendWindowsStringMessage(int hWnd, int wParam, string msg)
        {
            int result = 0;

            if (hWnd != 0)
            {
                result = SendMessage(hWnd, WM_SETTEXT, wParam, new StringBuilder(msg));
            }

            return result;
        }

        public int sendWindowsMessage(int hWnd, int Msg, int wParam, int lParam)
        {
            int result = 0;

            if (hWnd != 0)
            {
                result = SendMessage(hWnd, Msg, wParam, lParam);
            }

            return result;
        }

        public int getWindowId(string className, string windowName)
        {
            return FindWindow(className, windowName);  
        }

        public int getWindowIdEx(int hwndParent, int hwndChild, string className, string windowName)
        {
            return FindWindowEx(hwndParent, hwndChild, className, windowName);
        }

        public bool getWindowRect(int hWnd, Rectangle rect)
        {
            return GetWindowRect(hWnd, ref rect);
        }
/*
        public int getHandle(string exe_name)  // writen by S56A, ex-maintenance engineer
        {
            using System.Diagnostics;
            
            Process[] procs = Process.GetProcesses();
            IntPtr hWnd;
            int ret = 0;
            foreach (Process proc in procs)
            {
                if (((hWnd = proc.MainWindowHandle) != IntPtr.Zero) && (proc.ProcessName.Contains(name)))
                {
                    ret =  hWnd;
                }
            }
            return ret;
        }
*/
        public WINDOWINFO getWindowInfo(int hwnd)
        {
            WINDOWINFO Winfo = new WINDOWINFO();
            GetWindowInfo(hwnd, ref Winfo);
            return Winfo;
        }
    }
}