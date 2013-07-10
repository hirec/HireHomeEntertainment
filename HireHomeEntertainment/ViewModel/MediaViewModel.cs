using MediaBrowser.ApiInteraction;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Querying;
using HireHomeEntertainment.Singletons;
using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using System.IO;


namespace HireHomeEntertainment.ViewModel
{
    class MediaViewModel : ViewModelBase
    {
        #region Private Variables

        private ApiClient privApiClient;
        private string _callingPage;
        private bool _allowKeyPressMonitoring;

        #endregion

        #region Public Variables

        public List<string> MyMovies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                NotifyPropertyChanged("MyMovies");
            }
        } List<string> _movies;

        public string SelectedMediaSource
        {
            get { return _selectedMediaSource; }
            set
            {
                _selectedMediaSource = value;
                MovieCoverflow_SelectionChanged(_selectedMediaSource);
                NotifyPropertyChanged("SelectedMediaSource");
            }
        } string _selectedMediaSource;

        public string selectedMoviePath
        {
            get { return _selectedMoviePath; }
            set
            {
                _selectedMoviePath = value;
                MovieCoverflow_SelectionChanged(_selectedMoviePath);
                NotifyPropertyChanged("selectedMoviePath");
            }
        } string _selectedMoviePath;

        public int selectedMovieIndex
        {
            get { return _selectedMovieIndex; }
            set
            {
                _selectedMovieIndex = value;
                MovieCoverflow_SelectionIndexChanged(_selectedMovieIndex);
                NotifyPropertyChanged("selectedMovieIndex");
            }
        } int _selectedMovieIndex;

        public List<BitmapImage> MyMoviesImages
        {
            get { return _moviesImage; }
            set
            {
                _moviesImage = value;
                NotifyPropertyChanged("MyMoviesImages");
            }
        } List<BitmapImage> _moviesImage;

        public List<BaseItemDto> MyMovieItems
        {
            get { return _movieItems; }
            set
            {
                _movieItems = value;
                NotifyPropertyChanged("MyMovieItems");
            }
        } List<BaseItemDto> _movieItems;

        #endregion

        #region Constructor

        public MediaViewModel(string callingPage)
        {
            RegisterCommands();    
            _allowKeyPressMonitoring = true;
            _callingPage = callingPage;
            Messenger.Default.Register<KeyEventArgs>(this, MainWindow_KeyDown);     
            privApiClient = BaseMediaBrowserAPI.Instance.publicAPIClient;
            loadItems(privApiClient);
          
        }

        #endregion

        #region commands

        public static RelayCommand Load { get; set; }

        #endregion

        private void RegisterCommands()
        {
            Load = new RelayCommand(param => OnLoad());
        }

        private void OnLoad()
        {
            _allowKeyPressMonitoring = true;
        }

        private async void loadItems(ApiClient client)
        {
            var totalItems = 10;
            try
            {
                var result = await client.GetItemsAsync(new ItemQuery
                {
                    UserId = client.CurrentUserId,
                    IncludeItemTypes = new[] { "Movie" },
                    Limit = totalItems,
                    SortBy = new[] { ItemSortBy.DateCreated },
                    SortOrder = MediaBrowser.Model.Entities.SortOrder.Descending,
                    Recursive = true,
                    ImageTypes = new[] { ImageType.Backdrop },
                    Filters = new[] { ItemFilter.IsUnplayed },
                    Fields = new[] {
                    ItemFields.Path,
                    ItemFields.MediaStreams,
                    ItemFields.Genres,
                    }                

                });
                
                var items = result.Items.ToList();         
                MyMovieItems = items;
                var movielist = new List<string>();
                var movieimages = new List<BitmapImage>();                

                var imageoptions = new ImageOptions
                {
                    ImageType = ImageType.Primary,
                    Quality =  100                 
                };
                                             
                foreach (BaseItemDto item in items)
                {
                    if (item.HasPrimaryImage)
                    {
                        var uri = client.GetImageUrl(item, imageoptions);                        
                        BitmapImage bitmap = new BitmapImage();                      
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(uri);
                        bitmap.EndInit();
                        movieimages.Add(bitmap);                     
                    }
                    movielist.Add(item.Name);
                }
                MyMoviesImages = movieimages;
                MyMovies = movielist;

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("error " + e.Message);
            }
        }

        private void MovieCoverflow_SelectionChanged(string moviePath)
        {        
            //This can be used to set the selected movie path to play in external player
           
            var selectedMovie = MyMovieItems[selectedMovieIndex];          
            _selectedMediaSource = selectedMovie.Path;            
        }

        private void MovieCoverflow_SelectionIndexChanged(int index)
        {
            //This can be used to get or set the selected movie index
        }

        private void MainWindow_KeyDown(KeyEventArgs e)
        {
            if (PageNavigation.CurrentPage == _callingPage && _allowKeyPressMonitoring)
            {
                if (e.Key == Key.Left)
                {
                    if (selectedMovieIndex >= 1)
                    {
                        selectedMovieIndex--;
                    }

                }
                if (e.Key == Key.Right)
                {
                    selectedMovieIndex++;
                }

                if (e.Key == Key.Enter)
                {
                    PageNavigation.Instance.NavigatePage("MediaPlayer", _selectedMediaSource, _callingPage);
                }
                if (e.Key == Key.Escape)
                {
                    _allowKeyPressMonitoring = false;
                    PageNavigation.Instance.NavigateBack(_callingPage);
                }
            }
        }
               
    }   
}
