using System.IO;

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
    }
}
