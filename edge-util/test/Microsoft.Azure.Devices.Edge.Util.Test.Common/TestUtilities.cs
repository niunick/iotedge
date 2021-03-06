// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.Azure.Devices.Edge.Util.Test.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Xunit;

    public static class TestUtilities
    {
        public static void ApproxEqual(double expected, double actual, double tolerance)
        {
            Assert.True(Math.Abs(expected - actual) <= tolerance, $"Expected {expected} to be within {tolerance} of {actual}");
        }

        public static void OrderlessCompare<T>(IEnumerable<T> expected, IEnumerable<T> actual)
            where T : IEquatable<T>
        {
            LinkedList<T> expectedList = new LinkedList<T>(expected);

            foreach (T actualItem in actual)
            {
                LinkedListNode<T> expectedNode = expectedList.First;
                while (expectedNode != null)
                {
                    if (actualItem.Equals(expectedNode.Value))
                    {
                        break;
                    }

                    expectedNode = expectedNode.Next;
                }

                Assert.False(expectedNode == null, $"Didn't expect actual to contain {JsonConvert.SerializeObject(actualItem)}");
                expectedList.Remove(expectedNode);
            }

            Assert.False(expectedList.Any(), $"Expected actual to contain {JsonConvert.SerializeObject(expectedList)}");
        }
    }
}
