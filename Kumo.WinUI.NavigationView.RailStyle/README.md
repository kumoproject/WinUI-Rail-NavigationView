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

`Styles/CustomNavigationViewStyle.xaml` and `Helpers/NavItemIconHelper.cs` are derived from Microsoft PowerToys ShortcutGuide sources at commit `4f14070a1aaf634f0663e51727b3ccf05f62e7cc`, licensed under the MIT License.
