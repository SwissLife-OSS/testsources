using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
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
        private static HashAlgorithm DefaultHash = SHA256.Create();
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

        /// <summary>
        /// Reads the current file and returns a byte array of its content.
        /// </summary>
        /// <returns></returns>
        public static byte[] AsByteArrayExt(
            this ITestSourceFile testSourceFile)
        {
            return File.ReadAllBytes(testSourceFile.FullName);
        }

        /// <summary>
        /// Reads the current file and returns a FileStream to it
        /// </summary>
        /// <param name="testSourceFile"></param>
        /// <returns></returns>
        public static FileStream AsFileStreamExt(
            this ITestSourceFile testSourceFile)
        {
            return File.OpenRead(testSourceFile.FullName);
        }

        /// <summary>
        /// Reads the current file and returns its contents as a MemoryStream
        /// </summary>
        /// <param name="testSourceFile"></param>
        /// <returns></returns>
        public static MemoryStream AsMemoryStreamExt(
            this ITestSourceFile testSourceFile)
        {
            MemoryStream memoryStream = new MemoryStream();
            AsFileStreamExt(testSourceFile).CopyTo(memoryStream);
            return memoryStream;
        }

        /// <summary>
        /// Reads the current file and returns its contents as a Stream
        /// </summary>
        /// <param name="testSourceFile"></param>
        /// <returns></returns>
        public static Stream AsStreamExt(
            this ITestSourceFile testSourceFile)
        {
            return (Stream)AsMemoryStreamExt(testSourceFile);
        }

        /// <summary>
        /// Returns the hash of a file, given a Cryptographic hash algorithm
        /// </summary>
        /// <param name="testSourceFile"></param>
        /// <param name="hashAlgorithm"></param>
        /// <returns></returns>
        public static string GetHashExt(
            this ITestSourceFile testSourceFile,
            HashingAlgorithms hashAlgorithm = HashingAlgorithms.SHA256)
        {
            using (var hasher =
                HashAlgorithm.Create(hashAlgorithm.ToString()))
            {
                using (FileStream stream =
                    File.OpenRead(testSourceFile.FullName))
                {
                    byte[] hashBytes = hasher.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "");
                }
            }
        }

    }
}
