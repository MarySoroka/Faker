using System;

namespace FakerLibrary.generators
{
    public interface ICollectionGenerator<out T>: IGenerator<T>
    {
        T Generate(Type baseType);
    }
}