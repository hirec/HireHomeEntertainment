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


namespace HireHomeEntertainment.ViewModel
{
    class Page2ViewModelBackup : ViewModelBase
    {
        private ApiClient privApiClient;


        public List<string> MyMovies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                NotifyPropertyChanged("MyMovies");
            }
        } List<string> _movies;

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

        public Page2ViewModelBackup()
        {
            Messenger.Default.Register<KeyEventArgs>(this, MainWindow_KeyDown);
            privApiClient = BaseMediaBrowserAPI.Instance.publicAPIClient;
            loadItems(privApiClient);
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
                    Filters = new[] { ItemFilter.IsUnplayed }
                });

                var items = result.Items.ToList();
                var movielist = new List<string>();
                var movieimages = new List<BitmapImage>();

                var imageoptions = new ImageOptions
                {
                    ImageType = ImageType.Primary,
                    Quality = 100
                };
                var streamoptions = new StreamOptions
                {

                };

                foreach (BaseItemDto item in items)
                {
                    if (item.VideoType.HasValue)
                    {
                    }

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
        }

        private void MovieCoverflow_SelectionIndexChanged(int index)
        {
            //This can be used to get or set the selected movie index
        }

        private void MainWindow_KeyDown(KeyEventArgs e)
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
                PageNavigation.Instance.NavigatePage("MP", selectedMoviePath);
            }
        }
    }
}
