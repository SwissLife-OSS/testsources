using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using TestSources.Helpers;
using TestSources.Model;
using System.Diagnostics;
using System.Linq;

namespace TestSources.Generators
{
    [Generator]
    public class TestSourcesGenerator : ISourceGenerator
    {
        const string _TestSourcesFolder = "__TestSources__";
        private Dictionary<string, int> _classNames =
            new Dictionary<string, int>();

        private string _assemblyVersion = "0.0.0";

        public void Execute(GeneratorExecutionContext context)
        {
            // we want to go over the testsources folder and traverse through all the elements of
            // this root folder, for each element:
            //  - if we find a folder, process it like the root folder (recursive function)
            //  - if we find a file, generate the corresponding class for the file.
            //  - once the folder is processed, generate the class for the folder.
            //
            // Note that the root folder is a special one and will difer from the others.

            // for each class we will use a similar concept as
            // https://github.com/thomasclaudiushuber/mvvmgen/blob/main/src/MvvmGen.SourceGenerators/ViewModelBuilder.cs
            _assemblyVersion = GetType().Assembly.GetName().Version.ToString(3);
            var NumAdditionalFiles = context.AdditionalFiles.Length.ToString();

            // begin creating the source we'll inject into the users compilation
            var sourceBuilder = new StringBuilder(@"

// v11
using System;

namespace TestSources
{
    public partial class TestSources 
    {
        public static void ExploreTestSources() 
        {
            Console.WriteLine(""Exploration of Test Sources!"");
");
            string ident = "            ";
            sourceBuilder.AppendLine(ident + $@"// TestSources Generator version 0.1.10");
            sourceBuilder.AppendLine(ident + $@"// Detected {NumAdditionalFiles} Additional files.");
            sourceBuilder.AppendLine(ident + $@"// The TestSources directory is: {_TestSourcesFolder} ");
            sourceBuilder.AppendLine($@" ");
            sourceBuilder.AppendLine(ident + $@"// Obtaining the {_TestSourcesFolder} full path. ");
            var firstFileInTestSources = context.AdditionalFiles.First().Path;
            DirectoryInfo testSourcesDirectory = GetDirectoryInfoFromFolderName(
                _TestSourcesFolder,
                firstFileInTestSources);
            if (testSourcesDirectory is not null)
            {
                if (Directory.Exists(testSourcesDirectory.FullName))
                {
                    //  ProcessTestSourcesDirectory(testSourcesDirectory.FullName, 0, context);
                }

                // Check
                sourceBuilder.AppendLine(ident + $@"// Its parent directory is {testSourcesDirectory.Name}. ");
                sourceBuilder.AppendLine(ident + $@"// With full directory {testSourcesDirectory.FullName}. ");
                sourceBuilder.AppendLine($@" ");
                sourceBuilder.AppendLine(ident + $@"Console.WriteLine(@""Exploring context.AdditionalFiles"");");
                foreach (AdditionalText contextAdditionalFile in context.AdditionalFiles)
                {
                    sourceBuilder.AppendLine(ident + $@"Console.WriteLine(@"" - {contextAdditionalFile.Path}"");");
                }
            }
            else
            {
                sourceBuilder.AppendLine(ident + $@"// The {_TestSourcesFolder} folder could not be found." );
                sourceBuilder.AppendLine(ident + $@"// it can be that the folder was not added to additional files." );
                sourceBuilder.AppendLine(ident + $@"// To do so, edit the prj project file and add the following: ");
                sourceBuilder.AppendLine(ident + $@"// < ItemGroup >");
                sourceBuilder.AppendLine(ident + $@"//    < AdditionalFiles Include = '__TestSources__\**' />");
                sourceBuilder.AppendLine(ident + $@"// </ ItemGroup >");
            }

            sourceBuilder.AppendLine($@" ");

            // finish creating the source to inject
            sourceBuilder.Append(@"
        }
    }
}");

            // inject the created source into the users compilation
            context.AddSource("TestSourcesGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));

            // Real code
            if (testSourcesDirectory is not null)
            {
                if (Directory.Exists(testSourcesDirectory.FullName))
                {
                    ProcessTestSourcesDirectory(testSourcesDirectory.FullName, 0, context);
                }
            }


                //if (Directory.Exists(TestSourcesPath))
                //{
                //    ProcessTestSourcesDirectory(TestSourcesPath, 0, context);
                //}

            }

            private DirectoryInfo GetDirectoryInfoFromFolderName(string testSourcesFolder, string firstFileInTestSources)
        {
            FileInfo fi = new FileInfo(firstFileInTestSources);
            DirectoryInfo di = fi.Directory;

            while (di is not null && di.Name != testSourcesFolder)
            {
                di = di.Parent;
            } 

            return di;
        }

        private void ProcessTestSourcesDirectory(string testSourcesPath, int level, GeneratorExecutionContext context)
        {
            // Process the list of files found in the directory.
            List<FileToGenerate> files = new List<FileToGenerate>();
            string[] fileEntries = Directory.GetFiles(testSourcesPath);
            FileClassGenerator fileClassGenerator = new FileClassGenerator();

            foreach (string fileName in fileEntries)
            {
                // Create the class corresponding to the file
                string className = SetClassName(fileName);
                string fileClassCode = fileClassGenerator.Generate(className, fileName);
                context.AddSource(className, SourceText.From(fileClassCode, Encoding.UTF8));
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(testSourcesPath);
            foreach (string subdirectory in subdirectoryEntries)
            {
               // ProcessTestSourcesDirectory(Path.Combine(testSourcesPath, Path.GetFileName(subdirectory)), level + 1, context);
            }

            // Create the class corresponding to the directory - at this point all the files and
            // directories will have been created.

        }

        private string SetClassName(string fileNameWithPath)
        {
            string fileName = Path.GetFileName(fileNameWithPath);
            var className = fileName;
            if (Path.HasExtension(fileNameWithPath))
            {
                className = fileName.Replace('.', '_');
            }

            // see if it already exists and
            if (_classNames.ContainsKey(className))
            {
                int index = _classNames[className] + 1;
                _classNames[className] = index;
                className += "_" + index.ToString();
            }
            else
            {
                _classNames.Add(className, 0);
            }

            return className;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Enabling debugging "hack" for source generator
            // how-to here: https://nicksnettravels.builttoroam.com/debug-code-gen/
////#if DEBUG
////            if (!Debugger.IsAttached)
////            {
////                Debugger.Launch();
////            }
////#endif
            Debug.WriteLine("Initalize code generator");
        }
    }
}
