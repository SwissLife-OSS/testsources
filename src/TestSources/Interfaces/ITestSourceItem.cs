using System;
using System.Collections.Generic;
using System.Text;

namespace TestSources.Interfaces
{
    /// <summary>
    /// Allows an object to manage filesystem properties corresponding to a file or directory.
    /// </summary>
    public interface ITestSourceItem
    {
        /// <summary>
        /// Gets the Name of the TestSourceItem, which can be the name of a file or directory
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the full path of the directory or file. 
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the parent directory of a directory or file.
        /// </summary>
        ITestSourceDir Parent { get; }
    }
}
