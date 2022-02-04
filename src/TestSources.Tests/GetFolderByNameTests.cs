using System.IO;
using Snapshooter.Xunit;
using Xunit;
using FluentAssertions;
using TestSources.Helpers;
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
        public void GetFolderByName_FileInSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "SomeJson.json";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_FileInSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "SomeJson.json";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        //Sub2FolderBinFile.rar
        [Fact]
        public void GetFolderByName_FileInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "Sub2FolderBinFile.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_FileInFinalSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "Sub2FolderBinFile.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
        }

        [Fact]
        public void GetFolderByName_UnexistingFileInRoot_IsNotFoundByNameWithoutSubdirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "IDoNotExist.ext";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFolderByName_UnexistingFileInRoot_IsNotFoundByNameIncludingSubDirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "IDoNotExist.ext";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, true);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFolderByName_FileInRoot_IsNotFoundByNameInLowerCase()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "binfile01.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        //TextFile01.txt
        [Fact]
        public void GetFolderByName_FileInRootAndFinalSubfolder_OnlyFirstFolderFileIsFound()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string folderName = "TextFile01.txt";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFolderByName(folderName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(folderName);
            testSourcesItem.Parent.Name.Should().Be(GetFolderByNameTests.rootpath);

        }
    }
}
