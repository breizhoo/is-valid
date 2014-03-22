using System;
using System.IO;
using Domain.Interface;
using Microsoft.Web.XmlTransform;
using Ninject.Extensions.Logging;

namespace Domain.Implementation
{
    /// <summary>
    /// Transform a config file.
    /// </summary>
    internal class ConfigTransform : IConfigTransform
    {
        private readonly ILogger _logger;

        public ConfigTransform(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Launch transform.
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="transformFile"></param>
        /// <param name="destFile"></param>
        /// <returns></returns>
        public bool Transform(string sourceFile, string transformFile, string destFile)
        {
            if (!File.Exists(sourceFile))
            {
                _logger.Error("The sourceFile doesn't exist");
                return false;
            }
            if (!File.Exists(transformFile))
            {
                _logger.Error("The transformFile doesn't exist");
                return false;
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
                _logger.Error(ex, "The transformFile faild.");
                return false;
            }
        }

    }
}
