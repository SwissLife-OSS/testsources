using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestSources.Interfaces;

namespace TestSources
{
    /// <summary>
    /// Provides properties and instance methods for managing a TestSource file
    /// </summary>
    public class TestSourceDir : ITestSourceDir
    {
        private readonly DirectoryInfo _dirInfo;
        private List<ITestSourceItem> _childItems
            = new List<ITestSourceItem>();

        /// <summary>
        /// Initializes a new instance of the TestSourceDir class, which acts as a wrapper
        ///     for a TestSources directory.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parent"></param>
        public TestSourceDir(string path, ITestSourceDir parent)
        {
            _dirInfo = new DirectoryInfo(path);
            Parent = parent;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string Name => _dirInfo.Name;

        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>
        public string FullName => _dirInfo.FullName;

        private ITestSourceDir _parent;

        /// <summary>
        /// Gets the parent directory of a specified directory or file.
        /// </summary>
        public ITestSourceDir Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (_dirInfo.Parent is not null && _dirInfo.Parent.FullName != value.FullName)
                {
                    throw new ArgumentException("The directory of the file and the parent do not match.");
                }

                _parent = value;
            }
        }

        /// <summary>
        /// Provides access to the ChildItems collection, only to be populated internally
        /// on the TestSources assembly.
        /// </summary>
        internal List<ITestSourceItem> ChildItems
        {
            get
            {
                return _childItems;
            }
            set
            {
                _childItems = value;
            }
        }

        /// <summary>
        /// Gets the items in this directory, flat or recursively.
        /// </summary>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public IEnumerable<ITestSourceItem> GetItems(bool recursive = false)
        {
            var files = new List<ITestSourceItem>();

            foreach (ITestSourceItem item in ChildItems)
            {
                Type type = item.GetType();
                files.Add(item);

                if (item is ITestSourceDir && recursive)
                {
                   files.Union(((ITestSourceDir)item).GetItems(true).ToList());
                }
            }

            return files;
        }

        public IEnumerator<ITestSourceItem> GetEnumerator()
        {
            return _childItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
