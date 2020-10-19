using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FakerLibrary.generators;

namespace FakerLibrary.faker
{
    public class FakerUtils
    {
        internal static Dictionary<Type, IGenerator> LoadAllAvailableGenerators()
        {
            var result = new Dictionary<Type, IGenerator>();
            var pluginsPath = Directory.GetCurrentDirectory() + "\\FakerLib Plugins\\Generators\\";
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath);
            }

            foreach (var str in Directory.GetFiles(pluginsPath, "*.dll"))
            {
                var asm = Assembly.LoadFrom(str);
                foreach (var t in asm.GetTypes())
                {
                    if (!IsRequiredType(t, typeof(IGenerator))) continue;
                    var tmp = Activator.CreateInstance(t);
                    if (t.BaseType is { }) result.Add(t.BaseType.GetGenericArguments()[0], (IGenerator) tmp);
                }
            }

            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!IsRequiredType(t, typeof(IGenerator))) continue;
                if (t.BaseType is { })
                    result.Add(t.BaseType.GetGenericArguments()[0], (IGenerator) Activator.CreateInstance(t));
            }

            return result;
        }

        internal static bool IsPrimitive(Type t)
        {
            return t.IsPrimitive || (t == typeof(string)) || (t == typeof(decimal)) || (t == typeof(DateTime));
        }

        internal static bool IsRequiredType(Type plugin, Type required)
        {
            while (plugin != null && plugin != typeof(object))
            {
                var tmp = plugin.IsGenericType ? plugin.GetGenericTypeDefinition() : plugin;
                if (required == tmp)
                {
                    return true;
                }

                plugin = plugin.BaseType;
            }

            return false;
        }
        private FakerUtils(){}

    }
}