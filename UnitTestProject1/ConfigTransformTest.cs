using Domain.Implementation;
using Domain.Interface.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class ConfigTransformTest
    {
        [TestMethod]
        [DeploymentItem("Web.config")]
        [DeploymentItem("Web.Result.config")]
        [DeploymentItem("Web.Release.config")]
        public void ConfigTransform_Transform()
        {
            var configTransformOutput = new StubIConfigTransformOutput();
            var configTransform = new ConfigTransform(configTransformOutput);
            var task = configTransform.TransformAsync("Web.config", "Web.Release.config", "Web.ResultDebug.config");

            var result = task.Result;
            Assert.IsTrue(result, "The transformation faild.");
            var areEqual = FileEquals("Web.ResultDebug.config", "Web.Result.config");
            Assert.IsTrue(areEqual, "The transformation faild. File aren't equal");
        }

        static bool FileEquals(string path1, string path2)
        {
            byte[] file1 = File.ReadAllBytes(path1);
            byte[] file2 = File.ReadAllBytes(path2);
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    if (file1[i] != file2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
