using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Model
{
    public class FilePostResponse
    {
        [JsonPropertyName("status")] public string Status { get; set; }
        [JsonPropertyName("code")] public int Code { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("data")] public RequestedFile Data { get; set; }
    }
}
