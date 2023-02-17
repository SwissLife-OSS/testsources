using System;
using System.IO;
using System.Text;
using TestSources.Interfaces;
using TestSources.Extensions;

namespace TestSources;

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
    /// <returns>A File stream Opened for reading</returns>
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

    /// <summary>
    /// Reads the current file and returns a byte array of its content.
    /// </summary>
    /// <returns>A byte array with the file contents</returns>
    public byte[] AsByteArray() =>
        this.AsByteArrayExt();

    /// <summary>
    /// Reads the current file and returns a FileStream to it
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <returns>A file stream to the file contents</returns>
    public FileStream AsFileStream() =>
        this.AsFileStreamExt();

    /// <summary>
    /// Reads the current file and returns its contents as a MemoryStream
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <returns>A memory stream to the file contents</returns>
    public MemoryStream AsMemoryStream() =>
        this.AsMemoryStreamExt();

    /// <summary>
    /// Reads the current file and returns its contents as a Stream
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <returns>A stream to the file contents</returns>
    public Stream AsStream() =>
        this.AsStreamExt();

    /// <summary>
    /// Returns the hash of a file, given a Cryptographic hash algorithm
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <param name="hashAlgorithm"></param>
    /// <returns>the file hash</returns>
    public string GetHash() =>
        this.GetHashExt();

    /// <summary>
    /// Returns the content of a file as a concrete type, deserializing its JSON content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>The file contents as a type instance</returns>
    public T? AsType<T>() =>
        this.AsTypeExt<T>();

    /// <summary>
    /// Reads the current file and returns its content as a
    /// JSON string with a default UTF8 encoding.
    /// </summary>
    /// <returns>The file contents as a Json string</returns>
    public string AsJson() =>
        this.AsJsonExt();

    /// <summary>
    /// Reads the current file and returns its content as a
    /// JSON string with the provided encoding.
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns>The file contents as a Json string</returns>
    public string AsJson(Encoding encoding) =>
        this.AsJsonExt(encoding);

    #endregion As*** Methods

}
