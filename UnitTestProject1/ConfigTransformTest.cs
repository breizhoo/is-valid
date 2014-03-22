using System;
using System.Linq;
using Domain.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Ninject.Extensions.Logging.Fakes;
using NFluent;
using Domain.Interface;

namespace UnitTestProject1
{
    [TestClass]
    public class ConfigTransformTest
    {
        private IConfigTransform _configTransform;
        private string _destFile;

        [TestInitialize]
        public void TestInitializer()
        {
            var iLogger = new StubILogger();
            _configTransform = new ConfigTransform(iLogger);
            _destFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        }

        [TestMethod]
        [DeploymentItem("Web.config")]
        [DeploymentItem("Web.Result.config")]
        [DeploymentItem("Web.Release.config")]
        public void ConfigTransform_Transform_TransformeFile_Success()
        {
            Check.That(_configTransform.Transform(
                    "Web.config",
                    "Web.Release.config",
                    _destFile)
                ).IsTrue();

            Check.That(FileEquals(
                    Path.Combine(_destFile, "Web.config"),
                    "Web.Result.config")
                ).IsTrue();
        }

        [TestMethod]
        [DeploymentItem("Web.Result.config")]
        [DeploymentItem("Web.Release.config")]
        public void ConfigTransform_Transform_SourceFileNotFound_Error()
        {
            Check.That(_configTransform.Transform(
                    "Web.config",
                    "Web.Release.config",
                    null)
                ).IsFalse();
        }

        [TestMethod]
        [DeploymentItem("Web.config")]
        [DeploymentItem("Web.Result.config")]
        public void ConfigTransform_Transform_TransformFileNotFound_Error()
        {
            Check.That(_configTransform.Transform(
                    "Web.config",
                    "Web.Release.config",
                    null)
                ).IsFalse();
        }

        [TestMethod]
        [DeploymentItem("Web.config")]
        [DeploymentItem("Web.Result.config")]
        public void ConfigTransform_Transform_BadDestFile_Error()
        {
            Check.That(_configTransform.Transform(
                    "Web.config",
                    "Web.Release.config",
                    null)
                ).IsFalse();
        }

        static bool FileEquals(string path1, string path2)
        {
            byte[] file1 = File.ReadAllBytes(path1);
            byte[] file2 = File.ReadAllBytes(path2);
            if (file1.Length == file2.Length)
                return !file1.Where((t, i) => t != file2[i]).Any();
            return false;
        }
    }
}
