using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class VerseViewModel
    {
        private Verse verse;
        public Verse Verse
        {
            get { return verse; }
        }

        private string reference;
        public string Reference
        {
            get { return reference; }
        }

        private string version;
        public string Version
        {
            get { return version; }
        }

        public VerseViewModel(Verse verse)
        {
            this.verse = verse;
            this.reference = verse.reference;
            this.version = verse.version;

            // FrameworkElementFactory textBlock1 = CreateTextBlock(verse.reference + " " + verse.version, Brushes.White, 14.0, new Thickness(10));
        }

        private FrameworkElementFactory CreateTextBlock(string text, SolidColorBrush color, double fontSize, Thickness margin)
        {
            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));

            // Set values of TextBlock
            textBlock.SetValue(TextBlock.TextProperty, text);
            textBlock.SetValue(TextBlock.ForegroundProperty, color);
            textBlock.SetValue(TextBlock.FontSizeProperty, fontSize);
            textBlock.SetValue(FrameworkElement.MarginProperty, margin);

            return textBlock;
        }
    }
}
