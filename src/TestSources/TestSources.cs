using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestSources.Interfaces;

namespace TestSources
{
    /// <summary>
    /// Gets the root TestSources folder and enables access to its files, folders
    /// methods and properties.
    /// </summary>
    public partial class TestSources : TestSourceDir
    {
        private const string TestSourcesFolder = "__testsources__";

        private static string FullPath
        {
            get
            {
                var projectDirectory = GetProjectPath();

                return Path.Combine(projectDirectory, TestSourcesFolder);
            }
        }
        
        /// <summary>
        /// Constructor of the TestSources Folder 
        /// </summary>
        public TestSources() : base(FullPath, null)
        {
        }

        /// <summary>
        /// Scans the filesystem and builds the virtual filesystem tree that powers up TestSources
        ///  </summary>
        internal void ScanFileSystem()
        {
            //ChildItems.Add();
            if (Directory.Exists(this.FullName))
            {
                //ProcessTestSourcesDir(this.RootFolder, Files);
            }
            else
            {
                throw new DirectoryNotFoundException(
                    string.Format("{0} is not a valid file or directory.", this.FullName));
            }
        }

        private void ProcessTestSourcesDir(ITestSourceItem parentRoot, IEnumerable<ITestSourceItem> Files)
        {
            // Process the list of files found in the directory.
            //string[] fileEntries = Directory.GetFiles(parentRoot.Path);
            //foreach (string fileName in fileEntries)
            //{
            //    var TestSourceFile = new TestSourcesItem(
            //        parentRoot,
            //        Path.GetFileName(fileName),
            //        Path.Combine(parentRoot.Path, fileName),
            //        TestSourceType.file);
            //    (parentRoot.Items as List<ITestSourcesItem>).Add(TestSourceFile);
            //    (Files as List<ITestSourcesItem>).Add(TestSourceFile);
            //    (FilesAndFolders as List<ITestSourcesItem>).Add(TestSourceFile);
            //}

            //// Recurse into subdirectories of this directory.
            //string[] subdirectoryEntries = Directory.GetDirectories(parentRoot.Path);
            //foreach (string subdirectory in subdirectoryEntries)
            //{
            //    var TestSourceSubFolder = new TestSourcesItem(
            //        parentRoot,
            //        Path.GetFileName(subdirectory),
            //        Path.Combine(parentRoot.Path, Path.GetFileName(subdirectory)),
            //        TestSourceType.folder);
            //    (parentRoot.Items as List<ITestSourcesItem>).Add(TestSourceSubFolder);
            //    (FilesAndFolders as List<ITestSourcesItem>).Add(TestSourceSubFolder);

            //    ProcessTestSourcesDirectory(TestSourceSubFolder, Files);
            //}
        }


        private static string GetProjectPath()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory)?.Parent.Parent.FullName;

            return projectDirectory;
        }
    }
}
