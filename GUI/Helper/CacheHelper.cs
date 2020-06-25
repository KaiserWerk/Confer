using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Core.Model;

namespace GUI.Helper
{
    public class CacheHelper
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                     "\\ConferClient\\";

        private static readonly string file = path + "recent_files.json";
        public static List<RemoteFileInfo> GetRecentFiles()
        {
            var l = new List<RemoteFileInfo>();

            if (File.Exists(file))
            {
                l = JsonSerializer.Deserialize<List<RemoteFileInfo>>(File.ReadAllText(file));
            }

            return l;
        }

        public static void SaveRecentFiles(IEnumerable<RemoteFileInfo> fileList)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var d = JsonSerializer.Serialize(fileList, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(file, d);
        }
    }
}
