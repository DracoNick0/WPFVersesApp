using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        StorageManager storageManager = new StorageManager();

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
            //HomeVM.ViewChanged += OnViewChanged;

            SavedVersesVM = new SavedVersesViewModel(storageManager.loadedVerses);
            SavedVersesVM.ViewChanged += OnViewChanged;

            VersePacksVM = new VersePacksViewModel();
            //VersePacksVM.ViewChanged += OnViewChanged;

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

        private void OnViewChanged(object sender, string newValue)
        {
            switch(newValue)
            {
                case "home":
                    CurrentView = HomeVM;
                    break;
                case "saved verses":
                    CurrentView = SavedVersesVM;
                    break;
                case "verse packs":
                    CurrentView = VersePacksVM;
                    break;
                case "verse":
                    string reference = SavedVersesVM.LastButtonClickedParameter;
                    string verse = this.storageManager.loadedVerses[reference];
                    string dueDate = this.storageManager.loadedVerses[reference];


                    CurrentView = new VerseViewModel(SavedVersesVM.LastButtonClickedParameter);
                    break;
            }
        }
    }
}
