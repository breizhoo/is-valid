namespace Domain.Interface
{
    /// <summary>
    /// Transform a config file.
    /// </summary>
    public interface IConfigTransform
    {
        /// <summary>
        /// Launch transform.
        /// </summary>
        /// <param name="sourceFile">source file</param>
        /// <param name="transformFile">the file of transformation.</param>
        /// <param name="destFile">when the file will be tranformed.</param>
        /// <returns></returns>
        bool Transform(string sourceFile, string transformFile, string destFile);
    }
}