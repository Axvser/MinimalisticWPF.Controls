# MinimalisticWPF.Controls

###### Design some user controls based on [ MinimalisticWPF ]

The project aims to explore good practices for implementing front-ends in WPF projects through the use of C#

- Almost 90% of the work of designing user controls is done by using C#
  - 25% by yourself
  - 65% by source generator
- Follow the MVVM design pattern
  - partial ViewModel_Data _> abstract the business data of the control
  - partial ViewModel_Style _> abstract the style data of the control
  - partial ¡­¡­ _> by splitting a ViewModel into multiple parts, the development work on the front and back end is split into those parts to achieve separation

It is a validation of the usability of the [ MinimalisticWPF ](https://github.com/Axvser/MinimalisticWPF) library

---

## UserControls _>

```xml
xmlns:mn="clr-namespace:MinimalisticWPF.Controls;assembly=MinimalisticWPF.Controls"
```

- [Button](#Button)
- [ProgressBar](#ProgressBar)
- ¡­¡­ Under development

---

## ¢ñ Button

Just set the dependeny properties and your buttons will look different in different themes
- With theme switching effects
- Has hover effects

```xml
<Style TargetType="mn:Button" x:Key="ButtonWithDynamicTheme">
    <!--Dark-->
    <Setter Property="DarkHoveredForeground" Value="White"/>
    <Setter Property="DarkNoHoveredForeground" Value="White"/>
    <Setter Property="DarkHoveredBorderBrush" Value="Cyan"/>
    <Setter Property="DarkNoHoveredBorderBrush" Value="White"/>
    <Setter Property="DarkHoveredBackground" Value="White"/>
    <!--Light-->
    <Setter Property="LightHoveredForeground" Value="Violet"/>
    <Setter Property="LightNoHoveredForeground" Value="#1e1e1e"/>
    <Setter Property="LightHoveredBorderBrush" Value="Violet"/>
    <Setter Property="LightNoHoveredBorderBrush" Value="#1e1e1e"/>
    <Setter Property="LightHoveredBackground" Value="#1e1e1e"/>
    <!--NoTheme-->
    <Setter Property="HoveredBorderThickness" Value="1.6,0,0,0"/>
    <Setter Property="NoHoveredBorderThickness" Value="0"/>
    <Setter Property="HoveredCornerRadius" Value="5"/>
    <Setter Property="NoHoveredCornerRadius" Value="0"/>
    <Setter Property="HoveredBackOpacity" Value="0.2"/>
    <Setter Property="NoHoveredBackOpacity" Value="0"/>
</Style>
```

Apply the Style

```xml
<mn:Button Text="MyButton" Click="Button_Click" Style="{StaticResource ButtonWithDynamicTheme}"/>
```

## ¢ò ProgressBar

A more flexible look
- Support to switch between ring and bar
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
                ForeFill="Cyan"
                BackFill="Gray"/>
```