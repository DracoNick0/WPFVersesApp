using System;
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

        private Dictionary<string, Verse> loadedVerses;

        // Set RelayCommands here!
        public RelayCommand VerseViewCommand { get; set; }

        // Set ViewModels here!
        public VerseViewModel VerseVM { get; set; }

        private string lastButtonClickedParameter;

        public string LastButtonClickedParameter
        {
            get { return lastButtonClickedParameter; }
        }

        public SavedVersesViewModel(Dictionary<string, Verse> loadedVerses)
        {
            this.loadedVerses = loadedVerses;

            VerseViewCommand = new RelayCommand(VerseButtonClicked);
        }

        private void VerseButtonClicked(object parameter)
        {
            this.lastButtonClickedParameter = parameter as string;

            ViewChanged?.Invoke(this, "verse");
        }
    }
}
