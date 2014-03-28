namespace Domain.Interface
{
    public interface IConnectionStringItemValidator
    {
        /// <summary>
        /// Regexp
        /// </summary>
        string Regex { get; set; }

        /// <summary>
        /// true for match else not match.
        /// </summary>
        bool Match { get; set; }

        /// <summary>
        /// True if is a criteria 
        /// </summary>
        bool Criteria { get; set; }

        /// <summary>
        /// true if active else false.
        /// </summary>
        bool Active { get; set; }
    }
}