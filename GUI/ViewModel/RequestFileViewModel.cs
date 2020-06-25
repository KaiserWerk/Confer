using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
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
            var rfi = new RemoteFileInfo()
            {
                RemoteHost = this.ServerAddress,
                AuthKey = this.AuthKey,
                FileName = this.RemoteFile
            };
            try
            {
                var response = await DataHelper.FetchRemoteFile(rfi);
                if (response != null)
                {
                    Messenger.Send(rfi);

                    var f2 = new RequestedFile()
                    {
                        FileName = this.RemoteFile,
                        FileContent = response.Data.FileContent
                    };
                    Messenger.Send(f2);
                }
                else
                {
                    MessageBox.Show("Empty response!", "Error");
                }
            }
            catch (TaskCanceledException e)
            {
                MessageBox.Show("Connection timed out!", "Error");
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("Connection timed out!!", "Error");
            }

            WindowManager.CloseRequestFileWindow();
        }
    }
}
