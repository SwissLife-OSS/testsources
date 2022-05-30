using Xunit;
using FluentAssertions;
using TestSources.Interfaces;
using System.Text;
using System.IO;
using Snapshooter.Xunit;

namespace TestSources.Tests
{
    public class TestSourceFileExtensionsTests
    {
        [Fact]
        public void FileExtensionAsString_Succeeds()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            ITestSourceFile file = TestSource.GetFile(fileName, true);
            string textInFile = file.AsString();

            string fileContentsAsString =
                TestSource.GetFile(fileName, true)
                .AsString();

            // Assert
            fileContentsAsString.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FileExtensionAsStringUTF8_Succeeds()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            ITestSourceFile file = TestSource.GetFile(fileName, true);
            string textInFile = file.AsString(Encoding.UTF8);

            string fileContentsAsString =
                TestSource.GetFile(fileName, true)
                .AsString();

            // Assert
            fileContentsAsString.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FileExtensionAsByteArray_Succeeds()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            byte[] fileContentsAsByteArray =
                TestSource.GetFile(fileName, true)
                .AsByteArray();

            // Assert
            fileContentsAsByteArray.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void FileExtensionAsFileStream_Succeeds()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            FileStream fileStream =
                TestSource.GetFile(fileName, true)
                .AsFileStream();
            ITestSourceFile file = TestSource.GetFile(fileName, true);

            // Assert
            fileStream.Should().NotBeNull();
            byte[] buff = null;
            BinaryReader br = new BinaryReader(fileStream);
            long numBytes = new FileInfo(file.FullName).Length;
            buff = br.ReadBytes((int)numBytes);
            buff.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void FileExtensionAsMemoryStream_Succeeds()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            MemoryStream memoryStream =
                TestSource.GetFile(fileName, true)
                .AsMemoryStream();
            ITestSourceFile file = TestSource.GetFile(fileName, true);

            // Assert
            memoryStream.Should().NotBeNull();
            memoryStream.ToArray().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void FileExtensionAsStream_Succeeds()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            Stream stream =
                TestSource.GetFile(fileName, true)
                .AsStream();

            // Assert
            stream.Should().NotBeNull();
            byte[] fileContents = (stream as MemoryStream).ToArray();
            fileContents.Should().NotBeNullOrEmpty();   
        }

        [Fact]
        public void FileExtensionGetHash_Succeeds()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            string stringHash =
                TestSource.GetFile(fileName, true)
                .GetHash();

            // Assert
            stringHash.MatchSnapshot();
        }
    }
}