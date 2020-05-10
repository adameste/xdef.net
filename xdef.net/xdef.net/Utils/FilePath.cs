using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xdef.net.Utils
{
    public class FilePath
    {
        public string Path { get; set; }
        public bool Exists => File.Exists(Path);
        public string JavaPath => System.IO.Path.GetFullPath(Path).Replace("\\", "/");

        public FilePath(string path)
        {
            Path = path;
        }
    }
}
