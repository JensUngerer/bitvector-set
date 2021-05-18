using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System;
using System.Reflection;

namespace BitVectorSetLibrary
{
    public class BitVector : IEnumerable, IEquatable<BitVector>
    {
        private static int[] masks;

        public const int NUMBER_OF_BITS_IN_A_BITVECTOR32 = 32;

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

        private void setAt(int globalBitIndexInBitVectors, bool value)
        {
            int indexInBitVectorsArray = getIndexInBitVectorsArea(globalBitIndexInBitVectors);

            // https://www.dotnetperls.com/bitvector32
            int bitMask = getBitMaskAt(globalBitIndexInBitVectors);
            this.BitVectors[indexInBitVectorsArray][bitMask] = value;
        }

        private bool getAt(int globalBitIndexInBitVectors)
        {
            int indexInBitVectorsArray = getIndexInBitVectorsArea(globalBitIndexInBitVectors);

            // https://www.dotnetperls.com/bitvector32
            int bitMask = getBitMaskAt(globalBitIndexInBitVectors);
            return this.BitVectors[indexInBitVectorsArray][bitMask];
        }

        private int size;

        public int Size
        {
            get
            {
                return size;
            }
        }

        public BitVector32[] BitVectors { get; private set; }

        public void ExceptWith(BitVector other)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablehashset-1.system-collections-generic-iset-t--exceptwith?view=net-5.0
            foreach (int globalIndexToRemove in other)
            {
                setAt(globalIndexToRemove, false);
            }
        }

        public void Clear()
        {
            this.BitVectors = new BitVector32[this.BitVectors.Length];
        }

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

        // ?
        // private void IterateAndPerformLogicalOperation(BitVector other, String logicalOperation)
        // {
        //     // https://stackoverflow.com/questions/11113259/how-to-call-custom-operator-with-reflection
        //     var logicalOperator = typeof(int).GetMethod(logicalOperation, BindingFlags.Static | BindingFlags.Public);

        //     for (int i = 0; i < this.BitVectors.Length; i++)
        //     {
        //         object currentData = this.BitVectors[i].Data;
        //         object currentOtherData = other.BitVectors[i].Data;
        //         // currentData &= currentOtherData;
        //         currentData = logicalOperator.Invoke(null, new object[2]{currentData, currentOtherData});

        //         // DEBUGGING:
        //         // Console.WriteLine(currentData);

        //         this.BitVectors[i] = new BitVector32((int)currentData);

        //         // DEBUGGING:
        //         // Console.WriteLine(this.bitVectors[i]);
        //     }
        // }

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

        public void SymmetricDifference(BitVector other)
        {
            for (int i = 0; i < this.BitVectors.Length; i++)
            {
                int currentData = this.BitVectors[i].Data;
                int currentOtherData = other.BitVectors[i].Data;
                currentData ^= currentOtherData;

                // DEBUGGING:
                // Console.WriteLine(currentData);

                this.BitVectors[i] = new BitVector32(currentData);

                // DEBUGGING:
                // Console.WriteLine(this.bitVectors[i]);
            }
        }

        public void Union(BitVector other)
        {
            for (int i = 0; i < this.BitVectors.Length; i++)
            {
                int currentData = this.BitVectors[i].Data;
                int currentOtherData = other.BitVectors[i].Data;
                currentData |= currentOtherData;

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

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
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

        public bool Equals(BitVector other)
        {
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
            var otherBitVectors = other.BitVectors;

            if (this.BitVectors.Length != otherBitVectors.Length)
            {
                System.Console.WriteLine("not equal length");
                return false;
            }
            for (int currentIndex = 0; currentIndex < this.BitVectors.Length; currentIndex++)
            {
                var oneBitVector = this.BitVectors[currentIndex];
                var otherOneBitVector = otherBitVectors[currentIndex];
                var areBothCorrespondingEqual = oneBitVector.Equals(otherOneBitVector);

                isEqual = isEqual && areBothCorrespondingEqual;
                if (!isEqual)
                {
                    // DEBUGGING:
                    // System.Console.WriteLine(this.ToString() + "=?=" + otherObject.ToString());
                    // System.Console.WriteLine("!==" + other.ToString());
                    return false;
                }
            }

            return true;
        }
    }
}