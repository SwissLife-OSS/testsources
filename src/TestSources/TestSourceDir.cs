using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestSources.Interfaces;

namespace TestSources;

/// <summary>
/// Provides properties and instance methods for managing a TestSource directory
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
        _parent = parent;
    }

    /// <summary>
    /// Gets the name of the file.
    /// </summary>
    public string Name => _dirInfo.Name;

    /// <summary>
    /// Gets the full path of the directory or file.
    /// </summary>
    public string FullName => _dirInfo.FullName;

    private ITestSourceDir? _parent;

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
                throw new ArgumentException("The parent's directory fullname and the value do not match.");
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

    /// <summary>
    /// Provides the enumerator to the ChildItems IEnumerable (List) so it can be iterated
    /// and used in LINQ.
    /// </summary>
    public IEnumerator<ITestSourceItem> GetEnumerator()
    {
        return _childItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Gets and returns a IEnumerable with the files on this directory.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITestSourceFile> GetFiles()
    {
        IEnumerable<ITestSourceFile> files =
            (IEnumerable<ITestSourceFile>)GetTestSourceItemByTypes<ITestSourceFile>();

        return files;
    }

    /// <summary>
    /// Gets the folders contained on this directory.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ITestSourceDir> GetFolders()
    {
        IEnumerable<ITestSourceDir> folders =
            (IEnumerable<ITestSourceDir>)GetTestSourceItemByTypes<ITestSourceDir>();

        return folders;
    }

    /// <summary>
    /// Gets the files from the testsources folder that matches a type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerable<ITestSourceItem> GetTestSourceItemByTypes<T>()
    {
        IEnumerable<ITestSourceItem> files = this.ChildItems;
        IEnumerable<ITestSourceItem>
            filesByType = (IEnumerable<ITestSourceItem>)files.OfType<T>();

        return filesByType;
    }
}
