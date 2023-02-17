using System;
using System.Collections.Generic;
using System.Text;

namespace TestSources.Model;

internal class FileToGenerate
{
    public FileToGenerate(string fileName, string parentClassName)
    {
        FileName = fileName;
        ParentClassName = parentClassName;
    }

    public string FileName { get; }
    public string ParentClassName { get; set; }
}
