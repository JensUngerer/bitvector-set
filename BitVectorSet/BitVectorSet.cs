using System;
using System.Collections;
using System.Collections.Generic;
// using static BitVector;

namespace BitVectorSetLibrary
{
    public class BitVectorSet<T> : ICollection<T>
    {

        // private static void Init(int size)  {
        //     // https://stackoverflow.com/questions/34219191/how-to-pass-parameter-to-static-class-constructor
        //     if (Elements.Count == 0 || ElementsMap.Count == 0) {
        //         for (int i = 0; i < size; i++)
        //         {
                                        
        //         }
        //     }
        // }

        private int GetGlobalIndex(T item)
        {
            if (!ElementsMap.ContainsKey(item))
            {
                throw new ArgumentException();
            }
            var globalIndex = ElementsMap[item];
            return globalIndex;
        }

        private BitVector BitVector { get; }

        private static List<T> Elements { get; set;} = new List<T>();

        private static Dictionary<T, int> ElementsMap { get; set;} = new Dictionary<T, int>();

        public BitVectorSet(List<T> elements, Dictionary<T, int> elementsMap, int size = 32)
        {
            Elements = elements;
            ElementsMap = elementsMap;
            BitVector = new BitVector(size);
        }

        public int Count => BitVector.Cardinality;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var globalIndex = GetGlobalIndex(item);
            this.BitVector[globalIndex] = true;
        }

        public void Clear()
        {
            this.BitVector.Clear();
        }

        public bool Contains(T item)
        {
            var globalIndex = GetGlobalIndex(item);
            return this.BitVector[globalIndex];
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var item = Elements[arrayIndex];
            var isContained = Contains(item);
            if (isContained)
            {
                array[arrayIndex] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.BitVector.GetEnumerator();
            foreach (int globalIndex in this.BitVector)
            {
                yield return Elements[globalIndex];
            }
        }

        public bool Remove(T item)
        {
            var globalIndex = GetGlobalIndex(item);
            BitVector[globalIndex] = false;

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
