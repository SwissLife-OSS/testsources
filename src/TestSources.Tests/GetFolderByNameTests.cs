using Xunit;
using FluentAssertions;
using TestSources.Interfaces;

namespace TestSources.Tests
{
    public class GetFolderByNameTests
    {
        static readonly string rootpath = "__testsources__";

        [Fact]
        public void GetFolderByName_FolderInRoot_IsFoundByNameWithoutSubDirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "FileContentTests";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_FolderInRoot_IsFoundByNameIncludingSubDirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "FileContentTests";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }


        [Fact]
        public void GetFolderByName_FolderInSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "sub01";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_FolderInSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "sub01";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        //Sub2FolderBinFile.rar
        [Fact]
        public void GetFolderByName_FolderInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "sub02";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_FolderInFinalSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "sub02";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_UnexistingFolderInRoot_IsNotFoundByNameWithoutSubdirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "IDoNotExist";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFolderByName_UnexistingFolderInRoot_IsNotFoundByNameIncludingSubDirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "IDoNotExist";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFolderByName_FolderInRoot_IsNotFoundByNameInLowerCase()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "filecontenttests"; //FileContentTests
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFolderByName_FolderInRootAndFinalSubfolder_OnlyFirstFolderFileIsFound()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "sub01";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
            testSourcesItem.Parent.Name.Should().Be(GetFolderByNameTests.rootpath);
        }
    }
}
