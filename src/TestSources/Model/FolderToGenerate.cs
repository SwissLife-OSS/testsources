using System;
using System.Collections.Generic;
using System.Text;

namespace TestSources.Model;

internal class FolderToGenerate
{
    public FolderToGenerate(string path, string parentClassName)
    {
        Path = path;
        ParentClassName = parentClassName;
    }

    public string Path { get; }
    public string ParentClassName { get; set; }
}
