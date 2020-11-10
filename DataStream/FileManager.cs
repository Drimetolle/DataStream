using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataStream
{
    public class FileManager
    {
        private readonly List<FileInfo> _filesInfo;

        public List<string> FilesPaths => _filesInfo.Select(_ => _.FullName).ToList();

        /// <summary>
        /// Получает список всех файлов из дериктории с расширением .txt
        /// </summary>
        public FileManager()
        {
            var dataDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Data";
            var directory = new DirectoryInfo(dataDirectory);
            _filesInfo = directory.GetFiles("*.txt").ToList();
        }

        public FileManager(string path)
        {
            var directory = new DirectoryInfo(path);
            _filesInfo = directory.GetFiles("*.txt").ToList();
        }
    }
}
