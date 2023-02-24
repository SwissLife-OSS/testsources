using Xunit;
using TestSources.Interfaces;
using FluentAssertions;

namespace TestSources.Examples.Xunit;

public class GetFolderTests
{
    static readonly string rootpath = "__testsources__";

    /// <summary>
    /// This will find a folder named FileContentTests in the rooot of
    /// the __testsources__ folder and return it fulfilling the
    /// ITestSourceDir interface. Which we check.
    /// </summary>
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
        testSourcesDir.GetFiles().Should().HaveCount(2);
        testSourcesDir.GetFolders().Should().HaveCount(0);
        testSourcesDir.Parent.Name.Should().Be(rootpath);
    }

    /// <summary>
    /// This will find a folder in the root __testsources__ directory,
    /// searching for subdirectories.
    /// </summary>
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

    /// <summary>
    /// Tests that a directory in a subfolder can be found in a hierarchical
    /// search (including subdirectories).
    /// </summary>
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

    /// <summary>
    /// Checks that a folder in the last level can be found.
    /// </summary>
    [Fact]
    public void GetFolder_FolderInFinalSubFolder_IsFoundByNameWithSubDirsSearch()
    {
        // Arrange
        string folderName = "sub03";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().NotBeNull();
        testSourcesDir.Name.Should().Be(folderName);
    }

    /// <summary>
    /// Tests that a folder cannot be found if does not exist.
    /// </summary>
    [Fact]
    public void GetFolder_UnexistingFolder_IsNotFound()
    {
        // Arrange
        string folderName = "IDoNotExist";

        // Act
        ITestSourceDir testSourcesDir = TestSource.GetFolder(folderName, true);

        // Assert
        testSourcesDir.Should().BeNull();
    }
}
