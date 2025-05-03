using MinimalisticWPF.SourceGeneratorMark;
using MinimalisticWPF.Theme;
using System.Windows;

namespace MinimalisticWPF.Controls
{
    public partial class NotificationBox : Window
    {
        private const string T1 = "⇢ Make a choice";
        private const string T2 = "⇢ Make a confirmation";

        private bool _cancontinue = true;

        public static bool Choose(string question,string title = T1)
        {   
            var box = new NotificationBox()
            {
                Text = question,
                Topmost = true,
                MessageName = title,
            };
            box.ShowDialog();
            return box._cancontinue;
        }
        public static void Confirm(string message, string title = T2)
        {   
            var box = new NotificationBox()
            {
                Text = message,
                RightButtonWidth = 0,
                Topmost = true,
                MessageName = title,
            };
            box.ShowDialog();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _cancontinue = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _cancontinue = false;
            Close();
        }
    } // Interaction

    public partial class NotificationBox
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NotificationBox), new PropertyMetadata(new CornerRadius(10)));

        public string MessageName
        {
            get { return (string)GetValue(MessageNameProperty); }
            set { SetValue(MessageNameProperty, value); }
        }
        public static readonly DependencyProperty MessageNameProperty =
            DependencyProperty.Register("MessageName", typeof(string), typeof(NotificationBox), new PropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NotificationBox), new PropertyMetadata(string.Empty));

        public double RightButtonWidth
        {
            get { return (double)GetValue(RightButtonWidthProperty); }
            set { SetValue(RightButtonWidthProperty, value); }
        }
        public static readonly DependencyProperty RightButtonWidthProperty =
            DependencyProperty.Register("RightButtonWidth", typeof(double), typeof(NotificationBox), new PropertyMetadata(200d));

    } // DependencyProperty

    [Theme(nameof(Background), typeof(Light), ["#BEFFFFFF"])]
    [Theme(nameof(Background), typeof(Dark), ["#BE000000"])]
    [Theme(nameof(BorderBrush), typeof(Light), ["#BE000000"])]
    [Theme(nameof(BorderBrush), typeof(Dark), ["#BEFFFFFF"])]
    [Theme(nameof(Foreground), typeof(Light), ["Black"])]
    [Theme(nameof(Foreground), typeof(Dark), ["White"])]
    public partial class NotificationBox
    {

    } // Effect
}
