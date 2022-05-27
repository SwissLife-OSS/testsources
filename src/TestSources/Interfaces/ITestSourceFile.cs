using System.IO;
using System.Text;

namespace TestSources.Interfaces
{
    /// <summary>
    /// Allows an object to manage filesystem properties corresponding to a file and read it.
    /// </summary>
    public interface ITestSourceFile : ITestSourceItem
    {
        /// <summary>
        /// Opens an existing file for reading.
        /// </summary>
        /// <returns></returns>
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

        #endregion As*** Methods
    }
}
