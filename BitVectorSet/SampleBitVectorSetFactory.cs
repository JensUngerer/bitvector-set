using System.Collections.Generic;

namespace JensUngerer.BitVectorSet
{
    public class SampleBitVectorFactory : BitVectorSetFactory<string>
    {
        public override void CreateDataStructures(int size, string prefix)
        {
            var elements = new List<string>();
            prefixToElementMap[prefix] = elements;

            var elementsMap = new Dictionary<string, int>();
            prefixToElementsDictionaryMap[prefix] = elementsMap;

            for (int i = 0; i < size; i++)
            {
                var currentElement = prefix + i.ToString();
                elements.Add(currentElement);
                elementsMap.Add(currentElement, i);
            }
        }
    }
}