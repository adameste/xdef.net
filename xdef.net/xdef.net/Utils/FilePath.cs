using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xdef.net.Utils
{
    /// <summary>
    /// Class used as placeholder for java File class, required to distinguish between files and strings.
    /// Paths passed to Java are expended to fullpaths.
    /// </summary>
    public class FilePath
    {
        /// <summary>
        /// Relative or absolute path to file.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// true if this path leads to an existing file.
        /// </summary>
        public bool Exists => File.Exists(Path);
        internal string JavaPath => System.IO.Path.GetFullPath(Path).Replace("\\", "/");

        /// <summary>
        /// Initialize an instance of FilePath
        /// </summary>
        /// <param name="path">Relative or absolute path to file.</param>
        public FilePath(string path)
        {
            Path = path;
        }
    }
}
