using System.IO;
using Xunit;

namespace TestSources.Tests
{
    public class TestSourcesTests
    {
        [Fact]
        public void ExistingFile_CanBeFound()
        {
            // arrange
            TestSources ts = new TestSources();

            //act



            //assert
            FileInfo fi = new FileInfo("C:\\GIT\testsources\\src\\TestSources.Tests\\__TestSources__\\sub01\\SubTextFile01.txt");
            DirectoryInfo di = new DirectoryInfo("C:\\GIT\\testsources\\src\\TestSources.Tests\\__TestSources__");
            //FileSystemInfo fsi = new FileSystemInfo();

            // Name , the dir or file name
            var xxname = fi.Name; // or di.Name;
            //File f = new File()

            // Instrad of DirectoryName, we have FullName
            var xxx = fi.FullName; // or di.FullName;
            var xxxx = di.FullName;

            Assert.NotNull(fi);
            Assert.NotNull(di);
        }
    }
}
