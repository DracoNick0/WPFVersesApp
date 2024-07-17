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
        // Set RelayCommands here!
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SavedVersesViewCommand { get; set; }
        public RelayCommand VersePacksViewCommand { get; set; }
        public RelayCommand VerseViewCommand { get; set; }


        // Set ViewModels here!
        public HomeViewModel HomeVM { get; set; }
        public SavedVersesViewModel SavedVersesVM { get; set; }
        public VersePacksViewModel VersePacksVM { get; set; }
        public VerseViewModel VerseVM { get; set; }


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

            SavedVersesVM = new SavedVersesViewModel();
            SavedVersesVM.ViewChanged += OnViewChanged;

            VersePacksVM = new VersePacksViewModel();
            //VersePacksVM.ViewChanged += OnViewChanged;

            //VerseVM = new VerseViewModel();
            //VerseVM.ViewChanged += OnViewChanged;

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

            VerseViewCommand = new RelayCommand(o =>
            {
                CurrentView = VerseVM;
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
                    CurrentView = new VerseViewModel(SavedVersesVM.LastButtonClickedTag);
                    break;
            }
        }
    }
}
