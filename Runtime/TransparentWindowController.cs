using UnityEngine;

namespace RAStudio.TransparentOverlay
{
    /// <summary>
    /// MonoBehaviour controller for the TransparentWindowManager.
    /// </summary>
    public class TransparentWindowController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The mode in which the transparent window should be initialized.")]
        private WindowMode windowMode = WindowMode.SingleWindow;
        
        [SerializeField]
        [Tooltip("The UI mode to use for click-through detection.")]
        private UIMode uiMode = UIMode.Standard;

        private TransparentWindowManager windowManager;

        /// <summary>
        /// Gets the current UI mode.
        /// </summary>
        public UIMode CurrentUIMode => uiMode;

        private void Start()
        {
            #if !UNITY_EDITOR
            windowManager = TransparentWindowManager.Instance;
            windowManager.Initialize(windowMode, uiMode);

            Application.runInBackground = true;
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = Color.clear;
            #endif
        }

        private void Update()
        {
            #if !UNITY_EDITOR
            windowManager.UpdateClickThrough();
            #endif
        }

        private void OnDestroy()
        {
            #if !UNITY_EDITOR
            windowManager.Dispose();
            #endif
        }

        /// <summary>
        /// Switches the UI mode at runtime.
        /// </summary>
        /// <param name="newMode">The new UI mode to set.</param>
        public void SwitchUIMode(UIMode newMode)
        {
            #if !UNITY_EDITOR
            uiMode = newMode;
            windowManager.SetUIMode(uiMode);
            #endif
        }
    }
}