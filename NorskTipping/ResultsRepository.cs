using System;
using System.IO;

namespace NorskTipping
{
    public static class ResultsRepository
    {
        public static FileInfo[] ReadAllFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("The path to XML files cannot be empty.");
            var di = new DirectoryInfo(path);
            return di.GetFiles("*.txt");
        }
    }
}
