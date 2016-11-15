using System;
using NUnit.Framework;

namespace EntityFramework.Test
{
    [TestFixture]
    public class NUnitTestInstall
    {
        /// <summary>
        /// This function/test only exists to ensure that 
        /// tests are installed/will show up
        /// </summary>
        [Test]
        public void TestIfInstalled()
        {
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
            Assert.AreEqual(true, true);
        }
    }
}