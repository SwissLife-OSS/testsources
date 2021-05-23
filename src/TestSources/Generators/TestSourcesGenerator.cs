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

            var projectDirectory = ProjectHelpers.GetProjectPath();
            string TestSourcesPath = Path.Combine(projectDirectory, _TestSourcesFolder);

            if (Directory.Exists(TestSourcesPath))
            {
                ProcessTestSourcesDirectory(TestSourcesPath);
            }

            // begin creating the source we'll inject into the users compilation
            var sourceBuilder = new StringBuilder(@"
using System;

namespace TestSourcesGenerated
{
    public static class TestSources
    {
        public static void ExploreTestSources() 
        {
            Console.WriteLine(""Exploration of Test Sources!"");
");

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
        }

        private void ProcessTestSourcesDirectory(string testSourcesPath)
        {
            // Process the list of files found in the directory.
            List<FileToGenerate> files = new List<FileToGenerate>();
            string[] fileEntries = Directory.GetFiles(testSourcesPath);
            foreach (string fileName in fileEntries)
            {
                // Create the class corresponding to the file
                files.Add(new FileToGenerate(fileName, null));
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(testSourcesPath);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessTestSourcesDirectory(Path.Combine(testSourcesPath, Path.GetFileName(subdirectory)));
            }

            // Create the class corresponding to the directory - at this point all the files and
            // directories will have been created.

        }

            public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
        }
    }
}
