namespace Domain.Interface
{
    public interface IConfigTransform
    {
        bool Transform(string sourceFile, string transformFile, string destFile);
    }
}