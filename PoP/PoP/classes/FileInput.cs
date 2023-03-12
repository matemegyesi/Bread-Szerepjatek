using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
    }
}
