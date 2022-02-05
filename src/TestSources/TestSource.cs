using System.Collections.Generic;
using TestSources.Interfaces;

namespace TestSources
{
    /// <summary>
    /// The TestSource class exposes the TestSources class statically through
    /// a lazy instance of it. 
    /// </summary>
    public static class TestSource
    {
        #region TestSourceDir
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public static string Name => TestSources.Name;

        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>
        public static string FullName => TestSources.FullName;

        /// <summary>
        /// Gets the parent directory of a specified directory or file.
        /// </summary>
        public static ITestSourceDir Parent => TestSources.Parent;

        /// <summary>
        /// Gets the items in this directory, flat or recursively.
        /// </summary>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static IEnumerable<ITestSourceItem> GetItems(bool recursive = false)
        {
            return TestSources.GetItems(recursive);
        }

        /// <summary>
        /// Provides the enumerator to the ChildItems IEnumerable (List) so it can be iterated
        /// and used in LINQ.
        /// </summary>
        public static IEnumerator<ITestSourceItem> GetEnumerator() => TestSources.GetEnumerator();



        #endregion TestSourceDir

        /// <summary>
        /// Provides a list of ITestSourceItems with the files and folders
        /// in the testsources folder and subfolders
        /// </summary>
        public static IEnumerable<ITestSourceItem> FilesAndFolders
        {
            get => TestSources.FilesAndFolders;
        }

        /// <summary>
        /// Gets a file or folder from the testsources folder by its name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeSubdirs"></param>
        /// <returns></returns>
        public static ITestSourceItem GetByName(string name, bool includeSubdirs = true)
        {
            return TestSources.GetByName(name, includeSubdirs);
        }

        /// <summary>
        /// Gets a file from the files contained under the testsources folder by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeSubdirs"></param>
        /// <returns></returns>
        public static ITestSourceItem GetFileByName(string name, bool includeSubdirs = true)
        {
            return TestSources.GetFileByName(name, includeSubdirs);
        }

        /// <summary>
        /// Gets a folder from the files contained under the testsources folder by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeSubdirs"></param>
        /// <returns></returns>
        public static ITestSourceItem GetFolderByName(string name, bool includeSubdirs = true)
        {
            return TestSources.GetFolderByName(name, includeSubdirs);
        }

        private static TestSources _testSources;
        private static TestSources TestSources
        {
            get
            {
                if (_testSources == null)
                {
                    _testSources = new TestSources();
                }

                return _testSources;
            } 
        }
    }
}
