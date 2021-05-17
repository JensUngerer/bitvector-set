using System.Collections.Generic;
using System.Linq;
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

        private static int getGlobalIndex(int index, int BITVECTOR_LENGTH, int BIT_TO_SET_WITHIN_BITVECTOR)
        {
            return BIT_TO_SET_WITHIN_BITVECTOR + BITVECTOR_LENGTH * index;
        }

        [Test]
        public void GetHashCodeOverflowTest()
        {
            var bitVector = new BitVector(1000);
            var hashSet = new HashSet<BitVector>();
            var index = 0;
            const int BIT_TO_SET_WITHIN_BITVECTOR = 7;

            while (getGlobalIndex(index, BitVector.NUMBER_OF_BITS_IN_A_BITVECTOR32, BIT_TO_SET_WITHIN_BITVECTOR) < 1000)
            {
                bitVector[getGlobalIndex(index, BitVector.NUMBER_OF_BITS_IN_A_BITVECTOR32, BIT_TO_SET_WITHIN_BITVECTOR)] = true;

                // DEBUGGING:
                // System.Console.WriteLine(bitVector.ToString());
                // System.Console.WriteLine(bitVector.GetHashCode());
                var clonedBitVector = bitVector.Clone();
                // System.Console.WriteLine(bitVector.GetHashCode() +"===" + clonedBitVector.GetHashCode());
                hashSet.Add(clonedBitVector);

                index++;
            }

            // DEBUGGING:
            // foreach (var item in hashSet)
            // {  
            //    System.Console.WriteLine(item.ToString()); 
            // }
            // var arr = hashSet.ToArray();
            // for (int i = 0; i < arr.Length -1; i++)
            // {
            //     System.Console.WriteLine(arr[i].Equals(arr[i+1]));
            // }
        }

        [Test]
        public void EqualityComparer()
        {
            var bitVector = new BitVector(1000);
            var hashSet = new HashSet<BitVector>(new BitVectorEqualityComparer());
            var index = 0;
            const int BASE_INDEX = 1;
            while (getGlobalIndex(index, BitVector.NUMBER_OF_BITS_IN_A_BITVECTOR32, BASE_INDEX) < 1000)
            {
                bitVector[getGlobalIndex(index, BitVector.NUMBER_OF_BITS_IN_A_BITVECTOR32, BASE_INDEX)] = true;

                // DEBUGGING:
                // System.Console.WriteLine(bitVector.ToString());
                // System.Console.WriteLine(bitVector.GetHashCode());
                var clonedBitVector = bitVector.Clone();
                // System.Console.WriteLine(bitVector.GetHashCode() +"===" + clonedBitVector.GetHashCode());
                hashSet.Add(clonedBitVector);

                index++;
            }

            // DEBUGGING:
            // foreach (var item in hashSet)
            // {  
            //    System.Console.WriteLine(item.ToString()); 
            // }
            // // var arr = hashSet.ToArray();
            // for (int i = 0; i < arr.Length -1; i++)
            // {
            //     System.Console.WriteLine(arr[i].Equals(arr[i+1]));
            // }
        }
    }
}