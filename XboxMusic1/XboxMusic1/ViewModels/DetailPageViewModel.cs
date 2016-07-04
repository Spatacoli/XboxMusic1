using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using Windows.ApplicationModel.Resources;
using XboxMusic1.DataModel;
using XboxMusic1.Models;

namespace XboxMusic1.ViewModels
{
    class DetailPageViewModel : ViewModel
    {
        private INavigationService mNavigationService;
        private IMusicRepository mMusicRepository;

        public DelegateCommand GoBackCommand { get; set; }
        private ResourceLoader mResourceLoader;

        
        private string mGenreList;
        public string GenreList
        {
            get { return mGenreList; }
            set { SetProperty(ref mGenreList, value); }
        }
        
        private string mSubgenreList;
        public string SubgenreList
        {
            get { return mSubgenreList; }
            set { SetProperty(ref mSubgenreList, value); }
        }

        
        private string mReleaseDate;
        public string ReleaseDate
        {
            get { return mReleaseDate; }
            set { SetProperty(ref mReleaseDate, value); }
        }
      

        private string mXboxLink;
        public string XboxLink
        {
            get { return mXboxLink; }
            set { SetProperty(ref mXboxLink, value); }
        }
      
        private string mAlbumTitle;
        public string AlbumTitle
        {
            get { return mAlbumTitle; }
            set { SetProperty(ref mAlbumTitle, value); }
        }
      
        private string mImageUrl;
        public string ImageUrl
        {
            get { return mImageUrl; }
            set { SetProperty(ref mImageUrl, value); }
        }

        
        private ObservableCollection<Track> mTracks;
        public ObservableCollection<Track> Tracks
        {
            get { return mTracks; }
            set { SetProperty(ref mTracks, value); }
        }
      

        private Album mCurrentAlbum;
        public Album CurrentAlbum
        {
            get { return mCurrentAlbum; }
            set { SetProperty(ref mCurrentAlbum, value); }
        }
      
        public DetailPageViewModel(INavigationService navigationService, IMusicRepository musicRepository)
        {
            mNavigationService = navigationService;
            mMusicRepository = musicRepository;

            GoBackCommand = new DelegateCommand(
                () => mNavigationService.GoBack(),
                () => mNavigationService.CanGoBack());
        }

        public override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            var albumId = navigationParameter as string;
            mResourceLoader = ResourceLoader.GetForCurrentView();
            Tracks = new ObservableCollection<Track>();
            init(albumId);
        }

        private async void init(string albumId)
        {
            CurrentAlbum = await mMusicRepository.GetAlbumAsync(albumId);
            if (CurrentAlbum == null)
                mNavigationService.GoBack();
            XboxLink = CurrentAlbum.Link;
            AlbumTitle = string.Format(mResourceLoader.GetString("DetailPage_AlbumTitleFormat"), CurrentAlbum.Name, CurrentAlbum.Artists[0].Artist.Name);
            ImageUrl = CurrentAlbum.ImageUrl;
            GenreList = string.Format(mResourceLoader.GetString("DetailPage_GenreLabel"), string.Join(", ", CurrentAlbum.Genres));
            SubgenreList = string.Format(mResourceLoader.GetString("DetailPage_SubgenreLabel"), string.Join(", ", CurrentAlbum.Subgenres));
            ReleaseDate = string.Format(mResourceLoader.GetString("DetailPage_ReleaseDateLabel"), FormatReleaseDate(CurrentAlbum.ReleaseDate));

            foreach (var track in CurrentAlbum.Tracks.Items)
            {
                Tracks.Add(track);
            }
        }

        private string FormatReleaseDate(string releaseDate)
        {
            DateTime date;

            if (!DateTime.TryParse(releaseDate, out date))
                return string.Empty;

            return date.Date.ToString(mResourceLoader.GetString("DetailPage_ReleaseDateFormat"));
        }
    }
}
