using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using MVVM;
using Microsoft.Win32;
using WPFMediaKit.DirectShow.Controls;

namespace HireHomeEntertainment.ViewModel
{
    class MediaPlayerViewModel : ViewModelBase
    {  

       #region Public Variables
        public MediaUriElement MediaELVOB { set; get; }
        public MediaElement MediaEL { set; get; }
        public MediaState LoadedBehavior { set; get; }     
        public bool CanCommandExecute { set; get; }   
          
       #endregion

       #region Private Variables
        private string _currentVideoPlayer;
       
       #endregion

       #region Constructor

       public MediaPlayerViewModel(string MediaURI)
       {           
           RegisterCommands();
          
           if (MediaURI != "")
           {
               if (MediaURI.Contains(".avi") || MediaURI.Contains(".mp4") || MediaURI.Contains("mkv"))
               {
                   MediaEL = new MediaElement();
                   Uri MediaSource = new Uri(MediaURI);
                   MediaEL.Source = MediaSource;
                   MediaEL.LoadedBehavior = MediaState.Manual;
                   _currentVideoPlayer = "REG";                   
               }
               else
               {  
                  MediaELVOB = new MediaUriElement();
                  MediaURI = MediaURI + "\\VIDEO_TS\\VTS_01_1.VOB";
                  Uri MediaSource = new Uri(MediaURI);
                  MediaELVOB.Source = MediaSource;
                  MediaELVOB.LoadedBehavior = WPFMediaKit.DirectShow.MediaPlayers.MediaState.Manual;
                  _currentVideoPlayer = "VOB";
               }                     
           }
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
            if (_currentVideoPlayer == "VOB")
            {
                MediaELVOB.Play();
            }
            else
            {
                MediaEL.Play();
            }
        }
        private void ExecuteStop()
        {
            if (_currentVideoPlayer == "VOB")
            {
                MediaELVOB.Stop();
            }
            else
            {
                MediaEL.Stop();
            }
        }
        private void ExecutePause()
        {
            if (_currentVideoPlayer == "VOB")
            {
                MediaELVOB.Pause();
            }
            else
            {
                MediaEL.Pause();
            }
        }
        private void ExecuteResume()
        {
                  
        }

        #endregion
                
    }
}
