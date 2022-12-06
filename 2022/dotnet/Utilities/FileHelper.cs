using System;
using System.IO;
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
    }
}
