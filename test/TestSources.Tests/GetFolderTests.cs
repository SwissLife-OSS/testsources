using Xunit;
using FluentAssertions;
using TestSources.Interfaces;

namespace TestSources.Tests;

public class GetFolderTests
{
    static readonly string rootpath = "__testsources__";

    [Fact]
    public void GetFolder_FolderInRoot_IsFoundByNameWithoutSubDirs()
    {
        // Arrange
        string folderName = "FileContentTests";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, false);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
    }

    [Fact]
    public void GetFolder_FolderInRoot_IsFoundByNameIncludingSubDirs()
    {
        // Arrange
        string folderName = "FileContentTests";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
    }


    [Fact]
    public void GetFolder_FolderInSubFolder_IsFoundByNameWithSubDirsExplicit()
    {
        // Arrange
        string folderName = "sub01";
        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
    }

    [Fact]
    public void GetFolder_FolderInSubFolder_IsFoundByNameWithSubDirsImplicit()
    {
        // Arrange
        string folderName = "sub01";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
    }

    [Fact]
    public void GetFolder_FolderInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
    {
        // Arrange
        string folderName = "sub02";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
    }

    [Fact]
    public void GetFolder_FolderInFinalSubFolder_IsFoundByNameWithSubDirsImplicit()
    {
        // Arrange
        string folderName = "sub02";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
    }

    [Fact]
    public void GetFolder_UnexistingFolderInRoot_IsNotFoundByNameWithoutSubdirs()
    {
        // Arrange
        string folderName = "IDoNotExist";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, false);

        // Assert
        testSourcesDir.Should().BeNull();
    }

    [Fact]
    public void GetFolder_UnexistingFolderInRoot_IsNotFoundByNameIncludingSubDirs()
    {
        // Arrange
        string folderName = "IDoNotExist";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().BeNull();
    }

    [Fact]
    public void GetFolder_FolderInRoot_IsNotFoundByNameInLowerCase()
    {
        // Arrange
        string folderName = "filecontenttests";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, false);

        // Assert
        testSourcesDir.Should().BeNull();
    }

    [Fact]
    public void GetFolder_FolderInRootAndFinalSubfolder_OnlyFirstFolderFileIsFound()
    {
        // Arrange
        string folderName = "sub01";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
        testSourcesDir.Parent.Name.Should().Be(GetFolderTests.rootpath);
    }
}
