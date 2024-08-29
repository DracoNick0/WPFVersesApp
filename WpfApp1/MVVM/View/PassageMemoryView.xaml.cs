using System.Windows;
using System.Windows.Controls;
using WpfApp1.MVVM.ViewModel;

namespace WpfApp1.MVVM.View
{
    /// <summary>
    /// Interaction logic for TapToRevealView.xaml
    /// </summary>
    public partial class PassageMemoryView : UserControl
    {
        PassageMemoryViewModel PMVM { get; set; }

        public PassageMemoryView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is PassageMemoryViewModel passageMemoryViewModel)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    passageMemoryViewModel.TextBoxChangedCommand.Execute(textBox.Text);

                    // Move user cursor to end of text.
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DataContext is PassageMemoryViewModel passageMemoryViewModel)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    textBox.Text = string.Empty;
                }
            }
        }
    }
}
