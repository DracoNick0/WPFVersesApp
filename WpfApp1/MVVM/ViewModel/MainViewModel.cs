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

        public RelayCommand HomeViewCommand { get; set; }
        
        public RelayCommand SavedVersesViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public SavedVersesViewModel SavedVersesVM { get; set; }

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
            HomeVM = new HomeViewModel();
            SavedVersesVM = new SavedVersesViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SavedVersesViewCommand = new RelayCommand(o =>
            {
                CurrentView = SavedVersesVM;
            });
        }
    }
}
