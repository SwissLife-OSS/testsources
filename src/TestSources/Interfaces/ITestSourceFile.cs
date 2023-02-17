using System.IO;
using System.Text;
# nullable enable

namespace TestSources.Interfaces;

/// <summary>
/// Allows an object to manage filesystem properties corresponding to a file and read it.
/// </summary>
public interface ITestSourceFile : ITestSourceItem
{
    /// <summary>
    /// Opens an existing file for reading.
    /// </summary>
    /// <returns>A File stream Opened for reading</returns>
    FileStream OpenRead();

    #region As*** Methods

    /// <summary>
    /// Reads the current file and returns its content as an
    /// string with a default UTF8 encoding.
    /// </summary>
    /// <returns>The file contents</returns>
    string AsString();

    /// <summary>
    /// Reads the current file and returns its content as an
    /// string with the provided encoding.
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns>The file contents in string format</returns>
    string AsString(Encoding encoding);

    /// <summary>
    /// Reads the current file and returns a byte array of its content.
    /// </summary>
    /// <returns>A byte array with the file contents</returns>
    byte[] AsByteArray();

    /// <summary>
    /// Reads the current file and returns a FileStream to it
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <returns>A file stream to the file contents</returns>
    FileStream AsFileStream();

    /// <summary>
    /// Reads the current file and returns its contents as a MemoryStream
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <returns>A memory stream to the file contents</returns>
    MemoryStream AsMemoryStream();

    /// <summary>
    /// Reads the current file and returns its contents as a Stream
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <returns>A stream to the file contents</returns>
    Stream AsStream();

    /// <summary>
    /// Returns the hash of a file, given a Cryptographic hash algorithm
    /// </summary>
    /// <param name="testSourceFile"></param>
    /// <param name="hashAlgorithm"></param>
    /// <returns>the file hash</returns>
    string GetHash();

    /// <summary>
    /// Returns the content of a file as a concrete type, deserializing its JSON content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>The file contents as a type instance</returns>
    T? AsType<T>();

    /// <summary>
    /// Reads the current file and returns its content as a
    /// JSON string with a default UTF8 encoding.
    /// </summary>
    /// <returns>The file contents as a Json string</returns>
    string AsJson();

    /// <summary>
    /// Reads the current file and returns its content as a
    /// JSON string with the provided encoding.
    /// </summary>
    /// <param name="encoding"></param>
    /// <returns>The file contents as a Json string</returns>
    string AsJson(Encoding encoding);

    #endregion As*** Methods
}
