using Xunit;
using FluentAssertions;
using TestSources.Interfaces;

namespace TestSources.Tests
{
    public class GetFileTests
    {
        static readonly string rootpath = "__testsources__";

        [Fact]
        public void GetFile_FileInRoot_IsFoundByNameWithoutSubDirs()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFile_FileInRoot_IsFoundByNameIncludingSubDirs()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFile_FileInSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFile_FileInSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFile_FileInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            string fileName = "Sub2FolderBinFile.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFile_FileInFinalSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            string fileName = "Sub2FolderBinFile.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFile_UnexistingFileInRoot_IsNotFoundByNameWithoutSubdirs()
        {
            // Arrange
            string fileName = "IDoNotExist.ext";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFile_UnexistingFileInRoot_IsNotFoundByNameIncludingSubDirs()
        {
            // Arrange
            string fileName = "IDoNotExist.ext";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, true);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFile_FileInRoot_IsNotFoundByNameInLowerCase()
        {
            // Arrange
            string fileName = "binfile01.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFile_FileInRootAndFinalSubfolder_OnlyFirstFolderFileIsFound()
        {
            // Arrange
            string fileName = "TextFile01.txt";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFile(fileName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
            testSourcesItem.Parent.Name.Should().Be(GetFileTests.rootpath);
        }
    }
}
