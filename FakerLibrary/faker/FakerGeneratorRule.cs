using System;

namespace FakerLibrary.faker
{
    public class FakerGeneratorRule
    {
        private string _fieldName;
        private Type _parentClassType;
        private Type _targetFieldType;
        private Type _fieldGeneratorType;

        public FakerGeneratorRule(string fieldName, Type parentClassType, Type targetFieldType, Type fieldGeneratorType)
        {
            _fieldName = fieldName;
            _parentClassType = parentClassType;
            _targetFieldType = targetFieldType;
            _fieldGeneratorType = fieldGeneratorType;
        }
    }
}