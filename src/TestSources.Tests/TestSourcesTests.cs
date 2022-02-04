using System.IO;
using Snapshooter.Xunit;
using Xunit;
using FluentAssertions;
using TestSources.Helpers;
using TestSources.Interfaces;

namespace TestSources.Tests
{
    public class TestSourcesTests
    {
        [Fact]
        public void TestSources_Constructor_works()
        {
            // Arrange
            TestSources testSources = new TestSources();

            // Act
            // Assert
            testSources.Name.Should().Be("__testsources__");
            testSources.Parent.Should().BeNull();
        }

        [Fact]
        public void ExistingFile_CanBeFound()
        {
            // arrange
            TestSources ts = new TestSources();
            var projectDirectory = ProjectHelpers.GetProjectPath();
            var path = Path.Combine(projectDirectory, ts.Name);

            //act            
            //assert
            ts.FullName.Should().Be(path);
            ts.FilesAndFolders.Should().NotBeEmpty();
            ts.GetItems().Should().NotBeEmpty();
        }

    }
}
