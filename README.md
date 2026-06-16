# WinUI Rail NavigationView

Rail style NavigationView for WinUI and UWP.

[![WinUI NuGet](https://img.shields.io/nuget/v/Kumo.WinUI.NavigationView.RailStyle.svg?label=WinUI%203)](https://www.nuget.org/packages/Kumo.WinUI.NavigationView.RailStyle)
[![UWP NuGet](https://img.shields.io/nuget/v/Kumo.Uwp.NavigationView.RailStyle.svg?label=UWP)](https://www.nuget.org/packages/Kumo.Uwp.NavigationView.RailStyle)

![Rail style NavigationView](winui_demo.png)

## Usage

Merge the resource dictionary after `XamlControlsResources`.

```xml
<ResourceDictionary.MergedDictionaries>
    ...
    <RailNavigationViewResources xmlns="using:Kumo.WinUI.NavigationView.RailStyle" />
</ResourceDictionary.MergedDictionaries>
```

Apply the style explicitly:

```xml
<NavigationView Style="{StaticResource RailNavigationViewStyle}" />
```

## Samples

- `WinUIApp` demonstrates the WinUI 3 package and a title-bar back button connected to `Frame` navigation.
- `UwpApp` demonstrates the UWP package.

## Source Notice

The rail `NavigationView` template is derived from Microsoft AI Dev Gallery's `NavigationView.xaml`, licensed under the MIT License.
