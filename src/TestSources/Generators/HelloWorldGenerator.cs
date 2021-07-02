// Disabled HelloWorld Generator by commenting -
// This file is here just to check that Source Generation works fine
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.Text;

//namespace TestSources.Generators
//{
//    [Generator]
//    public class HelloWorldGenerator : ISourceGenerator
//    {
//        public void Execute(GeneratorExecutionContext context)
//        {
//            // begin creating the source we'll inject into the users compilation
//            var sourceBuilder = new StringBuilder(@"
//using System;
//namespace HelloWorldGenerated
//{
//    public static class HelloWorld
//    {
//        public static void SayHello() 
//        {
//            Console.WriteLine(""Hello from generated code!"");
//            Console.WriteLine(""Version 2!"");
//            Console.WriteLine(""The following syntax trees existed in the compilation that created this program:"");
//");

//            // using the context, get a list of syntax trees in the users compilation
//            IEnumerable<SyntaxTree> syntaxTrees = context.Compilation.SyntaxTrees;

//            // add the filepath of each tree to the class we're building
//            foreach (SyntaxTree tree in syntaxTrees)
//            {
//                sourceBuilder.AppendLine($@"Console.WriteLine(@"" - {tree.FilePath}"");");
//            }


//            // simple test
//            //sourceBuilder.AppendLine($@"// blabla... ");

//            // finish creating the source to inject
//            sourceBuilder.Append(@"
//        }
//    }
//}");

//            // inject the created source into the users compilation
//            context.AddSource("helloWorldGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
//        }

//        public void Initialize(GeneratorInitializationContext context)
//        {
//            // No initialization required for this one
//        }
//    }
//}
