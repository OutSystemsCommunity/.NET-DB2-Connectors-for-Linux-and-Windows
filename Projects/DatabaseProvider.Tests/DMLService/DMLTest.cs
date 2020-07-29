/* 
 This source code (the "Generated Software") is generated by the OutSystems Platform 
 and is licensed by OutSystems (http://www.outsystems.com) to You solely for testing and evaluation 
 purposes, unless You and OutSystems have executed a specific agreement covering the use terms and 
 conditions of the Generated Software, in which case such agreement shall apply. 
*/

using System;
using NUnit.Framework;
using OutSystems.ServerTests.DatabaseProvider.Framework;

namespace OutSystems.ServerTests.DatabaseProvider.DMLService {
    public class DMLTestsConfiguration : AgnosticDatabaseProviderTestConfiguration {

        protected override string ConfigurationPathSettingName {
            get {
                return "DatabaseProviderTests.DMLServiceFilesPath";
            }
        }
    }

    public class ServerOnlyDMLTestsConfiguration : DMLTestsConfiguration {

        protected override bool IsServerOnly {
            get {
                return true;
            }
        }
    }

    public class DMLTest : DatabaseProviderTest<DMLTestsConfiguration> {

        /// <summary>
        /// Asserts that two values are equal using <see cref="Assert.AreEqual(object, object, string)"/> with a fallback
        /// for decimal (and BigDecimal) values that uses <see cref="decimal.CompareTo(object)"/> to ignore precision differences
        /// in Java.
        /// </summary>
        protected static void AssertEqual(object expected, object result, string errorMessage) {
            try {
                Assert.AreEqual(expected, result, errorMessage);
            }  catch {
                // This is a fallback that rethrows the original exception in case of failture, in order to reuse the error messages
                if (expected is decimal || result is decimal) {
                    IComparable comparableExpected = (IComparable) expected;
                    IComparable comparableResult = (IComparable) result;
                    if (comparableExpected.CompareTo(comparableResult) != 0) {
                        throw;
                    }
                } else {
                    throw;
                }
            }
        }
    }
}
