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
            var hashSet = new HashSet<BitVector>();
            var index = 0;
            while (getGlobalIndex(index) < 1000)
            {
                bitVector[getGlobalIndex(index)] = true;

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
    }
}