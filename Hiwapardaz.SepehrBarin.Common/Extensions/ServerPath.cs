﻿using Hiwapardaz.SepehrBarin.Common.Exceptions;
using System.Reflection;

namespace Hiwapardaz.SepehrBarin.Common.Extensions
{
    public static class ServerPath
    {
        public static string GetProjectPath(Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;
            var applicationBasePath = AppContext.BaseDirectory;
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                directoryInfo = directoryInfo.Parent;
                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName));
                if (projectDirectoryInfo.Exists
                    && new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                {
                    return Path.Combine(projectDirectoryInfo.FullName, projectName);
                }
            }
            while (directoryInfo.Parent != null);
            throw new RootProjectNotFoundException(applicationBasePath);
        }
    }
}
