using System;
using System.Collections.Generic;

namespace FakerLibrary.generators
{
    public class ListGenerator<T>: ICollectionGenerator<List<T>>
    {
        public List<T> Generate(Type baseType)
        {
            return new List<T>();
        }
    }
}