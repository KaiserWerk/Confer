using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Animation;
using Core.Helper;
using Core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GUI.Helper;

namespace GUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string currentHost;
        private string currentAuthKey;
        private ObservableCollection<RemoteFileInfo> recentFiles = new ObservableCollection<RemoteFileInfo>();
        private string currentFileName;
        private string currentFileContent;

        public ObservableCollection<RemoteFileInfo> RecentFiles
        {
            get => recentFiles;
            set => base.Set(ref recentFiles, value);
        }

        public string CurrentFileName
        {
            get => currentFileName;
            set => base.Set(ref currentFileName, value);
        }

        public string CurrentFileContent
        {
            get => currentFileContent;
            set => base.Set(ref currentFileContent, value);
        }

        public RelayCommand RequestFileCommand { get; set; }
        public RelayCommand SaveFileCommand { get; set; }

        public MainViewModel()
        {
            Messenger.Register<RequestedFile>(this.ReceiveRequestedFile);
            Messenger.Register<RemoteFileInfo>(this.ReceiveRemoteFileInfo);

            this.RequestFileCommand = new RelayCommand(this.RequestFileCommandExecute);
            this.SaveFileCommand = new RelayCommand(this.SaveFileCommandExecute);
            
        }

        private async void SaveFileCommandExecute()
        {
            var rq = new RequestedFile()
            {
                FileName = this.CurrentFileName,
                FileContent = this.CurrentFileContent
            };
            var json = JsonSerializer.Serialize(rq);

            var response = await HttpHelper.PutRequestAsync("http://" + this.currentHost + "/file", json, this.currentAuthKey);
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                var respFile = JsonSerializer.Deserialize<FilePostResponse>(body);
                if (respFile.Status == "success")
                {
                    MessageBox.Show("File saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            MessageBox.Show("Error saving file!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void ReceiveRemoteFileInfo(object obj)
        {
            var f = obj as RemoteFileInfo;
            if (f == null)
                return;
            this.RecentFiles.Add(f);
            this.currentHost = f.RemoteHost;
            this.currentAuthKey = f.AuthKey;
        }

        private void ReceiveRequestedFile(object obj)
        {
            var file = obj as RequestedFile;
            if (file == null)
                return;
            this.CurrentFileName = file.FileName;
            this.CurrentFileContent = file.FileContent;

        }

        private void RequestFileCommandExecute()
        {
            WindowManager.OpenRequestFileWindow();
        }

        private void LoadRecentFiles()
        {
            this.RecentFiles = new ObservableCollection<RemoteFileInfo>(CacheHelper.GetRecentFiles());
        }
    }
}
