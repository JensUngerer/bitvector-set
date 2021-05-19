using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace JensUngerer.BitVectorSet
{
    public class SetTests
    {
        private static SampleBitVectorFactory SampleBitVectorFactory { get; set; }  = new SampleBitVectorFactory();

        private static BitVectorSet<string> Create()
        {
            const int size = 1000;
            var bitVectorSet = SampleBitVectorFactory.Create(size, "a");
            return bitVectorSet;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BasicSetTest()
        {
            // ARRANGE
            var bitVectorSet = Create();

            // ASSERT
            Assert.AreEqual(0, bitVectorSet.Count);
        }

        [Test]
        public void BasicSetWithDataTest()
        {
            // ARRANGE
            var bitVectorSet = Create();
            var elements = bitVectorSet.Elements;

            // ACT
            var globalBitIndexInBitVectors = 7;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            globalBitIndexInBitVectors = 14;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);

            // ASSERT
            Assert.AreEqual(2, bitVectorSet.Count);

            // DEBUGGING
            // System.Console.WriteLine(bitVectorSet);
        }

        [Test]
        public void BasicSetWithIntersectionDataTest()
        {
            // ARRANGE
            var bitVectorSet = Create();
            var elements = bitVectorSet.Elements;
            var bitVectorSetTwo = Create();

            var globalBitIndexInBitVectors = 7;
            bitVectorSetTwo.Add(elements[globalBitIndexInBitVectors]);
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            globalBitIndexInBitVectors = 14;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);

            // ACT
            bitVectorSet.IntersectWith(bitVectorSetTwo);

            // ASSERT
            Assert.AreEqual(1, bitVectorSet.Count);

            // DEBUGGING
            // System.Console.WriteLine(bitVectorSet);
        }

        [Test]
        public void BasicSetWithUnionDataTest()
        {
            // ARRANGE
            var bitVectorSet = Create();
            var elements = bitVectorSet.Elements;
            var bitVectorSetTwo = Create();

            var globalBitIndexInBitVectors = 7;
            bitVectorSetTwo.Add(elements[globalBitIndexInBitVectors]);
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            globalBitIndexInBitVectors = 14;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);

            // ACT
            bitVectorSet.UnionWith(bitVectorSetTwo);

            // ASSERT
            Assert.AreEqual(2, bitVectorSet.Count);

            // DEBUGGING
            // System.Console.WriteLine(bitVectorSet);
        }

        [Test]
        public void BasicSetWitExorDataTest()
        {
            // ARRANGE
            var bitVectorSet = Create();
            var elements = bitVectorSet.Elements;
            var bitVectorSetTwo = Create();

            var globalBitIndexInBitVectors = 7;
            bitVectorSetTwo.Add(elements[globalBitIndexInBitVectors]);
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            globalBitIndexInBitVectors = 14;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);

            // ACT
            bitVectorSet.ExceptWith(bitVectorSetTwo);

            // ASSERT
            Assert.AreEqual(1, bitVectorSet.Count);

            // DEBUGGING
            // System.Console.WriteLine(bitVectorSet);
        }

        [Test]
        public void BasicSetClear()
        {
            // ARRANGE
            var bitVectorSet = Create();
            var elements = bitVectorSet.Elements;

            var globalBitIndexInBitVectors = 7;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            globalBitIndexInBitVectors = 14;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);

            // ACT
            bitVectorSet.Clear();

            // ASSERT
            Assert.AreEqual(0, bitVectorSet.Count);

            // DEBUGGING
            // System.Console.WriteLine(bitVectorSet);
        }

        [Test]
        public void BasicSetIsContained()
        {
            // ARRANGE
            var bitVectorSet = Create();
            var elements = bitVectorSet.Elements;

            var globalBitIndexInBitVectors = 7;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            globalBitIndexInBitVectors = 14;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);

            // ACT
            var isContained = bitVectorSet.Contains(elements[globalBitIndexInBitVectors]);

            // ASSERT
            Assert.AreEqual(true, isContained);

            // DEBUGGING
            // System.Console.WriteLine(bitVectorSet);
        }

        [Test]
        public void BasicSetCopyTo()
        {
            // ARRANGE
            var bitVectorSet = Create();
            var elements = bitVectorSet.Elements;

            var globalBitIndexInBitVectors = 7;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            globalBitIndexInBitVectors = 14;
            bitVectorSet.Add(elements[globalBitIndexInBitVectors]);
            var array = new string[] { "A10", "A9", "A8" };

            // ACT
            bitVectorSet.CopyTo(array, 0);

            // ASSERT
            for (int i = 1; i <= bitVectorSet.Count; i++)
            {
                Assert.AreEqual(elements[7 * i], array[0 + i - 1]);
            }

            // DEBUGGING
            // System.Console.WriteLine(bitVectorSet);
        }
    }
}