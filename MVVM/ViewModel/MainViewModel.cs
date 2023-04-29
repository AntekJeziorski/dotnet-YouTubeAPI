using dotnet_YouTubeAPI.Core;

namespace dotnet_YouTubeAPI.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ArtistViewCommand { get; set; }
        public RelayCommand TracksViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }  

        public HomeViewModel HomeVm { get; set; }
        public ArtistViewModel ArtistVM { get; set; }
        public TracksViewModel TracksVM { get; set; }
        public AboutViewModel AboutVM  { get; set; }

        private object _currentView;

        public object CurrentView { get { return _currentView; } set { _currentView = value; OnPropertyChanged(); } }
        public MainViewModel() 
        { 
            HomeVm = new HomeViewModel(); 
            ArtistVM = new ArtistViewModel();
            TracksVM = new TracksViewModel();   
            AboutVM = new AboutViewModel();

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVm; });
            ArtistViewCommand = new RelayCommand(o => { CurrentView = ArtistVM; });
            TracksViewCommand = new RelayCommand(o => { CurrentView = TracksVM; });
            AboutViewCommand = new RelayCommand(o => { CurrentView = AboutVM; });   
        }
    }
}
