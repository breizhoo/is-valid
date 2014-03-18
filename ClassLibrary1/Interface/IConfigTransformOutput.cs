namespace Domain.Interface
{
    public interface IConfigTransformOutput
    {
        void TransformedConfigFile(string sourceFile, string transformFile, string destFile);
    }
}