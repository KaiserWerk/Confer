using System.Diagnostics;
using System.Text.Json;
using Core.Helper;
using Core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GUI.Helper;

namespace GUI.ViewModel
{
    public class RequestFileViewModel : ViewModelBase
    {
        public string ServerAddress { get; set; } = "127.0.0.1:1663";
        public string AuthKey { get; set; } = "your-authkey";
        public string RemoteFile { get; set; } = @"C:\Local\app.yaml";

        public RelayCommand RequestFileCommand { get; set; }

        public RequestFileViewModel()
        {
            this.RequestFileCommand = new RelayCommand(this.RequestFileCommandExecute);
        }

        private async void RequestFileCommandExecute()
        {
            FilePostResponse respFile = null;
            var reqFile = new RequestedFile()
            {
                FileName = this.RemoteFile
            };

            string data = JsonSerializer.Serialize(reqFile);

            var url = "http://" + this.ServerAddress + "/file";
            Debug.WriteLine(url);

            var response = await HttpHelper.PostRequestAsync(url, data, this.AuthKey);
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                respFile = JsonSerializer.Deserialize<FilePostResponse>(body);
            }

            if (respFile != null)
            {
                var f = new RemoteFileInfo()
                {
                    RemoteHost = this.ServerAddress,
                    AuthKey = this.AuthKey,
                    FileName = this.RemoteFile
                };
                Messenger.Send(f);

                var f2 = new RequestedFile()
                {
                    FileName = this.RemoteFile,
                    FileContent = respFile.Data.FileContent
                };
                Messenger.Send(f2);
            }

            WindowManager.CloseRequestFileWindow();
        }
    }
}
