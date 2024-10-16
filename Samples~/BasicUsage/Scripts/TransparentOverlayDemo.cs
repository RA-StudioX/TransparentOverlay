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
            UpdateButtonText(controller.CurrentUIMode == UIMode.Standard ? "Standard" : "UITK mode");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            var uimode = controller.CurrentUIMode == UIMode.Standard ? UIMode.UIToolkit : UIMode.Standard;
            controller.SwitchUIMode(uimode);
            Debug.Log("UI mode switched to " + uimode);
            UpdateButtonText(controller.CurrentUIMode == UIMode.Standard ? "Standard" : "UITK mode");
        }
    }

    void ToggleClickThrough()
    {
        if (controller != null)
        {
            UpdateButtonText(controller.CurrentUIMode == UIMode.Standard ? "Standard" : "UITK mode");
            Debug.Log("Click-through mode toggled");
        }
    }

    void UpdateButtonText(string text)
    {

        if (buttonText != null)
        {
            buttonText.text = text;
        }
        else
        {
            Debug.LogWarning("Button text not found");
        }
    }
}