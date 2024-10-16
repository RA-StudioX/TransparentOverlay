using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

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

        [DllImport("user32.dll")]
        static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("Dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        private const int GWL_EXSTYLE = -20;
        private const int GWL_STYLE = -16;
        private const uint WS_EX_LAYERED = 0x00080000;
        private const uint WS_EX_TRANSPARENT = 0x00000020;
        private const uint WS_POPUP = 0x80000000;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        private IntPtr windowHandle;
        private bool disposed = false;

        public void Initialize(WindowMode windowMode)
        {
            windowHandle = GetActiveWindow();
            SetWindowLong(windowHandle, GWL_STYLE, WS_POPUP);

            // Extend the frame into the client area
            var margins = new MARGINS() { cxLeftWidth = -1 };
            DwmExtendFrameIntoClientArea(windowHandle, ref margins);

            SetWindowLong(windowHandle, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

            switch (windowMode)
            {
                case WindowMode.SingleWindow:
                    SetSingleWindowMode();
                    break;
                case WindowMode.FullScreen:
                    SetFullScreenMode();
                    break;
                case WindowMode.AllMonitors:
                    SetAllMonitorsMode();
                    break;
            }
        }

        private void SetSingleWindowMode()
        {
            // Set the window to be topmost
            SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, 0, 0,
                            0x0001 | 0x0002 | 0x0040);  // SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW

            // Make sure the window covers the entire screen
            int width = Display.main.systemWidth;
            int height = Display.main.systemHeight;
            SetWindowPos(windowHandle, IntPtr.Zero, 0, 0, width, height, 0x0040);
        }

        private void SetFullScreenMode()
        {
            RECT primaryMonitor = GetPrimaryMonitorBounds();
            int width = primaryMonitor.right - primaryMonitor.left;
            int height = primaryMonitor.bottom - primaryMonitor.top;

            SetWindowPos(windowHandle, HWND_TOPMOST,
                primaryMonitor.left, primaryMonitor.top,
                width, height,
                0x0040);  // SWP_SHOWWINDOW

        }

        private void SetAllMonitorsMode()
        {
            RECT combinedBounds = GetCombinedMonitorBounds();
            int width = combinedBounds.right - combinedBounds.left;
            int height = combinedBounds.bottom - combinedBounds.top;

            SetWindowPos(windowHandle, HWND_TOPMOST,
                combinedBounds.left, combinedBounds.top,
                width, height,
                0x0040);  // SWP_SHOWWINDOW
        }

        private RECT GetPrimaryMonitorBounds()
        {
            RECT primaryMonitor = new RECT();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData) =>
                {
                    primaryMonitor = lprcMonitor;
                    return false; // Stop after first monitor (primary)
                }, IntPtr.Zero);
            return primaryMonitor;
        }

        private RECT GetCombinedMonitorBounds()
        {
            var monitors = new List<RECT>();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData) =>
                {
                    monitors.Add(lprcMonitor);
                    return true;
                }, IntPtr.Zero);

            RECT combinedBounds = new RECT
            {
                left = int.MaxValue,
                top = int.MaxValue,
                right = int.MinValue,
                bottom = int.MinValue
            };

            foreach (var monitor in monitors)
            {
                combinedBounds.left = Math.Min(combinedBounds.left, monitor.left);
                combinedBounds.top = Math.Min(combinedBounds.top, monitor.top);
                combinedBounds.right = Math.Max(combinedBounds.right, monitor.right);
                combinedBounds.bottom = Math.Max(combinedBounds.bottom, monitor.bottom);
            }

            return combinedBounds;
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