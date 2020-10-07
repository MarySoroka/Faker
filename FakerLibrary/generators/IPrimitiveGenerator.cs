namespace FakerLibrary.generators
{
    public interface IPrimitiveGenerator<out T>: IGenerator<T>
    {
        T Generate();
    }
}