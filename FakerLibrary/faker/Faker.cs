using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakerLibrary.generators;
using Microsoft.VisualBasic;

namespace FakerLibrary.faker
{
    public class Faker : IFaker
    {
        private int MaxCircularDependencyDepth { get; } = 0;

        private readonly Dictionary<Type, IGenerator> _generators;

        private readonly Stack<Type> _fakerStack = new Stack<Type>();

        private readonly List<FakerGeneratorRule> _fakerRules;


        public T Create<T>()
        {
            if (FakerUtils.IsPrimitive(typeof(T)))
            {
                try
                {
                    return (T) _generators[typeof(T)].GetType().InvokeMember("Generate",
                        BindingFlags.InvokeMethod | BindingFlags.Instance
                                                  | BindingFlags.Public, null, _generators[typeof(T)], null);
                }
                catch
                {
                    return default;
                }
            }

            var constructors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            if ((constructors.Length == 0 && !typeof(T).IsValueType) ||
                ((_fakerStack.Count(t => t == typeof(T))) >
                 MaxCircularDependencyDepth))
            {
                return default;
            }

            _fakerStack.Push(typeof(T));


            object constructed = default;
            if (typeof(T).IsValueType && constructors.Length == 0)
            {
                constructed = Activator.CreateInstance(typeof(T));
            }

            object[] ctorParams = null;
            ConstructorInfo ctor = null;

            foreach (var cInfo in constructors.OrderByDescending(c => c.GetParameters().Length)
            )
            {
                ctorParams = GenerateCtorParams(cInfo);

                try
                {
                    constructed = cInfo.Invoke(ctorParams);
                    ctor = cInfo;
                }
                catch
                {
                    // ignored
                }
            }

            GenerateFieldsAndProperties(constructed, ctorParams,
                ctor);

            _fakerStack.Pop();
            return (T) constructed;
        }

        private void GenerateFieldsAndProperties(object constructed, IReadOnlyList<object> ctorParams,
            ConstructorInfo cInfo)
        {
            var pInfo = cInfo?.GetParameters();
            var fields = constructed.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Cast<MemberInfo>();
            var properties = constructed.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Cast<MemberInfo>();
            var fieldsAndProperties = fields.Concat(properties);

            foreach (MemberInfo m in fieldsAndProperties)
            {
                var wasInitialized = false;

                var memberType = (m as FieldInfo)?.FieldType ?? (m as PropertyInfo)?.PropertyType;
                var memberValue = (m as FieldInfo)?.GetValue(constructed) ?? (m as PropertyInfo)?.GetValue(constructed);

                for (var i = 0; i < ctorParams?.Count; i++)
                {
                    var defaultValue = this.GetType()
                        .GetMethod("GetDefaultValue", BindingFlags.NonPublic | BindingFlags.Instance)
                        .MakeGenericMethod(memberType).Invoke(this, null);
                    if ((pInfo == null || ctorParams[i] != memberValue || memberType != pInfo[i].ParameterType ||
                         m.Name != pInfo[i].Name) && defaultValue?.Equals(memberValue) != false) continue;
                    wasInitialized = true;
                    break;
                }

                if (wasInitialized) continue;
                object newValue = default;
                try
                {
                    if (_fakerRules?.Any(r =>
                        (r.TargetFieldType == memberType) && (r.ParentClassType == constructed.GetType()) &&
                        (r.FieldName == m.Name)) == true)
                    {
                        var gen = Activator.CreateInstance(_fakerRules.Single(r => (r.TargetFieldType == memberType) &&
                            (r.ParentClassType == constructed.GetType())
                            && (r.FieldName == m.Name)).FieldGeneratorType);
                        newValue = gen.GetType().InvokeMember("Generate",
                            BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, gen, null);
                    }
                    else
                    {
                        if (!memberType.IsGenericType)
                        {
                            newValue = _generators[memberType].GetType().InvokeMember("Generate",
                                BindingFlags.InvokeMethod | BindingFlags.Instance
                                                          | BindingFlags.Public, null, _generators[memberType], null);
                        }
                        else
                        {
                            newValue = _generators[typeof(IList)].GetType().InvokeMember("Generate",
                                BindingFlags.InvokeMethod | BindingFlags.Instance
                                                          | BindingFlags.Public, null, _generators[typeof(IList)],
                                new object?[] {memberType, this});
                        }
                    }
                }
                catch (KeyNotFoundException e)
                {
                    if (!FakerUtils.IsPrimitive(memberType))
                    {
                        newValue = this.GetType().GetMethod("Create").MakeGenericMethod(memberType).Invoke(this, null);
                    }
                }

                (m as FieldInfo)?.SetValue(constructed, newValue);
                if ((m as PropertyInfo)?.CanWrite == true)
                {
                    (m as PropertyInfo).SetValue(constructed, newValue);
                }
            }
        }

        private object GetDefaultValue<T>()
        {
            return default(T);
        }

        private object[] GenerateCtorParams(ConstructorInfo cInfo)
        {
            var pInfo = cInfo.GetParameters();
            var ctorParams = new object[pInfo.Length];

            for (var i = 0; i < ctorParams.Length; i++)
            {
                var fieldType = pInfo[i].ParameterType;
                object newValue = default;
                try
                {
                    if (_fakerRules?.Any(r =>
                        (r.TargetFieldType == fieldType) && (r.ParentClassType == cInfo.DeclaringType) &&
                        (r.FieldName == pInfo[i].Name)) == true)
                    {
                        var gen = Activator.CreateInstance(_fakerRules.Single(r =>
                            (r.TargetFieldType == fieldType) && (r.ParentClassType == cInfo.DeclaringType)
                                                             && (r.FieldName == pInfo[i].Name)).FieldGeneratorType);
                        if (gen != null)
                            newValue = gen.GetType().InvokeMember("Generate", BindingFlags.InvokeMethod |
                                                                              BindingFlags.Instance |
                                                                              BindingFlags.Public,
                                null, gen, null);
                    }
                    else
                    {
                        if (!fieldType.IsGenericType)
                        {
                            newValue = _generators[fieldType].GetType().InvokeMember("Generate",
                                BindingFlags.InvokeMethod |
                                BindingFlags.Instance | BindingFlags.Public, null, _generators[fieldType], null);
                        }
                        else
                        {
                            var tmp = fieldType.GetGenericArguments();
                            newValue = _generators[typeof(IList)].GetType().InvokeMember("Generate",
                                BindingFlags.InvokeMethod |
                                BindingFlags.Instance | BindingFlags.Public, null, _generators[typeof(IList)],
                                new object[] {fieldType, this});
                        }
                    }
                }
                catch (KeyNotFoundException e)
                {
                    if (!FakerUtils.IsPrimitive(fieldType))
                    {
                        newValue = GetType().GetMethod("Create")?.MakeGenericMethod(fieldType)
                            .Invoke(this, new object[] {fieldType, this});
                    }
                }

                ctorParams[i] = newValue;
            }

            return ctorParams;
        }

        private Faker()
        {
            _generators = FakerUtils.LoadAllAvailableGenerators();
        }

        public Faker(FakerConfiguration config) : this()
        {
            _fakerRules = config.FakerGeneratorRules;
            _generators = FakerUtils.LoadAllAvailableGenerators();
        }
    }
}