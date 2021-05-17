using System;
using NUnit.Framework;

namespace BitVector.Test
{
    public class ExceptionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WrongIndexException()
        {
            // ARRANGE
            const int SIZE = 32;
            var bitVector = new BitVector();

            // ACT & ASSERT
            var globalBitIndexInBitVectors = 7;
            bitVector[globalBitIndexInBitVectors] = true;

            // try to set last index+1 to true
            Assert.AreEqual(1, bitVector.Cardinality);
            var testDelegate = new TestDelegate(() =>
            {
                bitVector[SIZE] = true;
            });
            Assert.Throws<IndexOutOfRangeException>(testDelegate);            
            // try opposite
            // Assert.DoesNotThrow(testDelegate);
            
            // size did not change...
            Assert.AreEqual(1, bitVector.Cardinality);
        }
    }
}