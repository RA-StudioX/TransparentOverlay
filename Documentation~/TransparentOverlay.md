# Transparent Overlay

## Overview

The Transparent Overlay package allows you to create and manage transparent, click-through windows in your Unity applications. This can be particularly useful for overlays, HUDs, or any scenario where you want your application to interact with other windows on the user's desktop.

## Installation

To install this package, follow these steps:

1. Open the Unity Package Manager (Window > Package Manager)
2. Click the '+' button and select "Add package from git URL..."
3. Enter the following URL: `https://github.com/RAStudio/TransparentOverlay.git`
4. Click 'Add'

## Usage

1. Create a TransparentWindowConfig asset (Right-click in Project window > Create > RAStudio > Transparent Overlay > Window Config)
2. Add the TransparentWindowController component to a GameObject in your scene
3. Assign the TransparentWindowConfig asset to the TransparentWindowController in the Inspector
4. Configure the WindowMode and UIMode in the TransparentWindowConfig asset

## Components

### TransparentWindowManager

This is the core component that manages the transparent window functionality. It's implemented as a singleton and handles the platform-specific implementations.

### TransparentWindowController

This MonoBehaviour script acts as the interface between your Unity scene and the TransparentWindowManager. It should be attached to a GameObject in your scene.

### TransparentWindowConfig

This ScriptableObject allows you to configure the window mode and UI mode for your transparent window.

## Platform Support

- Windows: Fully implemented
- macOS: Placeholder implementation (requires platform-specific code)
- Linux: Placeholder implementation (requires platform-specific code)

## Samples

The package includes a BasicUsage sample demonstrating how to set up and use the Transparent Overlay in a simple scene.

## Troubleshooting

If you encounter issues:

1. Ensure you're running on a supported platform
2. Check that the TransparentWindowController is properly set up in your scene
3. Verify that your TransparentWindowConfig settings are correct

For more assistance, please file an issue on the GitHub repository.
