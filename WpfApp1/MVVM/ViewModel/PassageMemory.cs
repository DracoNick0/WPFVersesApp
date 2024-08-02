﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class PassageMemoryViewModel : ObservableObject
    {
        private List<string> fullPassage = new List<string>();
        private int index = 0;

        private string revealedPassage;
        public string RevealedPassage
        {
            get { return revealedPassage; }
            set
            {
                revealedPassage = value;
                OnPropertyChanged(nameof(RevealedPassage));
            }
        }

        public RelayCommand revealButtonClicked { get; set; }

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

            revealButtonClicked = new RelayCommand(RevealText);
        }

        private void RevealText(object parameter)
        {
            if (index != fullPassage.Count)
            {
                RevealedPassage += fullPassage[index];
                index++;
            }
        }

    }
}
