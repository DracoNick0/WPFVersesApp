using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class VerseViewModel : ObservableObject
    {
        private ObservableCollection<Button> gameButtons = new ObservableCollection<Button>();
        public ObservableCollection<Button> GameButtons
        {
            get { return gameButtons; }
            set
            {
                gameButtons = value;
                OnPropertyChanged(nameof(GameButtons));
            }
        }

        private Verse verse;
        public Verse Verse
        {
            get { return verse; }
        }

        // Used for page title.
        private string reference;
        public string Reference
        {
            get { return reference; }
        }

        // Used for page title.
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

            gameButtons.Add(CreateGameButton("Tap to Reveal"));
            gameButtons.Add(CreateGameButton("Passage Memory"));
            gameButtons.Add(CreateGameButton("Word Bank"));
            gameButtons.Add(CreateGameButton("Fill in the Blanks"));
        }

        private Button CreateGameButton(string gameName)
        {
            // Create new button with basic specifications
            var newButton = new Button
            {
                Width = 300,
                Height = 65,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                CommandParameter = verse.reference,
                // Command = VerseViewCommand,
                Margin = new Thickness(10, 0, 0, 10)
            };

            // Create the ControlTemplate
            ControlTemplate template = new ControlTemplate(typeof(Button));

            // Create the Border
            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, new SolidColorBrush(Color.FromRgb(91, 195, 255)));
            border.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Border.BorderBrushProperty));
            border.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(Border.BorderThicknessProperty));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));

            // Create the Grid
            FrameworkElementFactory grid = new FrameworkElementFactory(typeof(Grid));

            // Create the TextBlocks
            FrameworkElementFactory tapToRevealTextBlock = CreateTextBlock(gameName, Brushes.White, 14.0, new Thickness(10));

            // Add the TextBlocks to the Grid
            grid.AppendChild(tapToRevealTextBlock);

            // Add the Grid to the Border
            border.AppendChild(grid);

            // Set the Border as the VisualTree of the ControlTemplate
            template.VisualTree = border;

            // Set the ControlTemplate to the Button
            newButton.Template = template;

            return newButton;
        }

        private FrameworkElementFactory CreateTextBlock(string text, SolidColorBrush color, double fontSize, Thickness margin)
        {
            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));

            // Set values of TextBlock
            textBlock.SetValue(TextBlock.TextProperty, text);
            textBlock.SetValue(TextBlock.ForegroundProperty, color);
            textBlock.SetValue(TextBlock.FontSizeProperty, fontSize);
            textBlock.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            textBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textBlock.SetValue(FrameworkElement.MarginProperty, margin);

            return textBlock;
        }
    }
}
