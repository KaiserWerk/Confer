using System.Text.Json.Serialization;

namespace Core.Model
{
    public class RemoteFileInfo
    {
        [JsonPropertyName("remote_host")] public string RemoteHost { get; set; }
        [JsonPropertyName("auth_key")] public string AuthKey { get; set; }
        [JsonPropertyName("file_name")] public string FileName { get; set; }
    }
}
