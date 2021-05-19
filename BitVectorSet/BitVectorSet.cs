using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
// using static BitVector;

namespace JensUngerer.BitVectorSet
{
    public class BitVectorSet<T> : ICollection<T>, ISet<T>
    {
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

        public List<T> Elements { get; private set; } = new List<T>();

        public Dictionary<T, int> ElementsMap { get; private set; } = new Dictionary<T, int>();

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
            // var item = Elements[arrayIndex];
            // var isContained = Contains(item);
            // if (isContained)
            // {
            var indexOffset = 0;
            foreach (T item in this)
            {
                array[arrayIndex + indexOffset] = item;
                indexOffset++;
            }
            // }
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

        bool ISet<T>.Add(T item)
        {
            var globalIndex = GetGlobalIndex(item);
            if (!this.BitVector[globalIndex])
            {
                this.BitVector[globalIndex] = true;
                return true;
            }
            return false;
        }

        private BitVector Create(IEnumerable<T> other)
        {
            var created = new BitVector(this.BitVector.Size);
            foreach (var item in other)
            {
                var globalIndex = GetGlobalIndex(item);
                created[globalIndex] = true;
            }
            return created;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            this.BitVector.ExceptWith(otherBitVector);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            this.BitVector.Intersect(otherBitVector);
        }

        private int getIntersectionCardinality(BitVector otherBitVector)
        {
            var ownClonedBitVector = this.BitVector.Clone();
            ownClonedBitVector.Intersect(otherBitVector);
            var subSetCardinality = ownClonedBitVector.Cardinality;
            return subSetCardinality;
        }
        private int getUnionCardinality(BitVector otherBitVector)
        {
            var ownClonedBitVector = this.BitVector.Clone();
            ownClonedBitVector.Union(otherBitVector);
            var superSetCardinality = ownClonedBitVector.Cardinality;
            return superSetCardinality;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            var subSetCardinality = getIntersectionCardinality(otherBitVector);
            // e.g {5} & {3, 5}
            // --> {5} --> own is proper subset of other
            // https://stackoverflow.com/questions/25041998/what-is-the-difference-between-hashsett-issubsetof-and-hashsett-isprope
            if (subSetCardinality == 0)
            {
                return false;
            }
            if (subSetCardinality < this.BitVector.Cardinality)
            {
                return true;
            }
            return false;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            var subSetCardinality = getIntersectionCardinality(otherBitVector);
            int superSetCardinality = getUnionCardinality(otherBitVector);

            // e.g {3, 5} | {5}
            // --> {3, 5} --> own is proper superset of other
            // https://stackoverflow.com/questions/22000393/difference-between-hashset-issupersetof-and-ispropersupersetof
            if (subSetCardinality != 0 &&
            superSetCardinality > this.BitVector.Cardinality)
            {
                return true;
            }

            return false;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            var subSetCardinality = getIntersectionCardinality(otherBitVector);

            // e.g {3, 5} & {3, 5}
            // --> {3, 5} --> own is subset of other
            // https://stackoverflow.com/questions/25041998/what-is-the-difference-between-hashsett-issubsetof-and-hashsett-isprope
            if (subSetCardinality == 0)
            {
                return false;
            }
            if (subSetCardinality <= this.BitVector.Cardinality)
            {
                return true;
            }
            return false;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            int superSetCardinality = getUnionCardinality(otherBitVector);
            var subSetCardinality = getIntersectionCardinality(otherBitVector);
            if (subSetCardinality != 0 &&
                superSetCardinality >= this.BitVector.Cardinality)
            {
                return true;
            }

            return false;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            var subSetCardinality = getIntersectionCardinality(otherBitVector);
            return subSetCardinality > 0;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            var otherBitVector = Create(other);
            var clonedBitVector = BitVector.Clone();
            return clonedBitVector.Equals(otherBitVector);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            // var otherBitVector = Create(other);
            // var clonedOwn = this.BitVector.Clone();
            // clonedOwn.Intersect(otherBitVector);

            // this.BitVector.Union(otherBitVector);
            // foreach (int intersection in clonedOwn)
            // {
            //     this.BitVector[intersection] = false;
            // }

            var otherBitVector = Create(other);
            this.BitVector.SymmetricDifference(otherBitVector);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1.unionwith?view=net-5.0
            var otherBitVector = Create(other);
            this.BitVector.Union(otherBitVector);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            BitVector.StartString(stringBuilder);
            foreach (int globalIdex in BitVector)
            {
                var element = Elements[globalIdex];
                BitVector.AppendEntry(stringBuilder, element);
            }
            BitVector.EndString(stringBuilder);

            return stringBuilder.ToString();
        }
    }
}
