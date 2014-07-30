SakuraUI Kit
========

This is an UI framework for Windows Universal app.

This project from Listen which is a Windows Phone app. SakuraUI contains many modern UI controls and UI classes. You can get many benefits from SakuraUI!

## UI Class ##

**ReactiveTheme**

ReactiveTheme is a theme class that allow you change color and notify property changed to UIElement immediate. You can binding brush with UIElement, and change color in later. When color changed, ReactiveTheme will notify color changed, and the UIElement will take effect immediate.

![ReactiveTheme][1]

----------

## UI Controls ##

**FlyTextBlock**

This is enhanced TextBlock. When you set new Text for it, this control will take a flying effect.

**RotationTextBlock**

This is also enhanced TextBlock. When you set new Text for it, this control will take a rotation effect.

**CircularProgressBar**

This is a progress bar with circle UI.

![SakuraUI Kit Controls][2]

----------

## Utilities ##

**SettingsHelper**

SettingsHelper can help you persisting your app settings. You can just use LocalSettingsHelper or RoamingSettingsHelper to save settings in local or cloud. Or you  can also use SettingsHelper to save other information with ApplicationDataContainer. These classes are so powerful and convenient.

**ActionCommand**

This class from UltraLightMvvm in codeplex. You can use it to create command-binding in ViewModel.

**LINQ to VisualTree**

LINQ to VisualTree is a powerful tool to get a UIElemeent from XAML visual tree. You can use Select, Where, DesendantsAs, ... to get UIElement(s) what you want.


  [1]: http://i1.tietuku.com/ca015991fe8c095f.png
  [2]: http://i1.tietuku.com/c6755226688d4f57.png
    
To be continued...
