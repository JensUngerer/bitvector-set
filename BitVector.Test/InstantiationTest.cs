using NUnit.Framework;

namespace BitVector.Test
{
    public class InstantiationTest
    {
        private static BitVector CreateFullBitVector(BitVector bitVector, int size)
        {
            for (int i = 0; i < size; i++)
            {
                bitVector[i] = true;
            }
            return bitVector;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreatedInstanceEmptyCardinality()
        {
            // ARRANGE
            var size = 64;
            var bitVector = new BitVector(size);

            // ASSERT
            Assert.AreEqual(0, bitVector.Cardinality);
        }

         [Test]
        public void CreatedInstanceCardinalityOfOne()
        {
            // ARRANGE
            var size = 64;
            var bitVector = new BitVector(size);
            
            // ACT
            var globalBitIndexInBitVectors = 7;
            bitVector[globalBitIndexInBitVectors] = true;

            // ASSERT
            Assert.AreEqual(1, bitVector.Cardinality);
            Assert.IsTrue(bitVector[globalBitIndexInBitVectors]);
        }

        [Test]
        public void CreatedInstanceFullCardinality()
        {
            // ARRANGE
            var size = 64;
            var bitVector = new BitVector(size);
            
            // ACT
            CreateFullBitVector(bitVector, size);

            // ASSERT
            Assert.AreEqual(size, bitVector.Cardinality);
        }

        [Test]
        public void CreatedInstanceFullCardinalityClone()
        {
            // ARRANGE
            var size = 64;
            var bitVector = new BitVector(size);
            
            // ACT
            CreateFullBitVector(bitVector, size);
            var clone = bitVector.Clone();

            // ASSERT
            Assert.AreNotSame(clone, bitVector);
            Assert.AreEqual(size, bitVector.Cardinality);
            Assert.AreEqual(clone.Cardinality, bitVector.Cardinality);
            Assert.AreEqual(clone.ToString(), bitVector.ToString());
        }
    }
}