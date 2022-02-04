using System.IO;
using Snapshooter.Xunit;
using Xunit;
using FluentAssertions;
using TestSources.Helpers;
using TestSources.Interfaces;

namespace TestSources.Tests
{
    public class GetFileByNameTests
    {
        static readonly string rootpath = "__testsources__";

        [Fact]
        public void GetFileByName_FileInRoot_IsFoundByNameWithoutSubDirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "BinFile01.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFileByName_FileInRoot_IsFoundByNameIncludingSubDirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "BinFile01.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }


        [Fact]
        public void GetFileByName_FileInSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "SomeJson.json";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFileByName_FileInSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "SomeJson.json";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        //Sub2FolderBinFile.rar
        [Fact]
        public void GetFileByName_FileInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "Sub2FolderBinFile.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFileByName_FileInFinalSubFolder_IsFoundByNameWithSubDirsImplicit()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "Sub2FolderBinFile.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, true);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
        }

        [Fact]
        public void GetFileByName_UnexistingFileInRoot_IsNotFoundByNameWithoutSubdirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "IDoNotExist.ext";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFileByName_UnexistingFileInRoot_IsNotFoundByNameIncludingSubDirs()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "IDoNotExist.ext";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, true);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        [Fact]
        public void GetFileByName_FileInRoot_IsNotFoundByNameInLowerCase()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "binfile01.rar";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, false);

            // Assert
            testSourcesItem.Should().BeNull();
        }

        //TextFile01.txt
        [Fact]
        public void GetFileByName_FileInRootAndFinalSubfolder_OnlyFirstFolderFileIsFound()
        {
            // Arrange
            TestSources testSources = new TestSources();
            string fileName = "TextFile01.txt";
            // Act
            ITestSourceItem testSourcesItem = testSources.GetFileByName(fileName, false);

            // Assert
            testSourcesItem.Should().NotBeNull();
            testSourcesItem.Name.Should().Be(fileName);
            testSourcesItem.Parent.Name.Should().Be(GetFileByNameTests.rootpath);

        }
    }
}
