using System.ComponentModel;
using System.Reflection;

namespace ControleDeRelease.Domain.Extensions
{
    public static class AttributeExtensions
    {
        public static string GetDescriptionAttribute<T>(this T source)
        {
            var fieldInfo = source.GetType().GetField(source.ToString());
            var attributes = (AssemblyDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0 )
            {
                return attributes[0].Description;
            }

            return source.ToString();
        }

    }
}
