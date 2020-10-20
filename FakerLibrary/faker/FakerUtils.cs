using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FakerLibrary.generators;

namespace FakerLibrary.faker
{
    public static class FakerUtils
    {
        internal static Dictionary<Type, IGenerator> LoadAllAvailableGenerators()
        {
            var result = new Dictionary<Type, IGenerator>();
            var pluginsPath = Directory.GetCurrentDirectory() + "\\Plugins\\";
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath);
            }

            foreach (var str in Directory.GetFiles(pluginsPath, "*.dll"))
            {
                var asm = Assembly.LoadFrom(str);
                foreach (var t in asm.GetTypes())
                {
                    if (!(IsRequiredType(t, typeof(IPrimitiveGenerator<>)) |
                          IsRequiredType(t, typeof(ICollectionGenerator<>)))) continue;
                    var tmp = Activator.CreateInstance(t);
                    if (t.BaseType is null) continue;
                    result.Add(t.GetInterfaces()[0].GenericTypeArguments[0], (IGenerator) tmp);
                }
            }

            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!(IsRequiredType(t, typeof(IPrimitiveGenerator<>)) |
                      IsRequiredType(t, typeof(ICollectionGenerator<>)))) continue;
                if (t.BaseType is { })
                    result.Add(t.GetInterfaces()[0].GenericTypeArguments[0], (IGenerator) Activator.CreateInstance(t));
            }

            return result;
        }

        internal static bool IsPrimitive(Type t)
        {
            return t.IsPrimitive || (t == typeof(string)) || (t == typeof(decimal)) || (t == typeof(DateTime));
        }

        private static bool IsRequiredType(Type plugin, MemberInfo required)
        {
            if (plugin == null || plugin == typeof(object)) return false;
            var baseInterface = plugin.GetInterface(required.Name);
            return baseInterface != null;
        }
        
        
    }
}