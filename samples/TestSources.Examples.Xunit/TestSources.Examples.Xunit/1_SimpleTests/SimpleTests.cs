using Xunit;
using TestSources.Interfaces;
using FluentAssertions;

namespace TestSources.Examples.Xunit;

public class SimpleTests
{
    static readonly string rootpath = "__testsources__";

    /// <summary>
    /// This will find a file named BinFile01.rar in the rooot of
    /// the __testsources__ folder and return it fulfilling the
    /// ITestSourceFile interface. Which we check.
    /// </summary>
    [Fact]
    public void GetFile_WhichExists_Succeeds()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        
        ITestSourceFile testSourcesItem = TestSource.GetFile(fileName);

        // Assert
        testSourcesItem.Should().NotBeNull();
        testSourcesItem.Name.Should().Be(fileName);
        testSourcesItem.FullName.Should().NotBeNullOrWhiteSpace();
        testSourcesItem.Parent.Name.Should().Be(rootpath);
    }
}
