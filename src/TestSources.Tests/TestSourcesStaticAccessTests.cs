using System.IO;
using Snapshooter.Xunit;
using Xunit;
using FluentAssertions;
using TestSources.Helpers;
using TestSources.Interfaces;
using TestSources;

namespace TestSources.Tests
{
    public class TestSourcesStaticAccessTests
    {
        [Fact]
        public void TestSources_Constructor_works()
        {
            // Arrange
            // Act
            // Assert
            TestSource.Name.Should().Be("__testsources__");
            TestSource.Parent.Should().BeNull();
        }

        [Fact]
        public void ExistingFile_CanBeFound()
        {
            // arrange
            var projectDirectory = ProjectHelpers.GetProjectPath();
            var path = Path.Combine(projectDirectory, TestSource.Name);

            //act            
            //assert
            TestSource.FullName.Should().Be(path);
            TestSource.FilesAndFolders.Should().NotBeEmpty();
            TestSource.GetItems().Should().NotBeEmpty();
        }

        [Fact]
        public void GetFileByName_FileInRoot_IsFoundByNameWithoutSubDirs()
        {
            // Arrange
            string fileName = "BinFile01.rar";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFileByName(fileName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFileByName_FileInFinalSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            string fileName = "Sub2FolderBinFile.rar";
            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFileByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFolderByName_FolderInRoot_IsFoundByNameWithoutSubDirs()
        {
            // Arrange
            string folderName = "FileContentTests";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFolderByName(folderName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_FolderInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            string folderName = "sub02";

            // Act
            ITestSourceItem testSourcesItem = TestSource.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }
    }
}
