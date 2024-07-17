﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class SavedVersesViewModel : ObservableObject
    {
        public event EventHandler<string> ViewChanged;

        // Set RelayCommands here!
        public RelayCommand VerseViewCommand { get; set; }

        // Set ViewModels here!
        public VerseViewModel VerseVM { get; set; }

        private string lastButtonClickedTag;

        public string LastButtonClickedTag
        {
            get { return lastButtonClickedTag; }
        }

        public SavedVersesViewModel()
        {
            VerseViewCommand = new RelayCommand(VerseButtonClicked);
        }

        private void VerseButtonClicked(object parameter)
        {
            this.lastButtonClickedTag = parameter as string;

            ViewChanged?.Invoke(this, "verse");
        }
    }
}
