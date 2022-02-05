using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestSources.Interfaces;
using TestSources.Helpers;

namespace TestSources
{
    /// <summary>
    /// Gets the root TestSources folder and enables access to its files, folders
    /// methods and properties.
    /// </summary>
    public partial class TestSources : TestSourceDir
    {
        private const string DefaultTestSourcesFolder = "__testsources__";
        private static bool _fileSystemScanned = false;

        private static string FullPath
        {
            get
            {
                var projectDirectory = ProjectHelpers.GetProjectPath();

                return Path.Combine(projectDirectory, DefaultTestSourcesFolder);
            }
        }

        private IEnumerable<ITestSourceItem> _filesAndFolders;

        /// <summary>
        /// Provides a list of ITestSourceItems with the files and folders
        /// in the testsources folder and subfolders
        /// </summary>
        public IEnumerable<ITestSourceItem> FilesAndFolders {
            get => _filesAndFolders;
            set => _filesAndFolders = value;
        }

        /// <summary>
        /// Constructor of the TestSources Folder 
        /// </summary>
        public TestSources() : base(FullPath, null)
        {
            ScanFileSystem();
        }

        /// <summary>
        /// Scans the filesystem and builds the virtual filesystem tree that powers up TestSources
        ///  </summary>
        internal void ScanFileSystem()
        {
            // don't repeat!
            if (_fileSystemScanned == true)
                return;

            //ChildItems.Add();
            if (Directory.Exists(this.FullName))
            {
                this.FilesAndFolders = new List<ITestSourceItem>();
                ProcessTestSourcesDir(this, this.ChildItems);
            }
            else
            {
                throw new DirectoryNotFoundException(
                    string.Format("{0} is not a valid file or directory.", this.FullName));
            }
        }

        private void ProcessTestSourcesDir(ITestSourceDir parentRoot, IEnumerable<ITestSourceItem> childItems)
        {
            // Process the list of files found in the parentRoot directory.
            string[] fileEntries = Directory.GetFiles(parentRoot.FullName);
            foreach (string fileName in fileEntries)
            {
                // Add all files
                var TestSourceFile = new TestSourceFile(fileName, parentRoot);
                (childItems as List<ITestSourceItem>).Add(TestSourceFile);

                // We add all the files to the full list, where all the files & folders
                // from the current folder and subfolder will be placed
                (FilesAndFolders as List<ITestSourceItem>).Add(TestSourceFile);
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectories = Directory.GetDirectories(parentRoot.FullName);
            foreach (string subdirectory in subdirectories)
            {
                var testSourceSubDir = new TestSourceDir(subdirectory, parentRoot);
                (childItems as List<ITestSourceItem>).Add(testSourceSubDir); // adding directories too to the ChildItems

                // We add all the files to the full list, where all the files & folders
                // from the current folder and subfolder will be placed
                (FilesAndFolders as List<ITestSourceItem>).Add(testSourceSubDir);

                ProcessTestSourcesDir(testSourceSubDir, testSourceSubDir.ChildItems);
            }
        }

        //TODO: Make those extension methods for ITestSourceItem so we can do this from any directory
        /// <summary>
        /// Gets a file or folder from the testsources folder by its name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeSubdirs"></param>
        /// <returns></returns>
        public ITestSourceItem GetByName(string name, bool includeSubdirs = true)
        {
            ITestSourceItem sourcesItem = GetFiles<ITestSourceItem>(includeSubdirs)
                       .Where(s => s.Name == name)
                       .FirstOrDefault();

            return sourcesItem;
        }

        /// <summary>
        /// Gets the files from the testsources folder that matches a type
        /// </summary>
        /// <param name="includeSubDirectories"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<ITestSourceItem> GetFiles(bool includeSubDirectories, Type type)
        {
            IEnumerable<ITestSourceItem> files;
            if (includeSubDirectories)
            {
                files = this.FilesAndFolders;
            }
            else
            {
                files = this.ChildItems;
            }

            IEnumerable<ITestSourceItem> filesByType = files
                .Where(x => x.GetType() == type);

            return filesByType;
        }

        /// <summary>
        /// Gets the files from the testsources folder that matches a type
        /// </summary>
        /// <param name="includeSubDirectories"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<ITestSourceItem> GetFiles<T>(bool includeSubDirectories)
        {
            IEnumerable<ITestSourceItem> files;
            if (includeSubDirectories)
            {
                files = this.FilesAndFolders;
            }
            else
            {
                files = this.ChildItems;
            }

            IEnumerable<ITestSourceItem> filesByType = (IEnumerable<ITestSourceItem>)files.OfType<T>();

            return filesByType;
        }

        /// <summary>
        /// Gets a file from the files contained under the testsources folder by its name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeSubdirs"></param>
        /// <returns></returns>
        public ITestSourceItem GetFileByName(string name, bool includeSubdirs = true)
        {
            ITestSourceItem sourcesItem = GetFiles<TestSourceFile>(includeSubdirs)
                .Where(s => s.Name == name)
                .FirstOrDefault();

            return sourcesItem;
        }

        /// <summary>
        /// Gets a folder from the files contained under the testsources folder by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeSubdirs"></param>
        /// <returns></returns>
        public ITestSourceItem GetFolderByName(string name, bool includeSubdirs = true)
        {
            ITestSourceItem sourcesItem = GetFiles<TestSourceDir>(includeSubdirs) 
                .Where(s => s.Name == name)
                .FirstOrDefault();

            return sourcesItem;
        }
    }
}
