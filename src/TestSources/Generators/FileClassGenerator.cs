using System;
using System.Collections.Generic;
using System.Text;
using TestSources.Helpers;

namespace TestSources.Generators
{
    internal class FileClassGenerator : ClassBuilder
    {
        private string _className;
        private string _fileName;
        
        public string Generate(string className, string fileName)
        {
            // initialization
            this.Clear();
            _className = className;
            _fileName = fileName;

            // FileClassGeneration
            GenerateHeader();
            GenerateNamespace();
            GenerateClass();
            GenerateConstructor();

            while (IndentLevel > 0) 
            {
                DecreaseIndent();
                AppendLine("}");
            }

            return ToString();
        }

        private void GenerateHeader()
        {
            GenerateHeaderComments();
            GenerateUsings();
        }

        private void GenerateUsings()
        {
            AppendLine("using System;");
            AppendLine("using System.IO;");
            AppendLine("using TestSources.Interfaces;");
        }

        private void GenerateHeaderComments()
        {
            AppendLine($"// class {_className} representing the file located at:");
            AppendLine($"// {_fileName}  ");
            AppendLine($"// generated at {DateTime.UtcNow} ");
        }

        private void GenerateNamespace()
        {
            AppendLine();
            AppendLine($"namespace {NameSpace}");
            AppendLine("{");
            IncreaseIndent();
        }

        private void GenerateClass()
        {
            AppendLine($"partial class {_className} " +
                       " : TestSourceFile");
            AppendLine("{");
            IncreaseIndent();
        }

        private void GenerateConstructor()
        {
            AppendLineBeforeMember();
            Append($"public {_className}() : ");
            Append(@"base(""{_fileName}"", null)");
            AppendLine("");
            AppendLine("{");
            AppendLine("}");
            AppendLine();
        }


    }
}
