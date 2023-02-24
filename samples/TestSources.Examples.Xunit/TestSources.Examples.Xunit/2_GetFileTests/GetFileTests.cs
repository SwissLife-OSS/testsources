using Xunit;
using TestSources.Interfaces;
using FluentAssertions;

namespace TestSources.Examples.Xunit;

public class GetFileTests
{
    static readonly string rootpath = "__testsources__";

    /// <summary>
    /// This will find a file named BinFile01.rar using the 
    /// simple invocation (no subfolders are explored).
    /// With the file is located in the rooot of the
    /// __testsources__ folder. It will be found and returned
    /// fulfilling the ITestSourceFile interface.
    /// </summary>
    [Fact]
    public void GetFile_FileInRoot_IsFoundByName()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName);

        // Assert
        testSourcesItem.Should().NotBeNull();
        testSourcesItem.Name.Should().Be(fileName);
    }

    /// <summary>
    /// This will find a file named BinFile01.rar in the rooot of
    /// the __testsources__ folder and return it fulfilling the
    /// ITestSourceFile interface.
    /// </summary>
    [Fact]
    public void GetFile_FileInRoot_IsFoundByNameWithoutSubDirs()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName, false);

        // Assert
        testSourcesItem.Should().NotBeNull();
        testSourcesItem.Name.Should().Be(fileName);
    }

    /// <summary>
    /// GetFile finds a file in root while also exploring subdirectories.
    /// </summary>
    [Fact]
    public void GetFile_FileInRoot_IsFoundByNameIncludingSubDirs()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName, true);

        // Assert
        testSourcesItem.Should().NotBeNull();
        testSourcesItem.Name.Should().Be(fileName);
    }

    /// <summary>
    /// Tests that we can find a file located in one of the __testsources__
    /// subfolders.
    /// </summary>
    [Fact]
    public void GetFile_FileInSubFolder_IsFoundByNameWithSubDirsExplicit()
    {
        // Arrange
        string fileName = "SomeJson.json";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName, true);

        // Assert
        testSourcesItem.Should().NotBeNull();
        testSourcesItem.Name.Should().Be(fileName);
    }

    /// <summary>
    ///  A File in a subfolders should not be found if searched
    ///  without subfolders, which happens by default.
    /// </summary>
    [Fact]
    public void GetFile_FileInSubFolder_IsFoundByNameWithSubDirsImplicit()
    {
        // Arrange
        string fileName = "SomeJson.json";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName);

        // Assert
        testSourcesItem.Should().BeNull(); // Fails as it is not hierarchical
    }

    /// <summary>
    /// Finding a file at the end of the subfolder chain should succeed if the
    /// file exists.
    /// </summary>
    [Fact]
    public void GetFile_FileInFinalSubFolder_IsFoundByNameWithSubDirsExplicit()
    {
        // Arrange
        string fileName = "Sub2FolderBinFile.rar";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName, true);

        // Assert
        testSourcesItem.Should().NotBeNull();
        testSourcesItem.Name.Should().Be(fileName);
    }

    /// <summary>
    /// An unexisting file should not be found.
    /// </summary>
    [Fact]
    public void GetFile_UnexistingFileInRoot_IsNotFoundByNameWithoutSubdirs()
    {
        // Arrange
        string fileName = "IDoNotExist.ext";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName, false);

        // Assert
        testSourcesItem.Should().BeNull();
    }

    /// <summary>
    /// An unexisting file should not be found, even if we explore all
    /// directories under __testsources__ it should not be there if is not.
    /// </summary>
    [Fact]
    public void GetFile_UnexistingFileInRoot_IsNotFoundByNameIncludingSubDirs()
    {
        // Arrange
        string fileName = "IDoNotExist.ext";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName, true);

        // Assert
        testSourcesItem.Should().BeNull();
    }

    /// <summary>
    /// Given two files with the same exact name, only the first one
    /// will be found and returned.
    /// </summary>
    [Fact]
    public void GetFile_FileInRootAndFinalSubfolder_OnlyFirstFolderFileIsFound()
    {
        // Arrange
        string fileName = "TextFile01.txt";

        // Act
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName, false);

        // Assert
        testSourcesItem.Should().NotBeNull();
        testSourcesItem.Name.Should().Be(fileName);
        testSourcesItem.Parent.Name.Should().Be(GetFileTests.rootpath);
    }
}
