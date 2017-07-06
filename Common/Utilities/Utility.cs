using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace Gdot.Care.Common.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class Utility
    {
        public static string GetIpAddress
        {
            get
            {
                var ip = string.Empty;

                foreach (
                    var ipa in
                        Dns.GetHostAddresses(Dns.GetHostName())
                            .Where(ipa => ipa.AddressFamily.ToString() == "InterNetwork"))
                {
                    ip = ipa.ToString();
                    break;
                }
                return ip;
            }
        }

        public static object CreateObject(string assemblyPath, string className)
        {
            if (!File.Exists(assemblyPath))
            {
                throw new FileNotFoundException($"AssemblyPath={assemblyPath} not found", assemblyPath);
            }
            var assembly = Assembly.LoadFrom(assemblyPath);
            assembly.GetExportedTypes();
            var instance = assembly.CreateInstance(className);
            return instance;
        }
        public static Type[] GetObject(string assemblyPath, string className)
        {
            if (!File.Exists(assemblyPath))
            {
                throw new FileNotFoundException($"AssemblyPath={assemblyPath} not found", assemblyPath);
            }
            var assembly = Assembly.LoadFrom(assemblyPath);
            return assembly.GetExportedTypes();
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            if (type.GetProperties(BindingFlags.Public | BindingFlags.Instance).All(p => p.Name != name))
            {
                return null;
            }
            return type.GetProperty(name);
        }
        public static string GetFullpath(string fileName)
        {
            var filePath = string.Empty;
            if (HttpContext.Current != null)
            {
                var serverPath = HttpContext.Current.Server.MapPath("~/");
                if (!string.IsNullOrEmpty(serverPath))
                {
                    filePath = Path.Combine(serverPath, fileName);
                }
            }
            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            }
            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            }
            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, fileName);
            }
            if (!File.Exists(filePath) && HttpContext.Current != null && !string.IsNullOrEmpty(HttpRuntime.BinDirectory))
            {
                filePath = Path.Combine(HttpRuntime.BinDirectory, fileName);
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"FilePath={filePath} is not found.");
            }
            return filePath;
        }

    }
}
