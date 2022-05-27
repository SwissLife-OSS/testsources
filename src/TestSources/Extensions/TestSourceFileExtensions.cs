using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestSources.Interfaces;

namespace TestSources.Extensions
{
    /// <summary>
    /// Provides the ability to Get the file as a certain type as well as
    /// to verify it on certain ways.
    /// </summary>
    public static class TestSourceFileExtensions
    {
        private static Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Reads the current file and returns its content as an
        /// string with a default UTF8 encoding.
        /// </summary>
        /// <param name="testSourceFile"></param>
        /// <returns>The file contents</returns>
        public static string AsStringExt(
            this ITestSourceFile testSourceFile)
        {
            return AsStringExt(testSourceFile, DefaultEncoding);
        }

        /// <summary>
        /// Reads the current file and returns its content as an
        /// string with the provided encoding.
        /// </summary>
        /// <param name="testSourceFile"></param>
        /// <param name="encoding"></param>
        /// <returns>The file contents in string format</returns>
        public static string AsStringExt(
            this ITestSourceFile testSourceFile,
            Encoding encoding)
        {
            return File.ReadAllText(testSourceFile.FullName, encoding);
        }

    }
}
