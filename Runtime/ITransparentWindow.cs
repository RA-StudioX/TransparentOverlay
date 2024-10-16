using System;

namespace RAStudio.TransparentOverlay
{
    /// <summary>
    /// Defines the interface for platform-specific transparent window implementations.
    /// </summary>
    public interface ITransparentWindow : IDisposable
    {
        /// <summary>
        /// Initializes the transparent window.
        /// </summary>
        /// <param name="windowMode">The mode in which the window should be initialized.</param>
        void Initialize(WindowMode windowMode);

        /// <summary>
        /// Sets the click-through state of the window.
        /// </summary>
        /// <param name="clickthrough">If true, enables click-through; if false, disables it.</param>
        void SetClickthrough(bool clickthrough);
    }

    /// <summary>
    /// Defines the available window modes for the transparent overlay.
    /// </summary>
    public enum WindowMode
    {
        /// <summary>
        /// Creates a single transparent window.
        /// </summary>
        SingleWindow,

        /// <summary>
        /// Creates a transparent window across all monitors.
        /// </summary>
        AllMonitors,

        /// <summary>
        /// Creates a full-screen transparent window.
        /// </summary>
        FullScreen
    }

    /// <summary>
    /// Defines the UI modes for click-through detection.
    /// </summary>
    public enum UIMode
    {
        /// <summary>
        /// Uses Unity's standard UI system for click-through detection.
        /// </summary>
        Standard,

        /// <summary>
        /// Uses Unity's UI Toolkit for click-through detection.
        /// </summary>
        UIToolkit
    }
}