using System;
using System.IO;
using System.Text;
using TestSources.Interfaces;
using TestSources.Extensions;

namespace TestSources
{
    /// <summary>
    /// Provides properties and instqance methods for managing a TestSource file
    /// </summary>
    public class TestSourceFile : ITestSourceFile
    {
        private FileInfo _fileInfo;

        /// <summary>
        /// Initializes a new instance of the TestSourceFile class, which acts as a wrapper
        ///     for a TestSources file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="parent"></param>
        public TestSourceFile(string fileName, ITestSourceDir parent)
        {
            _fileInfo = new FileInfo(fileName);
            _parent = parent;
            //Parent = parent;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string Name => _fileInfo.Name;

        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>
        public string FullName => _fileInfo.FullName;

        private ITestSourceDir? _parent;

        /// <summary>
        /// Gets the parent directory of a specified directory or file.
        /// </summary>
        public ITestSourceDir Parent
        {
            get { return _parent; }
            set {
                if (_fileInfo.Directory.FullName != value.FullName)
                {
                    throw new ArgumentException("The directory of the file and the parent do not match.");
                }
                _parent = value;
            }
        }

        /// <summary>
        /// Opens an existing file for reading.
        /// </summary>
        /// <returns></returns>
        public FileStream OpenRead()
        {
            return _fileInfo.OpenRead();
        }

        #region As*** Methods

        /// <summary>
        /// Reads the current file and returns its content as an
        /// string with a default UTF8 encoding.
        /// </summary>
        /// <returns>The file contents</returns>
        public string AsString() =>
            this.AsStringExt();

        /// <summary>
        /// Reads the current file and returns its content as an
        /// string with the provided encoding.
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns>The file contents in string format</returns>
        public string AsString(Encoding encoding) =>
            this.AsStringExt(encoding);

        #endregion As*** Methods

    }
}
