using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FakerLibrary.generators;

namespace FakerLibrary.faker
{
    public static class FakerUtils
    {
        public static List<IGenerator> LoadGenerators()
        {
            var generators = new List<IGenerator>
            {
                new ListGenerator(),
                new BooleanGenerator(),
                new DateGenerator(),
                new FloatGenerator(),
                new LongGenerator(),
                new ShortGenerator(),
                new StringGenerator(),
                new IntegerGenerator()
            };
            var pluginsPath = Directory.GetCurrentDirectory() + "//Plugins//";
            generators.AddRange(from name in Directory.GetFiles(pluginsPath, "*.dll")
                select Assembly.LoadFrom(name)
                into asm
                from t in asm.GetTypes()
                where t.GetInterface(nameof(IGenerator)) != null
                select Activator.CreateInstance(t)
                into currentGenerator
                select (IGenerator) currentGenerator);
            return generators;
        }
    }
}