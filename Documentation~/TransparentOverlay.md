# Transparent Overlay for Unity

## Overview

Transparent Overlay is a Unity package that allows you to create and manage transparent, click-through windows in your Unity applications. This can be particularly useful for overlays, HUDs, or any scenario where you want your application to interact with other windows on the user's desktop.

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

## Components

### TransparentWindowController

This MonoBehaviour script acts as the interface between your Unity scene and the TransparentWindowManager. It should be attached to a GameObject in your scene.

#### Properties

- `WindowMode`: Determines how the transparent window is initialized (SingleWindow, AllMonitors, or FullScreen).
- `UIMode`: Determines the method used for click-through detection (Standard or UIToolkit).
- `CurrentUIMode`: Gets the current UI mode.

#### Methods

- `SwitchUIMode(UIMode newMode)`: Switches the UI mode at runtime.

### TransparentWindowManager

This is the core component that manages the transparent window functionality. It's implemented as a singleton and handles the platform-specific implementations.

#### Methods

- `Initialize(WindowMode windowMode, UIMode uiMode)`: Initializes the transparent window with specified modes.
- `SetUIMode(UIMode mode)`: Sets the UI mode for click-through detection.
- `UpdateClickThrough()`: Updates the click-through state of the window.

## Platform Support

- Windows: Fully implemented
- macOS: Placeholder implementation (requires platform-specific code)
- Linux: Placeholder implementation (requires platform-specific code)

## Example Usage

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
