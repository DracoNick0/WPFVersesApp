using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class SavedVersesViewModel : ObservableObject
    {
        // UPDATE THIS WHEN NEW VERSE IS ADDED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private ObservableCollection<Button> verseButtons = new ObservableCollection<Button>();
        public ObservableCollection<Button> VerseButtons
        {
            get { return verseButtons; }
            set
            {
                verseButtons = value;
                OnPropertyChanged(nameof(VerseButtons));
            }
        }

        private Dictionary<string, Verse> loadedVerses;
        private string lastButtonClickedParameter;

        // Set RelayCommands here!
        public RelayCommand VerseViewCommand { get; set; }

        // Set ViewModels here!
        public VerseViewModel VerseVM { get; set; }

        public string LastButtonClickedParameter
        {
            get { return lastButtonClickedParameter; }
        }

        public SavedVersesViewModel(Dictionary<string, Verse> loadedVerses)
        {
            this.loadedVerses = loadedVerses;

            VerseViewCommand = new RelayCommand(VerseButtonClicked);

            CreateAllVerseButtons();
        }

        private void CreateAllVerseButtons()
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/../../Theme/VerseButtonTheme.xaml")
            };

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            foreach (Verse verse in loadedVerses.Values)
            {
                // Create new button
                Button newButton = CreateVerseButton(verse);

                // Add the button to your container (e.g., a panel)
                VerseButtons.Add(newButton);
            }
        }

        private Button CreateVerseButton(Verse verse)
        {
            // Create new button with basic specifications
            var newButton = new Button
            {
                Width = 300,
                Height = 65,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                CommandParameter = verse.reference,
                Command = VerseViewCommand,
                Margin = new Thickness(10,0,0,10)
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
            FrameworkElementFactory referenceVersionTextBlock = CreateTextBlock(verse.reference + " " + verse.version, Brushes.White, 14.0, VerticalAlignment.Top, HorizontalAlignment.Left, new Thickness(10));
            FrameworkElementFactory daysTillDueTextBlock = CreateTextBlock(verse.daysTillDue.ToString(), Brushes.White, 14.0, VerticalAlignment.Top, HorizontalAlignment.Right, new Thickness(10));
            if (verse.daysTillDue < 0)
            {
                daysTillDueTextBlock = CreateTextBlock(verse.daysTillDue.ToString(), Brushes.Red, 14.0, VerticalAlignment.Top, HorizontalAlignment.Right, new Thickness(10));
            }
            else if (verse.daysTillDue == 0)
            {
                daysTillDueTextBlock = CreateTextBlock(verse.daysTillDue.ToString(), Brushes.Yellow, 14.0, VerticalAlignment.Top, HorizontalAlignment.Right, new Thickness(10));
            }
            FrameworkElementFactory passageTextBlock = CreateTextBlock(verse.passage, Brushes.White, 14.0, VerticalAlignment.Bottom, HorizontalAlignment.Left, new Thickness(10));

            // Add the TextBlocks to the Grid
            grid.AppendChild(referenceVersionTextBlock);
            grid.AppendChild(daysTillDueTextBlock);
            grid.AppendChild(passageTextBlock);

            // Add the Grid to the Border
            border.AppendChild(grid);

            // Set the Border as the VisualTree of the ControlTemplate
            template.VisualTree = border;

            // Set the ControlTemplate to the Button
            newButton.Template = template;

            return newButton;
        }

        private FrameworkElementFactory CreateTextBlock(string text, SolidColorBrush color, double fontSize, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Thickness margin)
        {
            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));

            // Set values of TextBlock
            textBlock.SetValue(TextBlock.TextProperty, text);
            textBlock.SetValue(TextBlock.ForegroundProperty, color);
            textBlock.SetValue(TextBlock.FontSizeProperty, fontSize);
            textBlock.SetValue(TextBlock.VerticalAlignmentProperty, verticalAlignment);
            textBlock.SetValue(TextBlock.HorizontalAlignmentProperty, horizontalAlignment);
            textBlock.SetValue(FrameworkElement.MarginProperty, margin);

            return textBlock;
        }

        public event EventHandler<string> ViewChanged;
        private void VerseButtonClicked(object parameter)
        {
            this.lastButtonClickedParameter = parameter as string;

            ViewChanged?.Invoke(this, "verse");
        }
    }
}
