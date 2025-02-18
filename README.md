# MinimalisticWPF.Controls

The project aims to explore good practices for implementing front-ends in WPF projects through the use of C#

- Complex front-end effects can be achieved with a small amount of C# code
- Follow the MVVM design pattern

Go ¡ú

- [github](https://github.com/Axvser/MinimalisticWPF.Controls)
- [nuget](https://www.nuget.org/packages/MinimalisticWPF.Controls/)

Dependency ¡ú

[MinimalisticWPF](https://github.com/Axvser/MinimalisticWPF)

Versions ¡ú

[V1.2.0](#) `LTS`

---

## `UserControls`

```xml
xmlns:mn="clr-namespace:MinimalisticWPF.Controls;assembly=MinimalisticWPF.Controls"
```

- [Button](#Button)
- [ProgressBar](#ProgressBar)
- [TextBox](#TextBox)
- [TopBar](#TopBar)
- [Notification](#Notification)

Not all controls are very extensible, this project is just a demonstration of how you can use the `MinimalisticWPF` library to build user controls

---

## Button

Use the following template to set the appearance of your buttons

```xml
  <Style TargetType="mn:Button" x:Key="ButtonWithDynamicTheme">
      <!--Dark-->
      <Setter Property="DarkHoveredForeground" Value="White"/>
      <Setter Property="DarkNoHoveredForeground" Value="White"/>
      <Setter Property="DarkHoveredBorderBrush" Value="Cyan"/>
      <Setter Property="DarkNoHoveredBorderBrush" Value="White"/>
      <Setter Property="DarkHoveredBackground" Value="White"/>
      <!--Light-->
      <Setter Property="LightHoveredForeground" Value="Red"/>
      <Setter Property="LightNoHoveredForeground" Value="#1e1e1e"/>
      <Setter Property="LightHoveredBorderBrush" Value="Red"/>
      <Setter Property="LightNoHoveredBorderBrush" Value="#1e1e1e"/>
      <Setter Property="LightHoveredBackground" Value="#1e1e1e"/>
      <!--NoTheme-->
      <Setter Property="HoveredBorderThickness" Value="30,0,0,0"/>
      <Setter Property="NoHoveredBorderThickness" Value="1,0,0,0"/>
      <Setter Property="HoveredCornerRadius" Value="0"/>
      <Setter Property="NoHoveredCornerRadius" Value="0"/>
      <Setter Property="HoveredBackOpacity" Value="0.2"/>
      <Setter Property="NoHoveredBackOpacity" Value="0"/>
  </Style>
```

`Apply the Style`

```xml
<mn:Button Text="MyButton" Click="Button_Click" Style="{StaticResource ButtonWithDynamicTheme}"/>
```

---

## ProgressBar

A more flexible look
- Support to switch between `ring and line`
- The Angle of the ring is adjustable
- Can be flipped on the X or Y axis

```xml
<mn:ProgressBar Size="200"
                Thickness="5"  
                StartAngle="90"
                EndAngle="270"
                Shape="Ring"
                Progress="0.3"
                FlipX="True"
                FlipY="False"
                DarkForeFill="Cyan"
                DarkBackFill="Gray"
                LightForeFill="Violet"          
                LightBackFill="Gray"/>
```

---

## TextBox

Font size is automatically adjusted and all input fields share a theme

```xml
            <mn:TextBox OnTextChanged="TextBox_OnTextChanged"
                        DarkBackground="#32FFFFFF"
                        LightBackground="#32000000"
                        DarkBorderBrush="White"
                        LightBorderBrush="Black"
                        DarkCaretBrush="Cyan"
                        LightCaretBrush="Lime"
                        DarkForeground="White"
                        LightForeground="Black"
                        HoveredMargin="0"
                        NoHoveredMargin="3"/>
```

There is also a read-only `Text` property to get the current text content
 
---

## TopBar

This can help you quickly replace the zoom interaction provided by the default window

```xml
  <Style TargetType="mn:TopBar" x:Key="MyTopBar">
            <Setter Property="DarkTitleBrush" Value="Cyan"/>
            <Setter Property="LightTitleBrush" Value="Violet"/>
  </Style>

  <Style TargetType="mn:Button" x:Key="ButtonInTopBar">
      <!--Dark-->
      <Setter Property="DarkHoveredForeground" Value="Cyan"/>
      <Setter Property="DarkNoHoveredForeground" Value="White"/>
      <!--Light-->
      <Setter Property="LightHoveredForeground" Value="Red"/>
      <Setter Property="LightNoHoveredForeground" Value="Black"/>
  </Style>

  <mn:TopBar VerticalAlignment="Top" Height="50"
             Style="{StaticResource MyTopBar}"
             ButtonStyle="{StaticResource ResourceKey=ButtonInTopBar}"/>
```

---

## Notification

Glass style notification window, supporting light and dark theme.
- `await` Warn() / Ask() / Message()
- The bool value indicates whether the window is at the top
 
```csharp
  private async void Button_Click_3(object sender, RoutedEventArgs e)
  {
      if (await Notification.Warn("This can lead to a catastrophic error! Are you sure you want to continue?",true))
      {
          MessageBox.Show("You choose to continue");
      }
      else
      {
          MessageBox.Show("You chose to cancel");
      }
  }
```

---