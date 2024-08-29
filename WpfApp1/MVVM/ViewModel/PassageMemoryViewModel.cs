using System.Collections.Generic;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class PassageMemoryViewModel : ObservableObject
    {
        private List<string> fullPassage = new List<string>();
        private int index = 0;

        private string revealedPassage;
        private string textBoxText;

        public string TextBoxText
        {
            get { return textBoxText; }
            set
            {
                OnTextChanged(value);
            }
        }

        public RelayCommand TextBoxChangedCommand { get; set; }

        public PassageMemoryViewModel(Verse verse)
        {
            char[] delimiters = new char[] { ' ', '\0' };
            int prevDelimiter = -1;
            string partOfPassage;
            int i = 0;

            for (; i < verse.passage.Length; i++)
            {
                foreach (char delimiter in delimiters)
                {
                    if (verse.passage[i] == delimiter)
                    {
                        partOfPassage = verse.passage.Substring(prevDelimiter + 1, i - prevDelimiter);
                        this.fullPassage.Add(partOfPassage);
                        prevDelimiter = i;
                    }
                }
            }

            partOfPassage = verse.passage.Substring(prevDelimiter + 1, i - prevDelimiter - 1);
            this.fullPassage.Add(partOfPassage);

            TextBoxChangedCommand = new RelayCommand(OnTextChanged);

            textBoxText = "Type first letter of each word in the passage.";
        }

        private void OnTextChanged(object textBoxText)
        {
            string textBoxString = textBoxText as string;

            if (textBoxString != revealedPassage && textBoxString != string.Empty)
            {
                // If the full passage hasn't been revealed yet
                if (index != fullPassage.Count)
                {
                    if (textBoxString[textBoxString.Length - 1] == fullPassage[index].ToLower()[0])
                    {
                        RevealText();
                    }
                    else
                    {
                        SetTextBoxToRevealedText();
                    }
                }
                else
                {
                    SetTextBoxToRevealedText();
                }
            }
        }

        private void RevealText()
        {
            revealedPassage += fullPassage[index];
            index++;

            SetTextBoxToRevealedText();
        }

        // Sets textBox text to revealedText, and notifies the textBox of change.
        private void SetTextBoxToRevealedText()
        {
            textBoxText = revealedPassage;
            OnPropertyChanged(nameof(TextBoxText));
        }
    }
}
