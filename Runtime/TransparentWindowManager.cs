using System;
using UnityEngine;

namespace RAStudio.TransparentOverlay
{
    public class TransparentWindowManager : IDisposable
    {
        private static TransparentWindowManager _instance;
        public static TransparentWindowManager Instance
        {
            get
            {
                #if !UNITY_EDITOR
                if (_instance == null)
                {
                    _instance = new TransparentWindowManager();
                }
                return _instance;
                #else
                return null;
                #endif
            }
        }

        private ITransparentWindow transparentWindow;
        private UIMode currentUIMode;
        private Func<bool> shouldClickThrough;
        private bool disposed = false;

        private TransparentWindowManager() { }

        public void Initialize(WindowMode windowMode = WindowMode.FullScreen, UIMode uiMode = UIMode.UIToolkit)
        {
                #if !UNITY_EDITOR
#if UNITY_STANDALONE_WIN
            transparentWindow = new WindowsTransparentWindow();
#elif UNITY_STANDALONE_OSX
                transparentWindow = new MacOSTransparentWindow();
#elif UNITY_STANDALONE_LINUX
                transparentWindow = new LinuxTransparentWindow();
#else
                Debug.LogWarning("Transparent window not supported on this platform");
                return;
#endif

            transparentWindow.Initialize(windowMode);
            SetUIMode(uiMode);
                #endif
        }

        public void SetUIMode(UIMode mode)
        {
            #if !UNITY_EDITOR
            currentUIMode = mode;
            switch (mode)
            {
                case UIMode.Standard:
                    shouldClickThrough = () => Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) == null;
                    break;
                case UIMode.UIToolkit:
                    shouldClickThrough = () => !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                    break;
            }
            #endif
        }

        public void UpdateClickThrough()
        {
            #if !UNITY_EDITOR
            transparentWindow?.SetClickthrough(shouldClickThrough());
            #endif
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            #if !UNITY_EDITOR
            if (!disposed)
            {
                if (disposing)
                {
                    transparentWindow?.Dispose();
                }

                disposed = true;
            }
            #endif
        }

        ~TransparentWindowManager()
        {
            Dispose(false);
        }
    }
}