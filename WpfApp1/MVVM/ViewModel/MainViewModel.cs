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
                OnPropertyChanged();
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
            Verse verse = null;
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
                    // Find verse associated with button.
                    verse = storageManager.loadedVerses[SavedVersesVM.LastButtonClickedParameter];

                    // Setup viewmodel, change view, subscribe view.
                    VerseViewModel VerseVM = new VerseViewModel(verse);
                    CurrentView = VerseVM;
                    VerseVM.ViewChanged += OnViewChanged;
                    break;
                case "Tap to Reveal":
                    verse = storageManager.loadedVerses[SavedVersesVM.LastButtonClickedParameter];
                    CurrentView = new TapToRevealViewModel(verse);
                    break;
                case "Passage Memory":
                    verse = storageManager.loadedVerses[SavedVersesVM.LastButtonClickedParameter];
                    CurrentView = new PassageMemoryViewModel(verse);
                    break;
                case "Word Bank":
                    verse = storageManager.loadedVerses[SavedVersesVM.LastButtonClickedParameter];
                    //CurrentView = new WordBankViewModel(verse);
                    break;
                case "Fill in the Blanks":
                    verse = storageManager.loadedVerses[SavedVersesVM.LastButtonClickedParameter];
                    //CurrentView = new FillInTheBlankViewModel(verse);
                    break;
            }
        }
    }
}
