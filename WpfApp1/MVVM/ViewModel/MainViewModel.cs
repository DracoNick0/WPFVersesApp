using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        // Set RelayCommands here!
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SavedVersesViewCommand { get; set; }
        public RelayCommand VersePacksViewCommand { get; set; }


        // Set ViewModels here!
        public HomeViewModel HomeVM { get; set; }
        public SavedVersesViewModel SavedVersesVM { get; set; }
        public VersePacksViewModel VersePacksVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            { 
                _currentView = value;
                OnPropteryChanged();
            }
        }

        public MainViewModel()
        {
            // Create new VMs here
            HomeVM = new HomeViewModel();
            SavedVersesVM = new SavedVersesViewModel();
            VersePacksVM = new VersePacksViewModel();

            CurrentView = HomeVM;

            // Set VM commands here!
            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SavedVersesViewCommand = new RelayCommand(o =>
            {
                CurrentView = SavedVersesVM;
            });

            VersePacksViewCommand = new RelayCommand(o =>
            {
                CurrentView = VersePacksVM;
            });
        }
    }
}
