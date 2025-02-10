using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinimalisticWPF.Controls
{
    [DataContextConfig(nameof(TextBox), "MinimalisticWPF.Controls.ViewModel")]
    public partial class TextBox : UserControl
    {
        [Obsolete]
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
    }
}
