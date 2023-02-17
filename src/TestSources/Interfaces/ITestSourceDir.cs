using System.Collections.Generic;

namespace TestSources.Interfaces;

/// <summary>
/// Allows an object to manage filesystem properties corresponding to a directory.
/// </summary>
public interface ITestSourceDir : ITestSourceItem, IEnumerable<ITestSourceItem>
{
    /// <summary>
    ///      Returns a collection of files and directories in the current directory
    /// </summary>
    /// <param name="recursive"></param>
    /// <returns></returns>
    IEnumerable<ITestSourceItem> GetItems(bool recursive = false);

    /// <summary>
    /// Gets and returns a IEnumerable with the files on this directory.
    /// </summary>
    /// <returns></returns>
    //IEnumerable<ITestSourceFile> GetFiles();
    IEnumerable<ITestSourceFile> GetFiles();

    /// <summary>
    /// Gets the folders contained on this directory.
    /// </summary>
    /// <returns></returns>
    //IEnumerable<ITestSourceDir> GetFolders();
    IEnumerable<ITestSourceDir> GetFolders();
}
