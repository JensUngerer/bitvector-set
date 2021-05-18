using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BitVectorSetLibrary
{
    public class SetTests
    {
        private static List<String> CreateElements(int size) {
            var elements = new List<String>(size);
            for (int i = 0; i < size; i++)
            {
                elements.Add("A" + i.ToString());
            }
            return elements;
        }

        private static Dictionary<String, int> CreateElementsMap(List<string> elements) {
            var dict = new Dictionary<string, int>();

            var index = 0;
            foreach (var item in elements)
            {
                dict.Add(item, index);
                index++;
            }

            return dict;
        }
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BasicSetTest()
        {
            // ARRANGE
            const int size = 1000;
            var elements = CreateElements(size);
            var dict = CreateElementsMap(elements);
            var bitVectorSet = new BitVectorSet<string>(elements, dict, size);

            // ASSERT
            Assert.AreEqual(0, bitVectorSet.Count);
        }

         [Test]
        public void BasicSetWithDataTest()
        {
            // ARRANGE
            const int size = 1000;
            var elements = CreateElements(size);
            var dict = CreateElementsMap(elements);
            var bitVectorSet = new BitVectorSet<string>(elements, dict, size);

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
    }
}