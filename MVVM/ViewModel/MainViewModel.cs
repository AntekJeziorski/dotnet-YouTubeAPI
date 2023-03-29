using dotnet_YouTubeAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_YouTubeAPI.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ArtistViewCommand { get; set; }

        public HomeViewModel HomeVm { get; set; }
        public ArtistViewModel ArtistVM { get; set; }

        private object _currentView;

        public object CurrentView { get { return _currentView; } set { _currentView = value; OnPropertyChanged(); } }
        public MainViewModel() 
        { 
            HomeVm = new HomeViewModel(); 
            ArtistVM = new ArtistViewModel(); 
            
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVm; });
            ArtistViewCommand = new RelayCommand(o => { CurrentView = ArtistVM; });
        }
    }
}
