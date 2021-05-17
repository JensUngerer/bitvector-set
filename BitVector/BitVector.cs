using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;

namespace BitVector
{
    public class BitVector : IEnumerable /*, IEquatable<BitVector>, IEqualityComparer<BitVector>*/
    {
        private const int NUMBER_OF_BITS_IN_A_BITVECTOR32 = 32;

        private static int[] masks;


        private static int getIndexInBitVectorsArea(int globalBitIndexInBitVectors)
        {
            int indexInBitVectorsArray = globalBitIndexInBitVectors / NUMBER_OF_BITS_IN_A_BITVECTOR32;
            return indexInBitVectorsArray;
        }

        private static int getBitMaskAt(int globalBitIndexInBitVectors)
        {
            int localBitIndexInBitVector32 = globalBitIndexInBitVectors % NUMBER_OF_BITS_IN_A_BITVECTOR32;
            int bitMask = masks[localBitIndexInBitVector32];
            return bitMask;
        }

        private static void CheckIndexOutOfRange(int globalBitIndexInBitVectors, int size)
        {
            if (globalBitIndexInBitVectors >= size)
            {
                throw new IndexOutOfRangeException($"{globalBitIndexInBitVectors} >= {size}");
            }
        }

        private bool setAt(int globalBitIndexInBitVectors, bool value)
        {
            int indexInBitVectorsArray = getIndexInBitVectorsArea(globalBitIndexInBitVectors);

            // https://www.dotnetperls.com/bitvector32
            int bitMask = getBitMaskAt(globalBitIndexInBitVectors);
            this.BitVectors[indexInBitVectorsArray][bitMask] = value;

            return true;
        }

        private bool getAt(int globalBitIndexInBitVectors)
        {
            int indexInBitVectorsArray = getIndexInBitVectorsArea(globalBitIndexInBitVectors);

            // https://www.dotnetperls.com/bitvector32
            int bitMask = getBitMaskAt(globalBitIndexInBitVectors);
            return this.BitVectors[indexInBitVectorsArray][bitMask];
        }

        private int size;

        public BitVector32[] BitVectors { get; }

        public int Cardinality
        {
            get
            {
                var cardinality = 0;
                foreach (var globalBitIndexInBitVectors in this)
                {
                    cardinality++;
                }
                return cardinality;
            }
        }

        static BitVector()
        {
            // https://www.dotnetperls.com/bitvector32
            masks = new int[NUMBER_OF_BITS_IN_A_BITVECTOR32];
            masks[0] = BitVector32.CreateMask();
            for (int i = 1; i < NUMBER_OF_BITS_IN_A_BITVECTOR32; i++)
            {
                masks[i] = BitVector32.CreateMask(masks[i - 1]);
            }
        }

        public BitVector(int size = 32)
        {
            if (size <= 0)
            {
                throw new ArgumentException("size must be > 0");
            }
            this.size = size;
            var bitVectorsLength = size / NUMBER_OF_BITS_IN_A_BITVECTOR32;
            if (size % NUMBER_OF_BITS_IN_A_BITVECTOR32 != 0)
            {
                bitVectorsLength++;
            }
            this.BitVectors = new BitVector32[bitVectorsLength];
        }

        // necessary to e.g. create full BitVector?
        // public BitVector(int[] bitVectorData)
        // {
        //     if (bitVectorData == null || bitVectorData.Length == 0)
        //     {
        //         throw new ArgumentException();
        //     }
        //     var length = bitVectorData.Length;
        //     var clonedBitVectors = new BitVector32[length];
        //     int currentIndex = 0;
        //     foreach (var oneBitVectorData in bitVectorData)
        //     {
        //         clonedBitVectors[currentIndex] = new BitVector32(oneBitVectorData);
        //         currentIndex++;
        //     }
        //     this.BitVectors = clonedBitVectors;
        //     this.size = length * NUMBER_OF_BITS_IN_A_BITVECTOR32;
        // }

        private BitVector(BitVector32[] bitVectors)
        {
            // cf.: https://docs.microsoft.com/de-de/dotnet/api/system.object.memberwiseclone?view=net-5.0
            if (bitVectors == null || bitVectors.Length == 0)
            {
                throw new ArgumentException();
            }

            var length = bitVectors.Length;
            var size = length * NUMBER_OF_BITS_IN_A_BITVECTOR32;

            var clonedBitVectors = new BitVector32[length];
            var currentIndex = 0;
            foreach (var oneBitVector in bitVectors)
            {
                clonedBitVectors[currentIndex] = new BitVector32(oneBitVector);
                currentIndex++;
            }
            this.BitVectors = clonedBitVectors;
            this.size = size;
        }

