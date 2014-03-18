namespace Domain.Interface
{
    public interface IConnectionStringItemValidator
    {
        string Regex { get; }

        bool? Match { get; }
    }
}