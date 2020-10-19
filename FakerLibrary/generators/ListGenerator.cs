using System;
using System.Collections.Generic;

namespace FakerLibrary.generators
{
    public class ListGenerator: ICollectionGenerator<List<object>>
    {
        public List<object> Generate(Type baseType)
        {
            return new List<object>();
        }
    }
}