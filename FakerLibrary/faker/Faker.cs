using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakerLibrary.generators;

namespace FakerLibrary.faker
{
    public class Faker : IFaker
    {
        private readonly List<IGenerator> _generators;

        private int MaxCircularDependency { get; } = 0;
        private int _currentCircularDependency = 0;
        private readonly Stack<Type> _constructionStack = new Stack<Type>();
        private readonly Random _random;


        public Faker()
        {
            _random = new Random();
           _generators = FakerUtils.LoadGenerators();
        }

        public T Create<T>()
        {
            return (T) Create(typeof(T));
        }

        internal object Create(Type type)
        {
            if (((_currentCircularDependency = _constructionStack.Count(t => t == type)) > MaxCircularDependency))
            {
                return GetDefaultValue(type);
            }

            _constructionStack.Push(type);

            var currentGenerator = _generators.FirstOrDefault(g => g.CanGenerate(type));

            if (currentGenerator != null)
            {
                _constructionStack.Pop();
                return currentGenerator.Generate(new FakerContext(this, _random, type));
            }

            var createdObject = CreateObject(type);
            _constructionStack.Pop();

            return createdObject ?? GetDefaultValue(type);
        }

        private static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        private object CreateObject(Type type)
        {
            var currentConstructors = type.GetConstructors();
            object createdObject = default;

            if (currentConstructors.Length == 0 && type.IsClass)
                return default;

            ParameterInfo[] ctorParamInfos = null;
            var isCreated = true;
            foreach (var cInfo in currentConstructors.OrderByDescending(c => c.GetParameters().Length))
            {
                var parametersInfo = cInfo.GetParameters();
                var parameters = new object[parametersInfo.Length];
                for (var i = 0; i < parameters.Length; i++)
                    parameters[i] = Create(parametersInfo[i].ParameterType);

                try
                {
                    createdObject = cInfo.Invoke(parameters);
                    ctorParamInfos = parametersInfo;
                }
                catch
                {
                    isCreated = false;
                    continue;
                }

                if (isCreated)
                    break;
            }

            switch (createdObject)
            {
                case null when type.IsValueType:
                    try
                    {
                        return Activator.CreateInstance(type);
                    }
                    catch
                    {
                        return null;
                    }
                case null:
                    return null;
                default:
                    GenerateFieldsAndProperties(createdObject, ctorParamInfos);
                    return createdObject;
            }
        }

        private void GenerateFieldsAndProperties(object createdObject, IReadOnlyCollection<ParameterInfo> ctorParams)
        {
            var fields = createdObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Cast<MemberInfo>();
            var properties = createdObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Cast<MemberInfo>();
            var fieldsAndProperties = fields.Concat(properties);

            foreach (var m in fieldsAndProperties)
            {
                var wasInitialized = false;

                var memberType = (m as FieldInfo)?.FieldType ?? (m as PropertyInfo)?.PropertyType;
                var memberValue = (m as FieldInfo)?.GetValue(createdObject) ??
                                  (m as PropertyInfo)?.GetValue(createdObject);

                for (var i = 0; i < ctorParams?.Count; i++)
                {
                    var defaultValue = GetDefaultValue(memberType);
                    if (defaultValue?.Equals(memberValue) != false) continue;
                    wasInitialized = true;
                    break;
                }

                if (wasInitialized) continue;
                (m as FieldInfo)?.SetValue(createdObject, Create(((FieldInfo) m).FieldType));
                if ((m as PropertyInfo)?.CanWrite == true)
                    ((PropertyInfo) m).SetValue(createdObject, Create(((PropertyInfo) m).PropertyType));
            }
        }
    }
}