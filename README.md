SakuraUI Kit
========

This is an UI framework for Windows Universal app.

This project from Listen which is a Windows Phone app. SakuraUI contains many modern UI controls and UI classes. You can get many benefits from SakuraUI!

## UI Class ##

 - **ReactiveTheme**

    ReactiveTheme is a theme class that allow you change color and notify property changed to UIElement immediate. You can binding brush with UIElement, and change color in later. When color changed, ReactiveTheme will notify UIElements, and UIElements will take effect immediate.

 - **AnimationCreator**

    You can use this class to create some reusable animation without XAML code. If you use this helper class, please make surue your UIElement's RenderTransform is CompositeTransform or Projection is PlaneProjection.

![ReactiveTheme][1]

----------

## UI Controls ##

 - **FlyTextBlock**

    This is enhanced TextBlock. When you set new Text for it, this control will take a flying effect.

 - **RotationTextBlock**

    This is also enhanced TextBlock. When you set new Text for it, this control will take a rotation effect.

 - **CircularProgressBar**

    This is a progress bar with circle UI.
    
 - **MediaBasicController**

    This control from Listen app. It contains many Commands and CommandParameters for your Binding, and it also raise event when user taps button. 

![SakuraUI Kit Controls][2]

----------

## Utilities ##

 - **SettingsHelper**

    SettingsHelper can help you persisting your app settings. You can just use LocalSettingsHelper or RoamingSettingsHelper to save settings in local or cloud. Or you  can also use SettingsHelper to save other information with ApplicationDataContainer. These classes are so powerful and convenient.

 - **ActionCommand**

    This class from UltraLightMvvm in codeplex. You can use it to create command-binding in ViewModel.

 - **LINQ to VisualTree**

    LINQ to VisualTree is a powerful tool to get a UIElemeent from XAML visual tree. You can use Select, Where, DesendantsAs, ... to get UIElement(s) what you want.

 - **MessagePublisher**
    MessagePublisher is a class for publishing and receiving message in app. You can register your instance to receive message from other instances, and you can also send message to other instances by DEFAULT message publisher. 

  [1]: http://i1.tietuku.com/ca015991fe8c095f.png
  [2]: http://i1.tietuku.com/c6755226688d4f57.png
    
To be continued...
