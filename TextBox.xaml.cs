using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MinimalisticWPF.Controls
{
    [DataContextConfig(nameof(TextBox), "MinimalisticWPF.Controls.ViewModel")]
    public partial class TextBox : UserControl
    {
        public TextBox()
        {
            InitializeComponent();
        }

        public string Text { get; private set; } = string.Empty;

        public event TextChangedEventHandler? OnTextChanged;

        [Obsolete]
        public static double MeasureTextWidth(string text, double fontSize, string fontFamilyName)
        {
            Typeface typeface = new(new FontFamily(fontFamilyName), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            FormattedText formattedText = new(
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                typeface,
                fontSize,
                Brushes.Black
            );

            return formattedText.WidthIncludingTrailingWhitespace;
        }
        [Obsolete]
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox textBox)
            {
                Text = textBox.Text;
                ViewModel.VisualLength = MeasureTextWidth(textBox.Text, textBox.FontSize, textBox.FontFamily.ToString());
            }
            OnTextChanged?.Invoke(this, e);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            ViewModel.IsHovered = true;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ViewModel.IsHovered = false;
        }
    }
}
