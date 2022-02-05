using Xunit;
using FluentAssertions;
using TestSources.Interfaces;

namespace TestSources.Tests
{
    public class GetByNameTests
    {
        static readonly string rootpath = "__testsources__";

        [Fact]
        public void GetByName_FileInRoot_IsFoundByNameWithoutSubDirs()
        {
            // Arrange
            string fileName = "BinFile01.rar";
            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetByName_FileInRoot_IsFoundByNameIncludingSubDirs()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetByName_FileInSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetByName_FileInSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetByName_FileInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            string fileName = "Sub2FolderBinFile.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetByName_FileInFinalSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            string fileName = "Sub2FolderBinFile.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetByName_UnexistingFileInRoot_IsNotFoundByNameWithoutSubdirs()
        {
            // Arrange
            string fileName = "IDoNotExist.ext";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetByName_UnexistingFileInRoot_IsNotFoundByNameIncludingSubDirs()
        {
            // Arrange
            string fileName = "IDoNotExist.ext";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, true);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetByName_FileInRoot_IsNotFoundByNameInLowerCase()
        {
            // Arrange
            string fileName = "binfile01.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetByName_FileInRootAndFinalSubfolder_OnlyFirstFolderFileIsFound()
        {
            // Arrange
            string fileName = "TextFile01.txt";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetByName(fileName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
            testSourcesItem.Parent.Name.Should().Be(GetByNameTests.rootpath);
        }
    }
}
