using Xunit;
using TestSources.Interfaces;
using FluentAssertions;
using System.Text;
using Snapshooter.Xunit;

namespace TestSources.Examples.Xunit;

public class AsTypeTests
{
    static readonly string rootpath = "__testsources__";


    /// <summary>
    /// Tests that we can get a file AsString()
    /// </summary>
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

    /// <summary>
    /// Tests that we can get a file AsString() with UTF8 encoding 
    /// </summary>
    [Fact]
    public void FileExtensionAsStringUTF8_Succeeds()
    {
        // Arrange
        string fileName = "SomeJson.json";

        // Act
        string fileContentsAsString =
            TestSource.GetFile(fileName, true)
            .AsString(Encoding.UTF8);

        // Assert
        fileContentsAsString.Should().NotBeNullOrWhiteSpace();
    }

    /// <summary>
    /// Tests that we can get a byte array from a file.
    /// </summary>
    [Fact]
    public void FileExtensionAsByteArray_Succeeds()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        byte[] fileContentsAsByteArray =
            TestSource.GetFile(fileName, true)
            .AsByteArray();

        // Assert
        fileContentsAsByteArray.Should().NotBeNullOrEmpty();
    }

    /// <summary>
    /// Tests that we can get a file stream from a file
    /// and that it is properly read, with data.
    /// </summary>
    [Fact]
    public void FileExtensionAsFileStream_Succeeds()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        FileStream fileStream =
            TestSource.GetFile(fileName, true)
            .AsFileStream();

        // Assert
        fileStream.Should().NotBeNull();
        byte[] buff = null;
        BinaryReader br = new BinaryReader(fileStream);
        buff = br.ReadBytes((int)fileStream.Length);
        buff.Should().NotBeNullOrEmpty();
    }

    /// <summary>
    /// Tests that We can read a file as a memory stream.
    /// </summary>
    [Fact]
    public void FileExtensionAsMemoryStream_Succeeds()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        MemoryStream memoryStream =
            TestSource.GetFile(fileName, true)
            .AsMemoryStream();

        // Assert
        memoryStream.Should().NotBeNull();
        memoryStream.ToArray().Should().NotBeNullOrEmpty();
    }

    /// <summary>
    /// Checks that we can read a file as a stream.
    /// </summary>
    [Fact]
    public void FileExtensionAsStream_Succeeds()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        Stream stream =
            TestSource.GetFile(fileName, true)
            .AsStream();

        // Assert
        stream.Should().NotBeNull();
        byte[] fileContents = (stream as MemoryStream).ToArray();
        fileContents.Should().NotBeNullOrEmpty();
    }

    /// <summary>
    /// Checks that we can get the hash from a binary file and
    /// it matches the previously generated hash (Hashes should
    /// always be the same on binary files)
    /// </summary>
    [Fact]
    public void FileExtensionGetHash_Succeeds()
    {
        // Arrange
        string fileName = "BinFile01.rar";

        // Act
        string stringHash =
            TestSource.GetFile(fileName, true)
            .GetHash();

        // Assert
        stringHash.MatchSnapshot();
    }

    /// <summary>
    /// Validates that a file containing a Json string
    /// can be properly serialized into its type.
    /// </summary>
    [Fact]
    public void FileExtensionGetType_Succeeds()
    {
        // Arrange
        string fileName = "ComplexObject.txt";

        // Act
        RootObject rootObj =
            TestSource.GetFile(fileName, false)
            .AsType<RootObject>();

        // Assert
        rootObj.MatchSnapshot();
    }

    /// <summary>
    /// Checks that a file can be verified as a valid JSON and
    /// loaded into a JSON string.
    /// </summary>
    [Fact]
    public void FileExtensionAsJson_Default_Succeeds()
    {
        // Arrange
        string fileName = "SomeJson.json";

        // Act
        string fileContentsAsJsonString =
            TestSource.GetFile(fileName, true)
            .AsJson();

        // Assert
        fileContentsAsJsonString.Should().NotBeNullOrWhiteSpace();
    }

    public class GeoCoordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class Tourist
    {
        public string Name { get; set; }
        public string Shorttext { get; set; }
        public GeoCoordinates GeoCoordinates { get; set; }
        public List<string> Images { get; set; }
    }

    public class City
    {
        public List<Tourist> Tourist { get; set; }
    }

    public class RootObject
    {
        public List<City> city { get; set; }
    }
}
