using System;
using Xunit;
using Xunit.Abstractions;

namespace BitVector.Test
{
    public class BasicTests
    {

        private static BitVector CreateFullBitVector(int size)
        {
            var bitVector = new BitVector(size);
            for (int i = 0; i < size; i++)
            {
                bitVector[i] = true;
            }
            return bitVector;
        }

        private readonly ITestOutputHelper output;

        public BasicTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CreationAndCardinality()
        {
            var size = 64;
            var bitVector = CreateFullBitVector(size);

            // DEBUGGING
            // output.WriteLine("-----");
            // output.WriteLine(bitVector.ToString());
            // output.WriteLine("-----");

            Assert.Equal(size, bitVector.Cardinality);
        }

        [Fact]
        public void CreationAndIntersectionCardinality()
        {
            // Arrange
            var size = 64;
            var bitVector = CreateFullBitVector(size);
            var secondBitVecor = new BitVector(size);
            var globalBitIndexInBitVectors = 7;
            secondBitVecor[globalBitIndexInBitVectors] = true;

            // Act
            bitVector.Intersect(secondBitVecor);

            // DEBUGGING
            // output.WriteLine(bitVector.ToString());

            // Assert
            Assert.Equal(secondBitVecor.Cardinality, bitVector.Cardinality);
        }
    }
}
