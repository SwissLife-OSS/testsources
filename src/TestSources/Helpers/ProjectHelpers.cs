using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestSources.Helpers;

public static class ProjectHelpers
{
    public static string GetProjectPath()
    {
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        return projectDirectory;
    }

    public static string GeProjectFilePath(string projectDirectory)
    {
        var projectFile = Path.ChangeExtension(AppDomain.CurrentDomain.FriendlyName, "csproj");
        var projectFilePath = Path.Combine(projectDirectory, projectFile);

        if (File.Exists(projectFilePath))
        {
            return projectFilePath;
        }

        return Directory.GetFiles(projectDirectory, "*.csproj").FirstOrDefault();
    }
}
