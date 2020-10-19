using System;

namespace FakerLibrary.faker
{
    public class FakerGeneratorRule
    {
        public string FieldName { get; }
        public Type ParentClassType { get; }
        public Type TargetFieldType { get; }
        public Type FieldGeneratorType { get; }

        public FakerGeneratorRule(string fieldName, Type parentClassType, Type targetFieldType, Type fieldGeneratorType)
        {
            FieldName = fieldName;
            ParentClassType = parentClassType;
            TargetFieldType = targetFieldType;
            FieldGeneratorType = fieldGeneratorType;
        }
    }
}