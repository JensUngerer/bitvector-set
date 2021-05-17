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
            var settingIndexOutOfRangeValue = new TestDelegate(() =>
            {
                bitVector[SIZE] = true;
            });
            Assert.Throws<IndexOutOfRangeException>(settingIndexOutOfRangeValue);
            // try opposite
            // Assert.DoesNotThrow(settingIndexOutOfRangeValue);
            // size did not change...
            Assert.AreEqual(1, bitVector.Cardinality);

            var gettingIndexOutOfRange = new TestDelegate(() =>
            {
                var isLastBitSet = bitVector[SIZE];
            });
            Assert.Throws<IndexOutOfRangeException>(gettingIndexOutOfRange);
            // try opposite
            // Assert.DoesNotThrow(gettingIndexOutOfRange);

             var gettingIndexWithinRange = new TestDelegate(() => {
                 var isLastBitSet = bitVector[SIZE - 1];
                 Assert.False(isLastBitSet);
            });
            Assert.DoesNotThrow(gettingIndexWithinRange);
        }

        [Test]
        public void WrongArgumentException() {
            var createBitVectorWithZeroValue = new TestDelegate(() => {
                new BitVector(0);
            });
            Assert.Throws<ArgumentException>(createBitVectorWithZeroValue);
            // try opposite
            // Assert.DoesNotThrow(createBitVectorWithZeroValue);
        }
    }
}