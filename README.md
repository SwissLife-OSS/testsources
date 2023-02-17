![TestSources](https://raw.githubusercontent.com/SwissLife-OSS/testsources/main/logo.png)
## [![Nuget](https://img.shields.io/nuget/v/testsources.svg?style=flat)](https://www.nuget.org/packages/testsources.Xunit) [![GitHub Release](https://img.shields.io/github/release/SwissLife-OSS/testsources.svg?style=flat)](https://github.com/SwissLife-OSS/testsources/releases/latest) [![Build Status](https://dev.azure.com/swisslife-oss/swisslife-oss/_apis/build/status/testsources.Release?branchName=main)](https://dev.azure.com/swisslife-oss/swisslife-oss/_build/latest?definitionId=6&branchName=master) [![Coverage Status](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_TestSources&metric=coverage)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_TestSources) [![Quality](https://sonarcloud.io/api/project_badges/measure?project=SwissLife-OSS_TestSources&metric=alert_status)](https://sonarcloud.io/dashboard?id=SwissLife-OSS_TestSources)

**TestSources is a test file management tool for _.NET Core_ and _.NET Framework_**

_TestSources_ makes easy to handle, organize and use test files from a .NET test. It greatly simplifies the work of setting up file management tools, validations and access to them that you have to set up manually on every test project that manages files of any kind.


## Getting Started

To get started, install the [TestSources nuget package](https://www.nuget.org/packages/TestSources):

### In your favorite test tool, XUnit, NUnit or MSTest
```bash
dotnet add package TestSources
```

### Setting up TestSources
In the root of the project, add a New Folder, named "`__testsources__`".
It should be in lowercase, with two underscore characters "`_`" before and after.
Inside our tessources folder, create a text file named "aTextFile.txt" and inside add "Some text", or any text of your liking.

### Using TestSources to get a file
To get this test file and use it within your unit (or integration) tests, follow the next step:

#### 1. Get the file

Insert a TestSources GetFile statement `TestSource.GetFile("aTextFile.txt");` into your unit test.

Example:

```csharp
/// <summary>
/// Tests if the test file contains some text.
/// </summary>
[Fact]
public void TestThatTheTextFileContainsText()
{
    // arrange
    string fileName = "aTextFile.txt";

    // act
    string textInsideTheFile =
        TestSource.GetFile(fileName)
        .AsString();

    // Assert
    Assert.NotEmpty(textInsideTheFile);
}
```

#### 2. Run the unit test to Assert that the file is not empty

The `TestSource.GetFile()` statement will obtain the file with the specified name located on the  "`__testsources__`" folder.
Next, the metachained `AsString()` statement will obtain its contents and deliver them ready to use in a string form, with UTF-8 encoding. (it is configurable too)
<br><br>        
    
### It is herarchical (files and folders)

You can create folders inside the "`__testsources__`" folder, without any limit except the one set up by your operating system.<br>    
Same goes with files, you can add them at any level and use your imagination to organize your test files until it pleases you.<br>  

The statement `TestSource.GetFile(filename, true)` will get any file located under the testsources root directory, given that it exists and you have typed it properly.<br>    
  
In addition you can get a reference to a folder with a similar statement:  

`TestSource.GetFolder(foldername)` will find the folder named `foldername` in the root of the testsources root directory, whereas `TestSource.GetFile(filename, true)` will find the named folder in any of its subfolders.
<br>   

#### Managing files: ITestSourceFile
The files fulfill the `ITestSourceFile` Interface and this enables us to check its parent, get the file name, the full name including the path and then some extensions such as:
 - `OpenRead()` - Opens a file for reading returning a file stream.
 - `AsString()` - Reads the current file and returns its content as an string with a default UTF8 encoding, which can be overridden.
 - `AsByteArray()` - Reads the current file and returns a byte array of its content.
 - `AsFileStream()` - Reads the current file and returns a FileStream to it.
 - `AsMemoryStream()` - Reads the current file and returns its contents as a MemoryStream.
 - `AsStream()` - Reads the current file and returns its contents as a Stream.
 - `GetHash()` - Returns the hash of a file, given a Cryptographic hash algorithm.
 - `AsType<T>()` - Returns the content of a file as a concrete type, deserializing its JSON content.
 - `AsJson()` - Reads the current file and returns its content as a JSON string with a default UTF8 encoding, which can be overridden.
 <br>  

 #### Managing folders: ITestSourceDir
The folders or directories fulfill the `ITestSourceDir` Interface and this enables us to check its parent, get the folder name, the full name including the path and then some extensions such as:
 - `GetFiles()` - Returns a list of the files contained on this folder. Of course, fulfilling the ITestSourceFile interface.
 - `GetFolders()` - Returns a list of the folders contained on this folder. Of course, fulfilling the ITestSourceDir interface.
(if no files or folders, the collection is empty)


## Using testsources in CI-Builds

They simply work, nothing specially to set up there.

## Community

This project has adopted the code of conduct defined by the [Contributor Covenant](https://contributor-covenant.org/)
to clarify expected behavior in our community. For more information, see the [Swiss Life OSS Code of Conduct](https://swisslife-oss.github.io/coc).
