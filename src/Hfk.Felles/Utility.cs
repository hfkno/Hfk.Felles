using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extension methods.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        ///     Retrieves the executing location of the current assembly.
        /// </summary>
        public static string ExecutionLocation
        {
            get
            {
                var ass = typeof(Utility).GetTypeInfo().Assembly;
                return Path.GetDirectoryName(ass.Location);
            }
        }

        /// <summary>
        ///     Retrieves the location of the current assembly's code.
        ///     Typically used in unit testing to find a relative path for test files.
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var ass = typeof(Utility).GetTypeInfo().Assembly;
                var codeBase = ass.CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);

                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        ///     Checks to see if there is debugging underway.
        /// </summary>
        public static bool DebuggerIsAttached
        {
            get { return Debugger.IsAttached; }
        }
    }
}