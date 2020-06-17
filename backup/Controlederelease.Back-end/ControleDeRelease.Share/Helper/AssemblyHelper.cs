using System;
using System.IO;
using System.Reflection;

namespace ControleDeRelease.Share.Helper
{
    public class AssemblyHelper
    {
        private Assembly _assembly;

        public static AssemblyHelper Current = new AssemblyHelper(Assembly.GetExecutingAssembly());

        public string Title
        {
            get
            {
                AssemblyTitleAttribute attribute = (AssemblyTitleAttribute)this._assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];

                return attribute.Title;
            }
        }

        public Version Version => _assembly.GetName().Version;

        public string CodeBase => _assembly.CodeBase;

        public DateTime CreationTime => File.GetCreationTime(new Uri(CodeBase).LocalPath);

        public AssemblyHelper(Assembly assembly) => _assembly = assembly;
    }
}
