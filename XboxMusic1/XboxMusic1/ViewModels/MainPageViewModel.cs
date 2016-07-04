using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Newtonsoft.Json;
using Windows.Data.Json;
using Windows.Web.Http;
using XboxMusic1.DataModel;
using XboxMusic1.Models;

namespace XboxMusic1.ViewModels
{
    class MainPageViewModel : ViewModel
    {
        public DelegateCommand<Album> NavCommand { get; set; }
        public DelegateCommand GenreChanged { get; set; }

        
        private string mSelectedGenre;
        public string SelectedGenre
        {
            get { return mSelectedGenre; }
            set 
            { 
                SetProperty(ref mSelectedGenre, value);
                OnGenreChanged(value);
            }
        }
      

        private ObservableCollection<string> mGenres;
        public ObservableCollection<string> Genres
        {
            get { return mGenres; }
            set { SetProperty(ref mGenres, value); }
        }
      
        private ObservableCollection<Album> mAlbums;
        public ObservableCollection<Album> Albums
        {
            get { return mAlbums; }
            set { SetProperty(ref mAlbums, value); }
        }

        private INavigationService mNavigationService;
        private IMusicRepository mMusicRepository;
      
        public MainPageViewModel(INavigationService navigationService, IMusicRepository musicRepository)
        {
            mNavigationService = navigationService; 
            mMusicRepository = musicRepository;
            NavCommand = new DelegateCommand<Album>(OnNavCommand);
            Albums = new ObservableCollection<Album>();
        }

        private async void OnGenreChanged(string name)
        {
            if (name == Genres[0])
                name = string.Empty;
            Albums = await mMusicRepository.GetNewReleaseDataAsync(name);
        }

        private void OnNavCommand(Album album)
        {
            mNavigationService.Navigate("Detail", album.Id);
        }

        public override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            if (navigationMode == Windows.UI.Xaml.Navigation.NavigationMode.Back)
            {
                Genres = viewModelState["Genres"] as ObservableCollection<string>;
                SelectedGenre = viewModelState["Selected Genre"].ToString();
            }
            else
            {
                Init();
            }
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
            viewModelState.Add("Genres", Genres);
            viewModelState.Add("Selected Genre", SelectedGenre);
        }

        private async void Init()
        {
            Genres = await mMusicRepository.GetGenreListAsync();
            SelectedGenre = Genres[0];
        }

        
    }
}
