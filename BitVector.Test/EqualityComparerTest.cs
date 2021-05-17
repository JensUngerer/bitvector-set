using NUnit.Framework;

namespace BitVector.Test
{
    public class EqualityComparerTest
    {

        public EqualityComparerTest()
        {

        }

        [SetUp]
        public void Setup()
        {
        }

        private static int getGlobalIndex(int index)
        {
            const int BITVECTOR_LENGTH = 32;
            const int BIT_TO_SET_WITHIN_BITVECTOR = 7;


            return (BIT_TO_SET_WITHIN_BITVECTOR + BITVECTOR_LENGTH * index);
        }

        [Test]
        public void GetHashCodeOverflowTest()
        {
            var bitVector = new BitVector(1000);

            var index = 0;
            while (getGlobalIndex(index) < 1000)
            {
                bitVector[getGlobalIndex(index)] = true;

                // DEBUGGING:
                System.Console.WriteLine(bitVector.ToString());
                System.Console.WriteLine(bitVector.GetHashCode());

                index++;
            }
        }
    }
}