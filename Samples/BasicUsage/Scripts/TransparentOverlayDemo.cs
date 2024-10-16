using UnityEngine;
using UnityEngine.UI;
using RAStudio.TransparentOverlay;

public class TransparentOverlayDemo : MonoBehaviour
{
    private TransparentWindowController controller;
    private Button button;
    private Text buttonText;

    void Start()
    {
        controller = FindFirstObjectByType<TransparentWindowController>();
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();

        if (controller != null && button != null)
        {
            button.onClick.AddListener(ToggleClickThrough);
            UpdateButtonText();
        }
    }

    void ToggleClickThrough()
    {
        if (controller != null)
        {
            UIMode newMode = controller.CurrentUIMode == UIMode.Standard ? UIMode.UIToolkit : UIMode.Standard;
            controller.SwitchUIMode(newMode);
            UpdateButtonText();
        }
    }

    void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = controller.CurrentUIMode == UIMode.Standard ? "Enable Click-Through" : "Disable Click-Through";
        }
    }
}