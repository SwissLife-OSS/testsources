using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using TestSources.Helpers;
using TestSources.Model;

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
            //var projectDirectory = ProjectHelpers.GetProjectPath();
            //string TestSourcesPath = Path.Combine(projectDirectory, _TestSourcesFolder);


            // begin creating the source we'll inject into the users compilation
            var sourceBuilder = new StringBuilder(@"

//  v2

using System;

namespace TestSourcesGenerated
{
    public static class TestSources
    {
        public static void ExploreTestSources() 
        {
            Console.WriteLine(""Exploration of Test Sources!"");
");

            sourceBuilder.AppendLine($@"// AssemblyVersion is: {_assemblyVersion} ");
            //sourceBuilder.AppendLine($@"// projectDirectory is: {TestSourcesPath} ");

            sourceBuilder.AppendLine($@"Console.WriteLine(@""Exploring context.AdditionalFiles"");");
            foreach (AdditionalText contextAdditionalFile in context.AdditionalFiles)
            {
                sourceBuilder.AppendLine($@"Console.WriteLine(@"" - {contextAdditionalFile.Path}"");");

            }

            // using the context, get a list of syntax trees in the users compilation
            IEnumerable<SyntaxTree> syntaxTrees = context.Compilation.SyntaxTrees;

            sourceBuilder.AppendLine($@"Console.WriteLine(@""Exploring context.FilePath"");");
            // add the filepath of each tree to the class we're building
            foreach (SyntaxTree tree in syntaxTrees)
            {
                sourceBuilder.AppendLine($@"Console.WriteLine(@"" - {tree.FilePath}"");");
            }

            // finish creating the source to inject
            sourceBuilder.Append(@"
        }
    }
}");

            // inject the created source into the users compilation
            context.AddSource("TestSourcesGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));


            // Real code

            //if (Directory.Exists(TestSourcesPath))
            //{
            //    ProcessTestSourcesDirectory(TestSourcesPath, 0, context);
            //}

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
                ProcessTestSourcesDirectory(Path.Combine(testSourcesPath, Path.GetFileName(subdirectory)), level + 1, context);
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
            // No initialization required for this one
        }
    }
}
