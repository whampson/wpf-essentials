using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using WpfEssentials.Properties;

namespace WpfEssentials
{
    /// <summary>
    /// A <see cref="System.Windows.MessageBox"/> wrapper that is more compatible
    /// with WPF applications.
    /// </summary>
    /// <remarks>
    /// Adapted from responses to a question asked at https://stackoverflow.com/q/564710/.
    /// </remarks>
    public static class MessageBoxEx
    {
        private static readonly HookProc m_hookProc;
        private static IntPtr m_owner;
        private static IntPtr m_hHook;

        static MessageBoxEx()
        {
            m_hookProc = new HookProc(MessageBoxHookProc);
            m_hHook = IntPtr.Zero;
        }

        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text)
        {
            Initialize();

            return MessageBox.Show(text);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption; and that returns a result.
        /// </summary>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text, string caption)
        {
            Initialize();

            return MessageBox.Show(text, caption);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, and button; and
        /// thast returns a result.
        /// </summary>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons)
        {
            Initialize();

            return MessageBox.Show(text, caption, buttons);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon;
        /// and that returns a result.
        /// </summary>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons,
            MessageBoxImage icon)
        {
            Initialize();

            return MessageBox.Show(text, caption, buttons, icon);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon;
        /// and that accepts a default message box result and returns a result.
        /// </summary>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons,
            MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            Initialize();

            return MessageBox.Show(text, caption, buttons, icon, defaultResult);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon;
        /// and that accepts a default message box result, complies with the specified options,
        /// and returns a result.
        /// </summary>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <param name="options">A <see cref="MessageBoxOptions"/> value object that specifies the options.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons,
            MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            Initialize();

            return MessageBox.Show(text, caption, buttons, icon, defaultResult, options);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays
        /// a message and that returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text)
        {
            if (owner == null)
            {
                return Show(text);
            }

            m_owner = new WindowInteropHelper(owner).Handle;
            Initialize();

            return MessageBox.Show(owner, text);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays
        /// a message, title bar caption; and that returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text, string caption)
        {
            if (owner == null)
            {
                return Show(text, caption);
            }

            m_owner = new WindowInteropHelper(owner).Handle;
            Initialize();

            return MessageBox.Show(owner, text, caption);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays
        /// a message, title bar caption, and button; and that returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons)
        {
            if (owner == null)
            {
                return Show(text, caption, buttons);
            }

            m_owner = new WindowInteropHelper(owner).Handle;
            Initialize();

            return MessageBox.Show(owner, text, caption, buttons);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays
        /// a message, title bar caption, button, and icon; and that returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons,
            MessageBoxImage icon)
        {
            if (owner == null)
            {
                return Show(text, caption, buttons, icon);
            }

            m_owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays
        /// a message, title bar caption, button, and icon; and that accepts a default message
        /// box result and returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons,
            MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            if (owner == null)
            {
                return Show(text, caption, buttons, icon, defaultResult);
            }

            m_owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon, defaultResult);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays
        /// a message, title bar caption, button, and icon; and that accepts a default message
        /// box result, complies with the specified options, and returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="caption">The text to dsiplay in the window title.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <param name="options">A <see cref="MessageBoxOptions"/> value object that specifies the options.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons,
            MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            if (owner == null)
            {
                return Show(text, caption, buttons, icon, defaultResult, options);
            }

            m_owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon, defaultResult, options);
        }

        private static void Initialize()
        {
            if (m_hHook != IntPtr.Zero)
            {
                throw new NotSupportedException(Resources.NotSupported_MultipleCalls);
            }

#pragma warning disable 0618    // GetCurrentThreadId() is marked obsolete, but it still works for what we need it to do.
            if (m_owner != null)
            {
                m_hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, m_hookProc, IntPtr.Zero, GetCurrentThreadId().ToInt32());
            }
#pragma warning restore 0618
        }

        private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(m_hHook, nCode, wParam, lParam);
            }

            CWPRETSTRUCT msg = (CWPRETSTRUCT) Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            IntPtr hook = m_hHook;

            if (msg.message == (int) CbtHookAction.HCBT_ACTIVATE)
            {
                try
                {
                    CenterWindow(msg.hwnd);
                }
                finally
                {
                    UnhookWindowsHookEx(m_hHook);
                    m_hHook = IntPtr.Zero;
                }
            }

            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static void CenterWindow(IntPtr hChildWnd)
        {
            Rectangle recChild = new Rectangle(0, 0, 0, 0);
            GetWindowRect(hChildWnd, ref recChild);

            int width = recChild.Width - recChild.X;
            int height = recChild.Height - recChild.Y;

            Rectangle recParent = new Rectangle(0, 0, 0, 0);
            GetWindowRect(m_owner, ref recParent);

            System.Drawing.Point ptCenter = new System.Drawing.Point(0, 0)
            {
                X = recParent.X + ((recParent.Width - recParent.X) / 2),
                Y = recParent.Y + ((recParent.Height - recParent.Y) / 2)
            };

            System.Drawing.Point ptStart = new System.Drawing.Point(0, 0)
            {
                X = ptCenter.X - (width / 2),
                Y = ptCenter.Y - (height / 2)
            };

            ptStart.X = (ptStart.X < 0) ? 0 : ptStart.X;
            ptStart.Y = (ptStart.Y < 0) ? 0 : ptStart.Y;

            MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width, height, false);
        }


        /* ===== Win32 imports ===== */

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate void TimerProc(IntPtr hWind, uint uMsg, UIntPtr nIDEvent, uint dwTime);

        public const int WH_CALLWNDPROCRET = 12;

        public enum CbtHookAction : int
        {
            HCBT_MOVESIZE,
            HCBT_MINMAX,
            HCBT_QS,
            HCBT_CREATEWND,
            HCBT_DESTROYWND,
            HCBT_ACTIVATE,
            HCBT_CLICKSKIPPED,
            HCBT_KEYSKIPPED,
            HCBT_SYSCOMMAND,
            HCBT_SETFOCUS,
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")] 
        static extern IntPtr GetCurrentThreadId();

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        };
    }
}
