using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM;
using System.Windows.Controls;

namespace HireHomeEntertainment.ViewModel
{
    class MediaPlayerViewModel : ViewModelBase
    {
        public string selectedMoviePath
        {
            get { return _selectedMoviePath; }
            set
            {
                _selectedMoviePath = value;
                NotifyPropertyChanged("selectedMoviePath");
            }
        } string _selectedMoviePath;       

       #region Public Variables

       public MediaElement MediaEL { set; get; }
       public MediaState LoadedBehavior { set; get; }
       public bool CanCommandExecute { set; get; }   
          
       #endregion

       #region Private Variables
        
       #endregion

       #region Constructor

       public MediaPlayerViewModel(string MediaURI)
       {           
           RegisterCommands();           

           MediaEL = new MediaElement();
           if (MediaURI != "")
           {
               //Only add "\\VIDEO_TS\\VTS_01_1.VOB" if a vob file
               MediaURI = MediaURI + "\\VIDEO_TS\\VTS_01_0.VOB";
               Uri MediaSource = new Uri(MediaURI);                
               MediaEL.Source = MediaSource;
               System.Windows.MessageBox.Show(MediaSource.ToString());
           }
           MediaEL.LoadedBehavior = MediaState.Manual;
       }

       #endregion

       #region commands

        public static RelayCommand PlayCommand { get; set; }
        public static RelayCommand StopCommand { get; set; }
        public static RelayCommand PauseCommand { get; set; }
        public static RelayCommand ResumeCommand { get; set; }
        public static RelayCommand WorkspaceCloseCommand { get; set; }  

        #endregion        

        private void RegisterCommands()
        {
            PlayCommand = new RelayCommand(param => ExecutePlay());
            StopCommand = new RelayCommand(param => ExecuteStop());
            PauseCommand = new RelayCommand(param => ExecutePause());
            ResumeCommand = new RelayCommand(param => ExecuteResume()); 
        }

        #region Media Controls

        private void ExecutePlay()
        {
            MediaEL.Play();
        }
        private void ExecuteStop()
        {
            MediaEL.Stop();
        }
        private void ExecutePause()
        {
            MediaEL.Pause();
        }
        private void ExecuteResume()
        {
            
        }

        #endregion
                
    }
}
