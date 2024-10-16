using UnityEngine;

namespace RAStudio.TransparentOverlay
{
    /// <summary>
    /// Implements the transparent window functionality for macOS platform.
    /// </summary>
    public class MacOSTransparentWindow : ITransparentWindow
    {
        public void Initialize(WindowMode windowMode)
        {
            Debug.Log("MacOS transparent window initialization not implemented.");
        }

        public void SetClickthrough(bool clickthrough)
        {
            Debug.Log("MacOS SetClickthrough not implemented.");
        }

        public void Dispose()
        {
            // Cleanup code for MacOS
        }
    }
}