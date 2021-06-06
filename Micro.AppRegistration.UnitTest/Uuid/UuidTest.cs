using Micro.AppRegistration.Common;
using NUnit.Framework;

namespace Micro.AppRegistration.UnitTest.Uuid
{
    public class UuidTest
    {
        [Test]
        public void GenerateUuidAddsPrefix()
        {
            var service = new UuidService();
            Assert.True(service.GenerateUuId("prefix").StartsWith("prefix_"));
            Assert.True(service.GenerateUuId("session").StartsWith("session_"));
            Assert.True(service.GenerateUuId("test").StartsWith("test_"));
            Assert.True(service.GenerateUuId("new_prefix").StartsWith("new_prefix_"));
        }
    }
}
