# MinimalisticWPF.Controls

此项目仅演示如何使用[MinimalisticWPF](#https://github.com/Axvser/MinimalisticWPF)设计`用户控件`

[![GitHub](https://img.shields.io/badge/GitHub-Repository-blue?logo=github)](https://github.com/Axvser/MinimalisticWPF.Controls)  
[![NuGet](https://img.shields.io/nuget/v/MinimalisticWPF.Controls?color=green&logo=nuget)](https://www.nuget.org/packages/MinimalisticWPF.Controls/)

( 注意 : 这仅演示局部功能,完整功能见MinimalisticWPF的文档 )

( 注意 : 若安装此项目,则MinimalisticWPF也将作为可传递包被安装 )

---

## 特点 ✨
只需要为类标记一些特性，就可以自动实现下述功能
- `主题切换`动画&行为实现
- `悬停交互`动画&行为实现
- `帧循环`驱动的行为
- `构造器`自动化

---

## 用户控件 🚀

> 添加如下引用以使用这些用户控件

```xml
xmlns:views="clr-namespace:MinimalisticWPF.Controls;assembly=MinimalisticWPF.Controls"
```

- **[Button](#Button)** 
- **[NotificationBox](#NotificationBox)**
- **[HotKeyBox](#HotKeyBox)**
- **[ScrollViewer](#ScrollViewer)**
- **[FlowingCard](#FlowingCard)**

> 这些控件支持主题切换,下述代码演示了如何`手动切换主题`或`跟随系统主题`

1.是/否跟随系统主题

```csharp
public partial class App : Application
{
   public App()
   {
      DynamicTheme.FollowSystem(typeof(Dark)); // 跟随系统,若系统主题读取失败,则使用Dark主题

      DynamicTheme.StartWith(typeof(Dark)); // 手动指定,不跟随系统,选择此项后,请手动切换主题
   }
}
```

2.手动切换主题

```csharp
DynamicTheme.Apply(typeof(Light));
```

---

### Button ✨

> 按钮很常见,并且具备比较多的交互行为,本项目探索了一种简约而美观的Button实现方案。例如,Data属性可以自动解析路径或文本作为按钮的内容;鼠标悬停时,可以让按钮的内容转动起来。

1.按钮样式

> 特性被标记后,这些依赖属性就会自动生成,修改它们能直接影响按钮的外观。你还可在特性中直接传入参数用以构造这些依赖属性的初始值而不再需要全部在Style中声明。当然，这里为了完全展现MinimalisticWPF的源生成功能，我将所有自动产生的依赖属性都写进Style了。

```xml
        <Style TargetType="views:Button" x:Key="nicebutton">
            <!--1.深色主题效果选项-->
            <!--1.1悬停-->
            <Setter Property="DarkHoveredBackground" Value="#1e1e1e"/>
            <Setter Property="DarkHoveredForeground" Value="Red"/>
            <Setter Property="DarkHoveredBorderBrush" Value="White"/>
            <!--1.2非悬停-->
            <Setter Property="DarkNoHoveredBackground" Value="#1e1e1e"/>
            <Setter Property="DarkNoHoveredForeground" Value="White"/>
            <Setter Property="DarkNoHoveredBorderBrush" Value="White"/>
            
            <!--2.亮色主题效果选项-->
            <!--2.1悬停-->
            <Setter Property="LightHoveredBackground" Value="White"/>
            <Setter Property="LightHoveredForeground" Value="Cyan"/>
            <Setter Property="LightHoveredBorderBrush" Value="#1e1e1e"/>
            <!--2.2非悬停-->
            <Setter Property="LightNoHoveredBackground" Value="White"/>
            <Setter Property="LightNoHoveredForeground" Value="#1e1e1e"/>
            <Setter Property="LightNoHoveredBorderBrush" Value="#1e1e1e"/>
            
            <!--3.仅应用悬停的效果-->
            <!--3.1悬停-->
            <Setter Property="HoveredBorderThickness" Value="1"/>
            <Setter Property="HoveredCornerRadius" Value="5"/>
            <!--3.2非悬停-->
            <Setter Property="NoHoveredBorderThickness" Value="1"/>
            <Setter Property="NoHoveredCornerRadius" Value="0"/>
        </Style>
```
2.使用按钮
- `AllowHoverRotate` : 允许按钮在悬停时旋转
- `Data` : 使用`文本`或`路径`数据作为按钮内容 ( 尝试解析为Path失败后会直接使用文本 )
- `ContentScale` : 按钮内容缩放比例 ( 内容大小按此比例自动计算 )
```xml
<views:Button Style="{StaticResource nicebutton}" 
              Width="100" Height="100" 
              AllowHoverRotate="True" 
              ContentScale="1"
              Data="M15.75 21.495c0 .41.34.75.75.75l-.01.01c.98 0 1.47 0 1.93-.09a4.73 4.73 0 0 0 3.73-3.73c.09-.46.09-.95.09-1.93c0-.41-.34-.75-.75-.75s-.75.34-.75.75c0 .88 0 1.32-.06 1.63a3.24 3.24 0 0 1-2.55 2.55c-.31.06-.75.06-1.63.06c-.41 0-.75.34-.75.75m-8.25.75l-.01-.01c.41 0 .75-.34.75-.75s-.34-.75-.75-.75c-.87 0-1.32 0-1.63-.06a3.24 3.24 0 0 1-2.55-2.55c-.06-.31-.06-.75-.06-1.63c0-.41-.34-.75-.75-.75s-.75.34-.75.75c0 .98 0 1.47.09 1.93a4.74 4.74 0 0 0 3.73 3.73c.46.09.95.09 1.93.09m13.25-14.75c0 .41.34.75.75.75l.01.01c.41 0 .75-.34.75-.75c0-.98 0-1.47-.09-1.93a4.74 4.74 0 0 0-3.73-3.73c-.46-.09-.95-.09-1.93-.09c-.41 0-.75.34-.75.75s.34.75.75.75c.87 0 1.32 0 1.63.06c1.29.25 2.29 1.26 2.55 2.55c.06.31.06.75.06 1.63m-19 0c0 .41.34.75.75.75l.01-.01c.41 0 .75-.34.75-.75c0-.88 0-1.32.06-1.63a3.24 3.24 0 0 1 2.55-2.55c.31-.06.75-.06 1.63-.06c.41 0 .75-.34.75-.75s-.34-.75-.75-.75c-.98 0-1.47 0-1.93.09a4.73 4.73 0 0 0-3.73 3.73c-.09.46-.09.95-.09 1.93m9.25 9.75h2c2.02 0 3.14 0 3.94-.81c.81-.8.81-1.92.81-3.94s-.01-3.13-.81-3.94s-1.92-.81-3.94-.81h-.25v-1.25c0-.41-.34-.75-.75-.75s-.75.34-.75.75v1.25H11c-2.02 0-3.14 0-3.94.81c-.81.8-.81 1.92-.81 3.94s.01 3.13.81 3.94s1.92.81 3.94.81m-2.88-7.63c.35-.36 1.13-.37 2.88-.37h2c1.74 0 2.53.02 2.88.37c.36.35.37 1.14.37 2.88s-.02 2.53-.37 2.88c-.35.36-1.13.37-2.88.37h-2c-1.74 0-2.53-.02-2.88-.37c-.36-.35-.37-1.14-.37-2.88s.02-2.53.37-2.88m5.13 2.38c0 .41.34.75.75.75s.75-.34.75-.75v-.5c0-.41-.34-.75-.75-.75s-.75.34-.75.75zm-4 0c0 .41.34.75.75.75s.75-.34.75-.75v-.5c0-.41-.34-.75-.75-.75s-.75.34-.75.75z"/>
```

---

### NotificationBox ✨

> 现代化风格的通知框

```csharp
Loaded += (s, e) => NotificationBox.Choose("确定要继续吗?");
Loaded += (s, e) => NotificationBox.Confirm("操作已成功执行!");
```

---

### HotKeyBox ✨

> 在做一些快捷方式的设置界面时,我们可能需要一个用于接收用户输入的控件,并且用户输入完毕后,自动注册/注销快捷方式

1.快捷方式管理器样式

这里与`Button`的样式类似,假设Key为`nicehotkeybox`,细节省略…

2.使用快捷方式管理器
- `HotKeyInvoked` : 快捷方式被触发时的事件
- `TextScale` : 快捷方式文本缩放比例 ( 文本大小按此比例自动计算 )
- 自动确保快捷方式的`唯一性`

```xml
        <views:HotKeyBox Style="{StaticResource nicehotkeybox}"
                         Width="340" Height="100"
                         TextScale="0.618"
                         HotKeyInvoked="HotKeyBox_HotKeyInvoked"/>
```

```csharp
        private void HotKeyBox_HotKeyInvoked(object? sender, HotKeyEventArgs e)
        {
            // 处理快捷键触发事件
            MessageBox.Show("快捷方式被触发!");

            // 全局快捷键的sender为null
            // 局部快捷键的sender不为null
            // e传递了快捷键的信息
        }
```

### ScrollViewer ✨

> 极简风格的ScrollViewer,支持主题切换、滚轮缩放

### FlowingCard ✨

> 带有流光效果的卡片（ 或者说Grid ），支持主题切换


