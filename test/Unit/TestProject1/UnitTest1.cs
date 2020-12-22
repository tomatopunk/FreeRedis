using System;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            _testOutputHelper.WriteLine(Environment.GetEnvironmentVariable("test") ?? "test null");
            Assert.NotNull(Environment.GetEnvironmentVariable("test"));
        }
    }
}