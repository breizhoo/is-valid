namespace Domain.Interface
{
    public interface IConnectionStringItemForValidatorSimple
    {
        string Project { get; }

        string File { get; }

        string Provider { get; }

        string Name { get; }

        string ConnectionString { get; }
    }
}