        public BitVector Clone()
        {
            return new BitVector(this.BitVectors);
        }


        public bool this[int globalBitIndexInBitVectors]
        {
            get
            {
                CheckIndexOutOfRange(globalBitIndexInBitVectors, size);
                return getAt(globalBitIndexInBitVectors);
            }
            set
            {
                CheckIndexOutOfRange(globalBitIndexInBitVectors, size);
                setAt(globalBitIndexInBitVectors, value);
            }
        }

        public void Intersect(BitVector other)
        {
            for (int i = 0; i < this.BitVectors.Length; i++)
            {
                int currentData = this.BitVectors[i].Data;
                int currentOtherData = other.BitVectors[i].Data;
                currentData &= currentOtherData;

                // DEBUGGING:
                // Console.WriteLine(currentData);

                this.BitVectors[i] = new BitVector32(currentData);

                // DEBUGGING:
                // Console.WriteLine(this.bitVectors[i]);
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{");

            const string SEPARATOR = ", ";
            foreach (int globalBitIndexInBitVectors in this)
            {
                stringBuilder.Append(globalBitIndexInBitVectors.ToString());
                stringBuilder.Append(SEPARATOR);
            }

            stringBuilder.Remove(stringBuilder.Length - SEPARATOR.Length, SEPARATOR.Length);
            stringBuilder.Append("}");
            stringBuilder.Append('\n');

            return stringBuilder.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            for (int globalBitIndexInBitVectors = 0; globalBitIndexInBitVectors < size; globalBitIndexInBitVectors++)
            {
                if (getAt(globalBitIndexInBitVectors))
                {
                    yield return globalBitIndexInBitVectors;
                }
            }
        }

        public override bool Equals(object otherObject)
        {
            var other = otherObject as BitVector;
            
            // https://stackoverflow.com/questions/567642/how-to-best-implement-equals-for-custom-types
            if (other == null)
            {
                return false;
            }
            if (this == other)
            {
                return true;
            }

            var isEqual = true;
            var currentIndex = 0;
            var otherBitVectors = other.BitVectors;

            if (this.BitVectors.Length != otherBitVectors.Length)
            {
                return false;
            }

            foreach (var oneBitVector in this.BitVectors)
            {
                var otherOneBitVector = otherBitVectors[currentIndex];
                isEqual = isEqual && oneBitVector.Equals(otherOneBitVector);
                if (!isEqual)
                {
                    return false;
                }
                currentIndex++;
            }
            return true;
        }

        // public bool Equals(BitVector x, BitVector y)
        // {
        //     return x.Equals(y);
        // }

        // BitVector obj 
        public override int GetHashCode()
        {
            // var sum = 0;

            // // System.Console.WriteLine("---");
            // foreach (var oneBitVector in this.BitVectors)
            // {
            //     // sum += oneBitVector.Data.GetHashCode();
            //     // sum += oneBitVector.Data;
            //     sum += this.BitVectors.Length;

            //     // DEBUGGING:
            //     // System.Console.WriteLine(sum);
            // }
            // // System.Console.WriteLine("---");


            // return sum;

            // https://stackoverflow.com/questions/3404715/c-sharp-hashcode-for-array-of-ints/3404820
            /**
            * 
            * Be careful! This will only work with the default System.Collections.Generic.GenericEqualityComparer<T>.
            * If your HashSet is materialized by Entity Framework, it will have System.Data.Entity.Infrastructure.ObjectReferenceEqualityComparer. 
            *
            **/
            var array = new int[this.BitVectors.Length];
            var i = 0;
            foreach (var item in this.BitVectors)
            {
                array[i] = this.BitVectors[i].Data;
                i++;
            }
            int hc = array.Length;
            foreach (int val in array)
            {
                hc = unchecked(hc * 314159 + val);
            }
            return hc;
        }

        // public bool Equals(BitVector other)
        // {
        //     // return other.Equals(this);
        //     return false;
        // }

        // public bool Equals(BitVector x, BitVector y)
        // {
        //     return x.Equals(y);
        // }

        // public int GetHashCode(BitVector obj)
        // {
        //     // return obj.GetHashCode();
        //     return -1;
        // }
    }
}