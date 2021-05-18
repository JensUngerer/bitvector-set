using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BitVectorSetLibrary
{
    public class SetTests
    {
        private static List<String> CreateElements(int size)
        {
            var elements = new List<String>(size);
            for (int i = 0; i < size; i++)
            {
                elements.Add("A" + i.ToString());
            }
            return elements;
        }

        private static Dictionary<String, int> CreateElementsMap(List<string> elements)
        {
            var dict = new Dictionary<string, int>();

            var index = 0;
            foreach (var item in elements)
            {
                dict.Add(item, index);
                index++;
            }

            return dict;
        }

        private static BitVectorSet<string> Create()
        {
            const int size = 1000;
            var elements = CreateElements(size);
            var dict = CreateElementsMap(elements);
            var bitVectorSet = new BitVectorSet<string>(elements, dict, size);
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
            var elements = BitVectorSet<string>.Elements;

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
            var elements = BitVectorSet<string>.Elements;
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
            var elements = BitVectorSet<string>.Elements;
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
            var elements = BitVectorSet<string>.Elements;
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
            var elements = BitVectorSet<string>.Elements;

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
            var elements = BitVectorSet<string>.Elements;

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
    }
}