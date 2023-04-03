// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {

            var keyValuePairs = new Dictionary<string, string> {
                    { "url1", "params1" },
                    { "https://www.baidu.com/", "xxxxx" },
                    { "https://www.zhihu.com/topics", "xxxxxxxxx" }, };

            FY.Common.Helper.AsyncTaskHelper.StartTask(keyValuePairs);
        }
    }
}
