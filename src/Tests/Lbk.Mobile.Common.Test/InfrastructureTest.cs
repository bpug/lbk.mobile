//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InfrastructureTest.cs" company="ip-connect GmbH">
//    Copyright (c) ip-connect GmbH. All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Lbk.Mobile.Common.Test
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using Lbk.Mobile.Common.Extensions;

    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
    using MD5 = Lbk.Mobile.Common.Cryptography.MD5;

    [TestFixture]
    public class InfrastructureTest
    {
        [Test]
        public void GetMd5()
        {
            const string input = "This is an example";
            string hashString = MD5.GetHashString(input);

            var checksum = new MD5CryptoServiceProvider();
            var bytes = checksum.ComputeHash(Encoding.UTF8.GetBytes(input));
            string hashString2 = ToHex(bytes, true);

            Assert.AreEqual(hashString, hashString);
        }

        [Test]
        public void TestTruncate()
        {
            
            double[] values =
            {
                0.1, 0.897, 7.03, 7.64, 9.999, -0.1, -0.897, -7.03, -7.64, -9.999
            };
            foreach (double value in values)
            {
                Assert.AreEqual(Math.Truncate(value), value.Truncate());
            }
        }

        private static string ToHex(byte[] bytes, bool upperCase)
        {
            var result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            }
            return result.ToString();
        }
    }
}