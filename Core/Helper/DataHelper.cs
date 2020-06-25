using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Helper
{
    public class DataHelper
    {
        public static async Task<FilePostResponse> FetchRemoteFile(RemoteFileInfo info)
        {
            FilePostResponse respFile = null;
            var reqFile = new RequestedFile()
            {
                FileName = info.FileName
            };

            string data = JsonSerializer.Serialize(reqFile);


            var response = await HttpHelper.PostRequestAsync("http://" + info.RemoteHost + "/file", data, info.AuthKey);
            if (response.IsSuccessStatusCode)
            {
                string body = await response.Content.ReadAsStringAsync();
                respFile = JsonSerializer.Deserialize<FilePostResponse>(body);
            }

            return respFile;
        }
    }
}
