using System.Collections.Generic;
using System.Linq;
using TestSources.Interfaces;

namespace TestSources.Extensions
{
    /// <summary>
    /// provides the ability to get directories or files to ITestSourceDir
    /// </summary>
    public static class TestSourceDirExtensions
    {
        /// <summary>
        /// Gets the directories contained in a directory
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static IEnumerable<ITestSourceDir> GetDirectories(
            this ITestSourceDir dir,
            bool recursive = false)
                => dir.GetItems(recursive).OfType<ITestSourceDir>();

        /// <summary>
        /// Gets the files contained in a directory
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static IEnumerable<ITestSourceFile> GetFiles(
            this ITestSourceDir dir,
            bool recursive = false)
            => dir.GetItems(recursive).OfType<ITestSourceFile>();
    }
}
