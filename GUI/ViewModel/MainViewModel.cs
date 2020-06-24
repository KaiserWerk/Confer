using System.Collections.ObjectModel;
using Core;
using Core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GUI.Helper;

namespace GUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string connectedHost;
        private ObservableCollection<RemoteFileInfo> recentFiles;


        public string ConnectedHost
        {
            get => connectedHost;
            set => base.Set(ref connectedHost, value);
        }

        public ObservableCollection<RemoteFileInfo> RecentFiles
        {
            get => recentFiles;
            set => base.Set(ref recentFiles, value);
        }

        public RelayCommand RequestFileCommand { get; set; }

        public MainViewModel()
        {
            this.RequestFileCommand = new RelayCommand(this.RequestFileCommandExecute);

            Messenger.Default.Register<TransmitRequestedFileMessage>(this, this.ReceiveRequestedFile);
        }

        private void ReceiveRequestedFile(TransmitRequestedFileMessage obj)
        {
            var file = obj.Content;

            // add to cache
        }

        private void RequestFileCommandExecute()
        {
            WindowManager.OpenRequestFileWindow();
        }

        private void LoadRecentFiles(string host)
        {
            this.RecentFiles = new ObservableCollection<RemoteFileInfo>(CacheHelper.GetRecentFiles());
        }
    }
}
