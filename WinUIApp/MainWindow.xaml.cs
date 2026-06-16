using System;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.UI.ViewManagement;
using Muxc = Microsoft.UI.Xaml.Controls;

namespace WinUIApp;

public sealed partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        SetTitleBar();

        NavView.SelectedItem = NavView.MenuItems[0];
        Navigate("Home");
    }

    private void SetTitleBar()
    {
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(titleBar);
        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Standard;

        if (Content is FrameworkElement rootElement)
        {
            rootElement.ActualThemeChanged += (_, _) => UpdateCaptionButtonColors();
        }

        UpdateCaptionButtonColors();
    }

    private void UpdateCaptionButtonColors()
    {
        var captionTitleBar = AppWindow?.TitleBar;
        if (captionTitleBar is null)
        {
            return;
        }

        if (new AccessibilitySettings().HighContrast)
        {
            captionTitleBar.ButtonForegroundColor = null;
            captionTitleBar.ButtonHoverForegroundColor = null;
            captionTitleBar.ButtonHoverBackgroundColor = null;
            return;
        }

        var theme = (Content as FrameworkElement)?.ActualTheme
            ?? (App.Current.RequestedTheme == ApplicationTheme.Dark ? ElementTheme.Dark : ElementTheme.Light);

        var foreground = theme == ElementTheme.Dark ? Microsoft.UI.Colors.White : Microsoft.UI.Colors.Black;
        captionTitleBar.ButtonForegroundColor = foreground;
        captionTitleBar.ButtonHoverForegroundColor = foreground;
        captionTitleBar.ButtonHoverBackgroundColor = theme == ElementTheme.Dark
            ? Windows.UI.Color.FromArgb(24, 255, 255, 255)
            : Windows.UI.Color.FromArgb(24, 0, 0, 0);
    }

    private void NavView_ItemInvoked(Muxc.NavigationView sender, Muxc.NavigationViewItemInvokedEventArgs args)
    {
        if (args.InvokedItemContainer?.Tag is string tag)
        {
            Navigate(tag);
        }
    }

    private void Navigate(string tag)
    {
        if (NavFrame.Content is DemoPage currentPage
            && string.Equals(currentPage.PageTag, tag, StringComparison.Ordinal))
        {
            return;
        }

        NavFrame.Navigate(typeof(DemoPage), tag);
    }

    private void TitleBar_BackRequested(Muxc.TitleBar sender, object args)
    {
        if (NavFrame.CanGoBack)
        {
            NavFrame.GoBack();
        }
    }

    private void NavFrame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        var tag = e.Parameter as string
            ?? (NavFrame.Content as DemoPage)?.PageTag
            ?? "Home";

        titleBar.Title = DemoPage.GetTitle(tag);
        titleBar.IsBackButtonVisible = NavFrame.CanGoBack;
        titleBarIcon.Margin = NavFrame.CanGoBack ? new Thickness(0, 0, 8, 0) : new Thickness(16, 0, 0, 0);
        SelectNavigationItem(tag);
    }

    private void SelectNavigationItem(string tag)
    {
        var item = FindNavigationItem(tag);
        if (item is not null)
        {
            NavView.SelectedItem = item;
        }
    }

    private Muxc.NavigationViewItem? FindNavigationItem(string tag)
    {
        foreach (var item in NavView.MenuItems)
        {
            if (item is Muxc.NavigationViewItem navigationItem
                && string.Equals(navigationItem.Tag as string, tag, StringComparison.Ordinal))
            {
                return navigationItem;
            }
        }

        foreach (var item in NavView.FooterMenuItems)
        {
            if (item is Muxc.NavigationViewItem navigationItem
                && string.Equals(navigationItem.Tag as string, tag, StringComparison.Ordinal))
            {
                return navigationItem;
            }
        }

        return null;
    }
}
