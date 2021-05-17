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
    }
}