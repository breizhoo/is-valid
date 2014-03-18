using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.Web.XmlTransform;

namespace Domain.Implementation
{
    internal class ConfigTransform : IConfigTransform
    {
        public Task<bool> TransformAsync(string sourceFile, string transformFile, string destFile)
        {
            return Task.Run(() => Transform(sourceFile, transformFile, destFile));
        }

        public bool Transform(string sourceFile, string transformFile, string destFile)
        {
            if (!File.Exists(sourceFile))
            {
                return false;
                //throw new FileNotFoundException("sourceFile doesn't exist");
            }
            if (!File.Exists(transformFile))
            {
                return false;
                //throw new FileNotFoundException("transformFile doesn't exist");
            }

            try
            {
                using (var document = new XmlTransformableDocument())
                {
                    document.PreserveWhitespace = true;
                    document.Load(sourceFile);

                    using (var transform = new XmlTransformation(transformFile))
                    {
                        if (transform.Apply(document))
                        {
                            Directory.CreateDirectory(destFile);
                            var fileDest = Path.Combine(destFile, new FileInfo(sourceFile).Name);
                            document.Save(fileDest);
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                //todo : put log here.
                return false;
            }
        }

    }
}
