using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RAStudio.TransparentOverlay
{
    /// <summary>
    /// Implements the transparent window functionality for Windows platform.
    /// </summary>
    public class WindowsTransparentWindow : ITransparentWindow
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        private const int GWL_EXSTYLE = -20;
        private const uint WS_EX_LAYERED = 0x00080000;
        private const uint WS_EX_TRANSPARENT = 0x00000020;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        private IntPtr windowHandle;
        private bool disposed = false;

        public void Initialize(WindowMode windowMode)
        {
            windowHandle = GetActiveWindow();
            SetWindowLong(windowHandle, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

            switch (windowMode)
            {
                case WindowMode.SingleWindow:
                case WindowMode.FullScreen:
                    SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, Screen.width, Screen.height, 0x0040);
                    break;
                case WindowMode.AllMonitors:
                    // Implementation for all monitors setup
                    Debug.Log("All monitors mode not implemented for Windows.");
                    break;
            }
        }

        public void SetClickthrough(bool clickthrough)
        {
            SetWindowLong(windowHandle, GWL_EXSTYLE, clickthrough ? WS_EX_LAYERED | WS_EX_TRANSPARENT : WS_EX_LAYERED);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                }

                SetWindowLong(windowHandle, GWL_EXSTYLE, 0);
                disposed = true;
            }
        }

        ~WindowsTransparentWindow()
        {
            Dispose(false);
        }
    }
}