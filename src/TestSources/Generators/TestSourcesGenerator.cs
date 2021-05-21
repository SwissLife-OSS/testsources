using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace TestSources.Generators
{
    [Generator]
    public class TestSourcesGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
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

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
        }
    }
}
