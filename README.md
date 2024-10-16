<p align="center">
    <a href="https://ra-studio.net" target="_blank">
        <img src="Images/RAStudio-logo.svg" alt="RA Studio Logo" width="200"/>
    </a>
</p>

# Transparent Overlay

## Description

Transparent Overlay is a Unity package that enables the creation and management of transparent, click-through windows in Unity applications. It's particularly useful for creating overlays, HUDs, or any scenario where you want your Unity application to interact with other windows on the user's desktop.

## Features

- Create transparent, click-through windows
- Support for different window modes (SingleWindow, AllMonitors, FullScreen)
- Easy integration with both standard Unity UI and UI Toolkit
- Platform-specific implementations (Windows fully supported, placeholders for macOS and Linux)

## Installation

To install this package, follow these steps:

1. Open the Unity Package Manager (Window > Package Manager)
2. Click the '+' button and select "Add package from git URL..."
3. Enter the following URL: `https://github.com/RA-StudioX/TransparentOverlay.git`
4. Click 'Add'

## Quick Start

1. Add a new empty GameObject to your scene and name it "TransparentOverlayManager".
2. Add the TransparentWindowController component to this GameObject.
3. Configure the Window Mode and UI Mode in the Inspector as needed.

## Important Note

- The transparent window functionality is designed to work only in built applications, not within the Unity Editor. This is to prevent unexpected behavior when modifying window properties in the Editor environment.
- To use Transparent window with ui toolkit need to add EventSystem to the same scene
- There is convenient sample scene and script for you.

## Usage

Here's a simple example of how to toggle click-through behavior:

```csharp
using UnityEngine;
using RAStudio.TransparentOverlay;

public class ClickThroughToggle : MonoBehaviour
{
    private TransparentWindowController controller;

    void Start()
    {
        controller = FindObjectOfType<TransparentWindowController>();
    }

    public void ToggleClickThrough()
    {
        if (controller != null)
        {
            UIMode newMode = controller.CurrentUIMode == UIMode.Standard ? UIMode.UIToolkit : UIMode.Standard;
            controller.SwitchUIMode(newMode);
        }
    }
}
```

## Documentation

For more detailed information about the package, its components, and how to use them, please refer to the [full documentation](https://github.com/RA-StudioX/TransparentOverlay/blob/main/Documentation~/TransparentOverlay.md).

## Requirements

- Unity 2021.3 or later

## Contributing

Contributions are welcome! If you have any ideas, suggestions, or find bugs, feel free to open an issue or submit a pull request.

## License

This package is licensed under the MIT License. See the [LICENSE](https://github.com/RA-StudioX/TransparentOverlay/blob/main/LICENSE.md) file for details.

## Author

Rafael Azriaiev

- Email: contact@ra-studio.net
- Website: https://ra-studio.net
- GitHub: https://github.com/RA-StudioX

## Support

If you encounter any issues or have questions, please file an issue on the [GitHub repository](https://github.com/RA-StudioX/TransparentOverlay/issues).

## Community

Join our community on [Discord](https://discord.gg/cBdHEGjR) to get help, share ideas, and collaborate with other developers.
