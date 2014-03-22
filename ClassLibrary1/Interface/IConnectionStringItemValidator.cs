namespace Domain.Interface
{
    public interface IConnectionStringItemValidator
    {
        /// <summary>
        /// Regexp
        /// </summary>
        string Regex { get; }

        /// <summary>
        /// true for match else not match.
        /// </summary>
        bool Match { get; }

        /// <summary>
        /// True if is a criteria 
        /// </summary>
        bool Criteria { get; }

        /// <summary>
        /// true if active else false.
        /// </summary>
        bool Active { get; }
    }
}