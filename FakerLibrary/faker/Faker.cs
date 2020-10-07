namespace FakerLibrary.faker
{
    public class Faker: IFaker
    {
        public T Create<T>()
        {
            return default;
        }
    }
}