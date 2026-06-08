# Kumo.WinUI.NavigationView.RailStyle

Rail-style `NavigationView` templates and helper attached properties for WinUI 3.

## Usage

Merge the resource dictionary after `XamlControlsResources`:

```xml
<ResourceDictionary.MergedDictionaries>
    <XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
    <ResourceDictionary Source="ms-appx:///Kumo.WinUI.NavigationView.RailStyle/Styles/CustomNavigationViewStyle.xaml" />
</ResourceDictionary.MergedDictionaries>
```

Apply the style explicitly:

```xml
<NavigationView Style="{StaticResource RailNavigationViewStyle}" />
```

Optional item helpers live in `Kumo.WinUI.NavigationView.RailStyle.Helpers.NavItemIconHelper`.

## Source Notice

`Styles/CustomNavigationViewStyle.xaml` is derived from Microsoft AI Dev Gallery's `NavigationView.xaml`, licensed under the MIT License.
