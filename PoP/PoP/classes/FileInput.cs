using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace PoP.classes
{
    class FileInput
    {
        public static string[] GetAllLines(string filePath)
        {
            return File.ReadAllLines(filePath, Encoding.UTF8);
        }
        public static List<string> GetAllLinesAsList(string filePath)
        {
            return File.ReadAllLines(filePath, Encoding.UTF8).ToList();
        }

        public static Dictionary<string, object> GetJsonDict(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }

        public static List<Dictionary<string, object>> GetJsonDictList(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
        }
    }
}
