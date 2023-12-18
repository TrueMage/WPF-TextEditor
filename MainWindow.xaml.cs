using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WPF_TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Random rand = new Random();

            #region FontSize
            for (int i = 10; i <= 18; i++)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = i.ToString();
                if (i == 12) temp.IsSelected = true;
                temp.Selected += new RoutedEventHandler(ChangeFontSize);
                fontsizeBox.Items.Add(temp);
                if (i == 18)
                {
                    for (int j = 2; j < 4; j++)
                    {
                        temp = new ComboBoxItem();
                        temp.Content = (i * j).ToString();
                        temp.Selected += new RoutedEventHandler(ChangeFontSize);
                        fontsizeBox.Items.Add(temp);
                    }
                }
            }
            #endregion

            #region FontColor
            for (int i = 0; i <= 10; i++)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Height = 20;
                if (i == 1)
                {
                    temp.IsSelected = true;
                    temp.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                }else temp.Background = new SolidColorBrush(Color.FromRgb((byte)rand.Next(0,255),(byte)rand.Next(0, 255), (byte)rand.Next(0, 255)));
                temp.Selected += new RoutedEventHandler(ChangeColor);
                colorBox.Items.Add(temp);
            }
            #endregion
        }

        void MakeBold(object sender, RoutedEventArgs e)
        {
            TextSelection sSelectedText = richtb.Selection;

            String type = sSelectedText.GetPropertyValue(TextElement.FontWeightProperty).ToString();
            if (type == "Bold")
            {
                sSelectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                return;
            }

            sSelectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        void MakeItalic(object sender, RoutedEventArgs e)
        {
            TextSelection sSelectedText = richtb.Selection;

            String type = sSelectedText.GetPropertyValue(TextElement.FontStyleProperty).ToString();
            if (type == "Italic")
            {
                sSelectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                return;
            }

            sSelectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        void MakeUnderline(object sender, RoutedEventArgs e)
        {
            TextSelection sSelectedText = richtb.Selection;

            // Не оч хороший вариант, может быть, потом исправлю
            TextDecorationCollection type = sSelectedText.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;
            if (type.Count > 0)
            {
                sSelectedText.ClearAllProperties();
                return;
            }

            sSelectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        void ChangeFontSize(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cb = sender as ComboBoxItem;
            TextSelection sSelectedText = richtb.Selection;
            sSelectedText.ApplyPropertyValue(TextElement.FontSizeProperty, Convert.ToDouble(cb.Content));
        }

        void ChangeColor(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cb = sender as ComboBoxItem;
            //colorBox. = cb.Foreground;
            TextSelection sSelectedText = richtb.Selection;
            sSelectedText.ApplyPropertyValue(TextElement.ForegroundProperty, cb.Background);
        }
    }
}