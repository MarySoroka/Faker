namespace FakerLibrary.faker
{
    public interface IFaker
    {
        T Create<T>();
    }
}