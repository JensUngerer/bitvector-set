using System.Collections.Generic;

namespace JensUngerer.BitVectorSet
{
    public abstract class BitVectorSetFactory<T>
    {
        protected Dictionary<T, List<T>> prefixToElementMap = new Dictionary<T, List<T>>();
        protected Dictionary<T, Dictionary<T, int>> prefixToElementsDictionaryMap = new Dictionary<T, Dictionary<T, int>>();

        private BitVectorSet<T> GetDataStructuresAndCreate(int size, T prefix)
        {
            var elements = prefixToElementMap[prefix];
            var elementsDict = prefixToElementsDictionaryMap[prefix];
            return new BitVectorSet<T>(elements, elementsDict, size);
        }

        public BitVectorSet<T> Create(int size, T prefix)
        {
            if (!prefixToElementMap.ContainsKey(prefix))
            {
                CreateDataStructures(size, prefix);
            }
            return GetDataStructuresAndCreate(size, prefix);
        }

        public abstract void CreateDataStructures(int size, T prefix);
    }
}