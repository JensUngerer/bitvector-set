using System;
using System.Collections.Generic;

namespace BitVector
{
    public class BitVectorEqualityComparer : IEqualityComparer<BitVector>
    {
        public bool Equals(BitVector x, BitVector y)
        {
            // DEBUGGING:
            // System.Console.WriteLine("x:" + x);
            // System.Console.WriteLine("y:" + y);

            return x.Equals(y);
        }

        public int GetHashCode(BitVector obj)
        {
            // DEBUGGING:
            // System.Console.WriteLine("obj:" + obj);
            return obj.GetHashCode();
        }
    }
}
