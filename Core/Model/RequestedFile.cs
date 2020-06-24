using System.Text.Json.Serialization;

namespace Core.Model
{
    public class RequestedFile
    {
        [JsonPropertyName("file_name")] public string FileName { get; set; }
        [JsonPropertyName("file_content")] public string FileContent { get; set; }
    }
}
