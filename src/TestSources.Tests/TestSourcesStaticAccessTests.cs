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
            TestSources.TestSource.Name.Should().Be("__testsources__");
            TestSources.TestSource.Parent.Should().BeNull();
        }

        [Fact]
        public void ExistingFile_CanBeFound()
        {
            // arrange
            var projectDirectory = ProjectHelpers.GetProjectPath();
            var path = Path.Combine(projectDirectory, TestSources.TestSource.Name);

            //act            
            //assert
            TestSources.TestSource.FullName.Should().Be(path);
            TestSources.TestSource.FilesAndFolders.Should().NotBeEmpty();
            TestSources.TestSource.GetItems().Should().NotBeEmpty();
        }

    }
}
