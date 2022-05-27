using Xunit;
using FluentAssertions;
using TestSources.Interfaces;

namespace TestSources.Tests
{
    public class TestSourceFileExtensionsTests
    {
        [Fact]
        public void FileExtensionAsString_Succeeds()
        {
            // Arrange
            string fileName = "SomeJson.json";

            // Act
            ITestSourceFile file = TestSource.GetFile(fileName, true);
            string textInFile = file.AsString();

            string fileContentsAsString =
                TestSource.GetFile(fileName, true)
                .AsString();

            // Assert
            fileContentsAsString.Should().NotBeNullOrWhiteSpace();
        }

    }
}
