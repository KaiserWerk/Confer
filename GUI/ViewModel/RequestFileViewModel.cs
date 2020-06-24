using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Core;
using Core.Helper;
using Core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace GUI.ViewModel
{
    public class RequestFileViewModel : ViewModelBase
    {
        public string ServerAddress { get; set; }
        public string AuthKey { get; set; }
        public string RemoteFile { get; set; }

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

            var response = await HttpHelper.PostRequestAsync("http://" + this.ServerAddress, data, this.AuthKey);
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
                Messenger.Default.Send(f);

                var f2 = new RequestedFile()
                {
                    FileName = this.RemoteFile,
                    FileContent = respFile.Data.FileContent
                };
                Messenger.Default.Send(f2);
            }
        }
    }
}
