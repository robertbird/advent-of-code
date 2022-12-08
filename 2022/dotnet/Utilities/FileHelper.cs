using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utilities
{
    public static class FileHelper
    {
        public static string GetFileContents(string relativePath)
        {
            string readContents;
            using (StreamReader streamReader = new StreamReader(relativePath, Encoding.UTF8))
            {
                readContents = streamReader.ReadToEnd();
            }
            return readContents;
        }

        public static List<string> GetFileAsRows(string relativePath)
        {
            var input = GetFileContents(relativePath);

            return input.Split("\r\n")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

        }
    }
}
