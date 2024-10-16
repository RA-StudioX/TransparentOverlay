using UnityEditor;
using UnityEngine;

namespace RAStudio.TransparentOverlay.Editor
{
    /// <summary>
    /// Custom editor for the TransparentWindowController.
    /// </summary>
    [CustomEditor(typeof(TransparentWindowController))]
    public class TransparentWindowControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TransparentWindowController controller = (TransparentWindowController)target;

            if (GUILayout.Button("Switch UI Mode"))
            {
                if (!Application.isPlaying)
                {
                    Debug.LogWarning("Switching UI mode is only available at runtime.");
                    return;
                }

                UIMode newMode = controller.CurrentUIMode == UIMode.Standard
                    ? UIMode.UIToolkit
                    : UIMode.Standard;
                controller.SwitchUIMode(newMode);
            }
        }
    }
}