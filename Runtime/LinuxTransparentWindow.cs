using UnityEngine;

namespace RAStudio.TransparentOverlay
{
    /// <summary>
    /// Implements the transparent window functionality for Linux platform.
    /// </summary>
    public class LinuxTransparentWindow : ITransparentWindow
    {
        public void Initialize(WindowMode windowMode)
        {
            Debug.Log("Linux transparent window initialization not implemented.");
        }

        public void SetClickthrough(bool clickthrough)
        {
            Debug.Log("Linux SetClickthrough not implemented.");
        }

        public void Dispose()
        {
            // Cleanup code for Linux
        }
    }
}