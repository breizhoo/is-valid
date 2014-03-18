namespace Domain.Interface
{
    public interface IConfigTransformOutput
    {
        bool TransformedConfigFile(string sourceFile, string transformFile, string destFile);
    }
}